using Atlas.API.Interfaces;
using Atlas.Core.Constants;
using Atlas.Core.Exceptions;
using Atlas.Core.Logging.Interfaces;
using Atlas.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Origin.Core.Models;
using Origin.Data.Access.Interfaces;

namespace Atlas.API.Endpoints.Origin
{
    internal static class DocumentPropertiesEndpoints
    {
        internal static async Task<IResult> GetDocumentFonts(IDocumentPropertiesData documentPropertiesData, IClaimService claimService, ILogService logService, CancellationToken cancellationToken)
        {
            Authorisation? authorisation = null;

            try
            {
                authorisation = await documentPropertiesData.GetAuthorisationAsync(claimService.GetClaim(), cancellationToken)
                    .ConfigureAwait(false);

                if (authorisation == null
                    || !authorisation.HasPermission(Auth.DOCUMENT_READ))
                {
                    return Results.Unauthorized();
                }

                IEnumerable<DocumentFont>? documentFonts = await documentPropertiesData.GetDocumentFontsAsync(cancellationToken)
                    .ConfigureAwait(false);

                return Results.Ok(new AuthResult<IEnumerable<DocumentFont>?> { Authorisation = authorisation, Result = documentFonts });
            }
            catch (AtlasException ex)
            {
                logService.Log(Core.Logging.Enums.LogLevel.Error, ex.Message, ex, authorisation?.User);

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        internal static async Task<IResult> GetDocumentFont(int id, IDocumentPropertiesData documentPropertiesData, IClaimService claimService, ILogService logService, CancellationToken cancellationToken)
        {
            Authorisation? authorisation = null;

            try
            {
                authorisation = await documentPropertiesData.GetAuthorisationAsync(claimService.GetClaim(), cancellationToken)
                    .ConfigureAwait(false);

                if (authorisation == null
                    || !authorisation.HasPermission(Auth.DOCUMENT_READ))
                {
                    return Results.Unauthorized();
                }

                DocumentFont? documentFont = await documentPropertiesData.GetDocumentFontAsync(id, cancellationToken)
                    .ConfigureAwait(false);

                return Results.Ok(documentFont);
            }
            catch (AtlasException ex)
            {
                logService.Log(Core.Logging.Enums.LogLevel.Error, ex.Message, ex, authorisation?.User);

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        internal static async Task<IResult> CreateDocumentFont([FromBody] DocumentFont documentFont, IDocumentPropertiesData documentPropertiesData, IClaimService claimService, ILogService logService, CancellationToken cancellationToken)
        {
            Authorisation? authorisation = null;

            try
            {
                authorisation = await documentPropertiesData.GetAuthorisationAsync(claimService.GetClaim(), cancellationToken)
                    .ConfigureAwait(false);

                if (authorisation == null
                    || !authorisation.HasPermission(Auth.DOCUMENT_WRITE))
                {
                    return Results.Unauthorized();
                }

                DocumentFont? newdocumentFont = await documentPropertiesData.CreateDocumentFontAsync(documentFont, cancellationToken)
                    .ConfigureAwait(false);

                return Results.Ok(newdocumentFont);
            }
            catch (AtlasException ex)
            {
                logService.Log(Core.Logging.Enums.LogLevel.Error, ex.Message, ex, authorisation?.User);

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        internal static async Task<IResult> UpdateDocumentFont([FromBody] DocumentFont documentFont, IDocumentPropertiesData documentPropertiesData, IClaimService claimService, ILogService logService, CancellationToken cancellationToken)
        {
            Authorisation? authorisation = null;

            try
            {
                authorisation = await documentPropertiesData.GetAuthorisationAsync(claimService.GetClaim(), cancellationToken)
                    .ConfigureAwait(false);

                if (authorisation == null
                    || !authorisation.HasPermission(Auth.DOCUMENT_WRITE))
                {
                    return Results.Unauthorized();
                }

                DocumentFont? updatedDocumentFont = await documentPropertiesData.UpdateDocumentFontAsync(documentFont, cancellationToken)
                    .ConfigureAwait(false);

                return Results.Ok(updatedDocumentFont);
            }
            catch (AtlasException ex)
            {
                logService.Log(Core.Logging.Enums.LogLevel.Error, ex.Message, ex, authorisation?.User);

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        internal static async Task<IResult> DeleteDocumentFont(int id, IDocumentPropertiesData documentPropertiesData, IClaimService claimService, ILogService logService, CancellationToken cancellationToken)
        {
            Authorisation? authorisation = null;

            try
            {
                authorisation = await documentPropertiesData.GetAuthorisationAsync(claimService.GetClaim(), cancellationToken)
                    .ConfigureAwait(false);

                if (authorisation == null
                    || !authorisation.HasPermission(Auth.DOCUMENT_WRITE))
                {
                    return Results.Unauthorized();
                }

                int affectedRows = await documentPropertiesData.DeleteDocumentFontAsync(id, cancellationToken)
                    .ConfigureAwait(false);

                return Results.Ok(affectedRows);
            }
            catch (AtlasException ex)
            {
                logService.Log(Core.Logging.Enums.LogLevel.Error, ex.Message, ex, authorisation?.User);

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        internal static async Task<IResult> GetDocumentColours(IDocumentPropertiesData documentPropertiesData, IClaimService claimService, ILogService logService, CancellationToken cancellationToken)
        {
            Authorisation? authorisation = null;

            try
            {
                authorisation = await documentPropertiesData.GetAuthorisationAsync(claimService.GetClaim(), cancellationToken)
                    .ConfigureAwait(false);

                if (authorisation == null
                    || !authorisation.HasPermission(Auth.DOCUMENT_READ))
                {
                    return Results.Unauthorized();
                }

                IEnumerable<DocumentColour>? documentColours = await documentPropertiesData.GetDocumentColoursAsync(cancellationToken)
                    .ConfigureAwait(false);

                return Results.Ok(new AuthResult<IEnumerable<DocumentColour>?> { Authorisation = authorisation, Result = documentColours });
            }
            catch (AtlasException ex)
            {
                logService.Log(Core.Logging.Enums.LogLevel.Error, ex.Message, ex, authorisation?.User);

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        internal static async Task<IResult> GetDocumentColour(int id, IDocumentPropertiesData documentPropertiesData, IClaimService claimService, ILogService logService, CancellationToken cancellationToken)
        {
            Authorisation? authorisation = null;

            try
            {
                authorisation = await documentPropertiesData.GetAuthorisationAsync(claimService.GetClaim(), cancellationToken)
                    .ConfigureAwait(false);

                if (authorisation == null
                    || !authorisation.HasPermission(Auth.DOCUMENT_READ))
                {
                    return Results.Unauthorized();
                }

                DocumentColour? documentColour = await documentPropertiesData.GetDocumentColourAsync(id, cancellationToken)
                    .ConfigureAwait(false);

                return Results.Ok(documentColour);
            }
            catch (AtlasException ex)
            {
                logService.Log(Core.Logging.Enums.LogLevel.Error, ex.Message, ex, authorisation?.User);

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        internal static async Task<IResult> CreateDocumentColour([FromBody] DocumentColour documentColour, IDocumentPropertiesData documentPropertiesData, IClaimService claimService, ILogService logService, CancellationToken cancellationToken)
        {
            Authorisation? authorisation = null;

            try
            {
                authorisation = await documentPropertiesData.GetAuthorisationAsync(claimService.GetClaim(), cancellationToken)
                    .ConfigureAwait(false);

                if (authorisation == null
                    || !authorisation.HasPermission(Auth.DOCUMENT_WRITE))
                {
                    return Results.Unauthorized();
                }

                DocumentColour? newDocumentColour = await documentPropertiesData.CreateDocumentColourAsync(documentColour, cancellationToken)
                    .ConfigureAwait(false);

                return Results.Ok(newDocumentColour);
            }
            catch (AtlasException ex)
            {
                logService.Log(Core.Logging.Enums.LogLevel.Error, ex.Message, ex, authorisation?.User);

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        internal static async Task<IResult> UpdateDocumentColour([FromBody] DocumentColour documentColour, IDocumentPropertiesData documentPropertiesData, IClaimService claimService, ILogService logService, CancellationToken cancellationToken)
        {
            Authorisation? authorisation = null;

            try
            {
                authorisation = await documentPropertiesData.GetAuthorisationAsync(claimService.GetClaim(), cancellationToken)
                    .ConfigureAwait(false);

                if (authorisation == null
                    || !authorisation.HasPermission(Auth.DOCUMENT_WRITE))
                {
                    return Results.Unauthorized();
                }

                DocumentColour? updatedDocumentColour = await documentPropertiesData.UpdateDocumentColourAsync(documentColour, cancellationToken)
                    .ConfigureAwait(false);

                return Results.Ok(updatedDocumentColour);
            }
            catch (AtlasException ex)
            {
                logService.Log(Core.Logging.Enums.LogLevel.Error, ex.Message, ex, authorisation?.User);

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        internal static async Task<IResult> DeleteDocumentColour(int id, IDocumentPropertiesData documentPropertiesData, IClaimService claimService, ILogService logService, CancellationToken cancellationToken)
        {
            Authorisation? authorisation = null;

            try
            {
                authorisation = await documentPropertiesData.GetAuthorisationAsync(claimService.GetClaim(), cancellationToken)
                    .ConfigureAwait(false);

                if (authorisation == null
                    || !authorisation.HasPermission(Auth.DOCUMENT_WRITE))
                {
                    return Results.Unauthorized();
                }

                int affectedRows = await documentPropertiesData.DeleteDocumentColourAsync(id, cancellationToken)
                    .ConfigureAwait(false);

                return Results.Ok(affectedRows);
            }
            catch (AtlasException ex)
            {
                logService.Log(Core.Logging.Enums.LogLevel.Error, ex.Message, ex, authorisation?.User);

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
