using Atlas.API.Interfaces;
using Atlas.API.Utility;
using Atlas.Core.Constants;
using Atlas.Core.Exceptions;
using Atlas.Core.Models;
using Atlas.Logging.Interfaces;
using FluentValidation;
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
                logService.Log(Logging.Enums.LogLevel.Error, ex.Message, ex, authorisation?.User);

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
                logService.Log(Logging.Enums.LogLevel.Error, ex.Message, ex, authorisation?.User);

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        internal static async Task<IResult> CreateDocumentFont([FromBody] DocumentFont documentFont, IValidator<DocumentFont> validator, IDocumentPropertiesData documentPropertiesData, IClaimService claimService, ILogService logService, CancellationToken cancellationToken)
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

                await validator.ValidateAndThrowAtlasException(documentFont, "CreateDocumentFont", cancellationToken).ConfigureAwait(false);

                DocumentFont? newdocumentFont = await documentPropertiesData.CreateDocumentFontAsync(documentFont, cancellationToken)
                    .ConfigureAwait(false);

                return Results.Ok(newdocumentFont);
            }
            catch (AtlasException ex)
            {
                logService.Log(Logging.Enums.LogLevel.Error, ex.Message, ex, authorisation?.User);

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        internal static async Task<IResult> UpdateDocumentFont([FromBody] DocumentFont documentFont, IValidator<DocumentFont> validator, IDocumentPropertiesData documentPropertiesData, IClaimService claimService, ILogService logService, CancellationToken cancellationToken)
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

                await validator.ValidateAndThrowAtlasException(documentFont, "UpdateDocumentFont", cancellationToken).ConfigureAwait(false);

                DocumentFont? updatedDocumentFont = await documentPropertiesData.UpdateDocumentFontAsync(documentFont, cancellationToken)
                    .ConfigureAwait(false);

                return Results.Ok(updatedDocumentFont);
            }
            catch (AtlasException ex)
            {
                logService.Log(Logging.Enums.LogLevel.Error, ex.Message, ex, authorisation?.User);

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
                logService.Log(Logging.Enums.LogLevel.Error, ex.Message, ex, authorisation?.User);

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
                logService.Log(Logging.Enums.LogLevel.Error, ex.Message, ex, authorisation?.User);

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
                logService.Log(Logging.Enums.LogLevel.Error, ex.Message, ex, authorisation?.User);

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        internal static async Task<IResult> CreateDocumentColour([FromBody] DocumentColour documentColour, IValidator<DocumentColour> validator, IDocumentPropertiesData documentPropertiesData, IClaimService claimService, ILogService logService, CancellationToken cancellationToken)
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

                await validator.ValidateAndThrowAtlasException(documentColour, "CreateDocumentColour", cancellationToken).ConfigureAwait(false);

                DocumentColour? newDocumentColour = await documentPropertiesData.CreateDocumentColourAsync(documentColour, cancellationToken)
                    .ConfigureAwait(false);

                return Results.Ok(newDocumentColour);
            }
            catch (AtlasException ex)
            {
                logService.Log(Logging.Enums.LogLevel.Error, ex.Message, ex, authorisation?.User);

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        internal static async Task<IResult> UpdateDocumentColour([FromBody] DocumentColour documentColour, IValidator<DocumentColour> validator, IDocumentPropertiesData documentPropertiesData, IClaimService claimService, ILogService logService, CancellationToken cancellationToken)
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

                await validator.ValidateAndThrowAtlasException(documentColour, "UpdateDocumentColour", cancellationToken).ConfigureAwait(false);

                DocumentColour? updatedDocumentColour = await documentPropertiesData.UpdateDocumentColourAsync(documentColour, cancellationToken)
                    .ConfigureAwait(false);

                return Results.Ok(updatedDocumentColour);
            }
            catch (AtlasException ex)
            {
                logService.Log(Logging.Enums.LogLevel.Error, ex.Message, ex, authorisation?.User);

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
                logService.Log(Logging.Enums.LogLevel.Error, ex.Message, ex, authorisation?.User);

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
