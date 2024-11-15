using Atlas.API.Interfaces;
using Atlas.Core.Constants;
using Atlas.Core.Exceptions;
using Atlas.Core.Logging.Interfaces;
using Atlas.Core.Models;
using Atlas.Data.Access.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Origin.Core.Models;
using Origin.Service.Interface;
using Origin.Test.Data;

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
    }
}
