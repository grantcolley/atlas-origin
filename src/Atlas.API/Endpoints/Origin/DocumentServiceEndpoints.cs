using Atlas.API.Interfaces;
using Atlas.Core.Constants;
using Atlas.Core.Dynamic;
using Atlas.Core.Exceptions;
using Atlas.Core.Models;
using Atlas.Data.Access.Interfaces;
using Atlas.Logging.Interfaces;
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
                logService.Log(Logging.Enums.LogLevel.Error, ex.Message, new AtlasException(ex.Message, ex), authorisation?.User);

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        internal static async Task<IResult> GetCustomerProductDocument(int id, IDocumentData documentData, ICommercialData commercialData, IClaimService claimService, ILogService logService, CancellationToken cancellationToken)
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

                Company? company = await commercialData.GetCompanyAsync(1, cancellationToken)
                    .ConfigureAwait(false);

                if (company == null) throw new NullReferenceException(nameof(company));

                Customer? customer = await commercialData.GetCustomerByProductAsync(id, cancellationToken)
                    .ConfigureAwait(false);

                if (customer == null) throw new NullReferenceException(nameof(customer));

                Product? product = customer.Products.FirstOrDefault(p => p.ProductId.Equals(id));

                if (product == null) throw new NullReferenceException(nameof(product));

                IEnumerable<DocumentConfig> documentConfigs = await documentData.GetDocumentConfigsAsync(cancellationToken)
                    .ConfigureAwait(false);

                DocumentConfig customerLetter = documentConfigs.First(dc => dc.Name != null && dc.Name.Equals("Customer Product Letter"));

                DocumentConfig? documentConfig = await documentData.GetDocumentConfigAsync(customerLetter.DocumentConfigId, cancellationToken)
                                    .ConfigureAwait(false);

                if (documentConfig == null) throw new NullReferenceException(nameof(documentConfig));

                DynamicType<Customer> customerType = DynamicTypeHelper.Get<Customer>();
                DynamicType<Product> productType = DynamicTypeHelper.Get<Product>();
                DynamicType<Company> companyType = DynamicTypeHelper.Get<Company>();

                foreach (DocumentSubstitute substitute in documentConfig.Substitutes)
                {
                    if (!string.IsNullOrEmpty(substitute.Key))
                    {
                        if (customerType.SupportedProperties.Any(pi => pi.Name == substitute.Key))
                        {

                            substitute.Value = customerType.GetValue(customer, substitute.Key)?.ToString();

                            continue;
                        }

                        if(productType.SupportedProperties.Any(pi => pi.Name == substitute.Key))
                        {
                            if(substitute.Key.Contains("Date"))
                            {
                                substitute.Value = Convert.ToDateTime(productType.GetValue(product, substitute.Key)).GetDateTimeFormats('d')[0];
                            }
                            else
                            {
                                substitute.Value = productType.GetValue(product, substitute.Key)?.ToString();
                            }

                            continue;
                        }

                        if(companyType.SupportedProperties.Any(pi => pi.Name == substitute.Key))
                        {
                            substitute.Value = companyType.GetValue(company, substitute.Key)?.ToString();
                        }
                    }
                }

                documentConfig.ApplySubstitutes = true;

                Document document = new()
                {
                    DocumentGeneratorType = DocumentGeneratorType.PdfSharp,
                    Config = documentConfig,
                    FilenameTemplate = $"Customer_Product_[Surname]",                    
                };

                return Results.Ok(document);
            }
            catch (AtlasException ex)
            {
                logService.Log(Logging.Enums.LogLevel.Error, ex.Message, ex, authorisation?.User);

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
