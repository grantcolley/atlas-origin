using Atlas.API.Interfaces;
using Atlas.Core.Constants;
using Atlas.Core.Dynamic;
using Atlas.Core.Exceptions;
using Atlas.Core.Logging.Interfaces;
using Atlas.Core.Models;
using Atlas.Data.Access.Interfaces;
using Commercial.Core.Models;
using Commercial.Data.Access.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Origin.Core.Models;
using Origin.Data.Access.Interfaces;
using Origin.Service.Interface;

namespace Atlas.API.Endpoints.Origin
{
    public static class DocumentServiceEndpoints
    {
        internal static async Task<IResult> GeneratePdf([FromBody] DocumentConfig documentConfig, IUserAuthorisationData userAuthorisationData, IDocumentGeneratorProvider documentGeneratorProvider, IClaimService claimService, ILogService logService, CancellationToken cancellationToken)
        {
            Authorisation? authorisation = null;

            try
            {
                authorisation = await userAuthorisationData.GetAuthorisationAsync(claimService.GetClaim(), cancellationToken)
                    .ConfigureAwait(false);

                if (authorisation == null
                    || !authorisation.HasPermission(Auth.DOCUMENT_READ))
                {
                    return Results.Unauthorized();
                }

                IDocumentGenerator documentGenerator = documentGeneratorProvider.GetDocumentGenerator(DocumentGeneratorType.PdfSharp);

                byte[] contents = documentGenerator.Generate(documentConfig);

                return Results.File(contents, "application/pdf");
            }
            catch (Exception ex)
            {
                logService.Log(Core.Logging.Enums.LogLevel.Error, ex.Message, new AtlasException(ex.Message, ex), authorisation?.User);

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        internal static async Task<IResult> GetCustomerDocument(int customerId, IDocumentData documentData, ICommercialData commercialData, IClaimService claimService, ILogService logService, CancellationToken cancellationToken)
        {
            Authorisation? authorisation = null;

            try
            {
                authorisation = await commercialData.GetAuthorisationAsync(claimService.GetClaim(), cancellationToken)
                    .ConfigureAwait(false);

                if (authorisation == null
                    || !authorisation.HasPermission(Auth.COMMERCIAL_READ)
                    || !authorisation.HasPermission(Auth.DOCUMENT_READ))
                {
                    return Results.Unauthorized();
                }

                Customer? customer = await commercialData.GetCustomerAsync(customerId, cancellationToken)
                    .ConfigureAwait(false);

                if (customer == null) throw new NullReferenceException(nameof(customer));

                IEnumerable<DocumentConfig> documentConfigs = await documentData.GetDocumentConfigsAsync(cancellationToken)
                    .ConfigureAwait(false);

                DocumentConfig customerLetter = documentConfigs.First(dc => dc.Name != null && dc.Name.Equals("Customer Letter"));

                DocumentConfig? documentConfig = await documentData.GetDocumentConfigAsync(customerLetter.DocumentConfigId, cancellationToken)
                                    .ConfigureAwait(false);

                if (documentConfig == null) throw new NullReferenceException(nameof(documentConfig));

                DynamicType<Customer> customerType = DynamicTypeHelper.Get<Customer>();
                DynamicType<Product> productType = DynamicTypeHelper.Get<Product>();

                foreach (DocumentSubstitute substitute in documentConfig.Substitutes)
                {
                    if (customerType.SupportedProperties.Any(pi => pi.Name == substitute.Key))
                    {
                        if (!string.IsNullOrEmpty(substitute.Key))
                        {
                            substitute.Value = customerType.GetValue(customer, substitute.Key)?.ToString();
                        }
                    }
                }

                return Results.Ok(customer);
            }
            catch (AtlasException ex)
            {
                logService.Log(Core.Logging.Enums.LogLevel.Error, ex.Message, ex, authorisation?.User);

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
