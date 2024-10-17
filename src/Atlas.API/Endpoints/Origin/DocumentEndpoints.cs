using Atlas.API.Interfaces;
using Atlas.API.Utility;
using Atlas.Core.Constants;
using Atlas.Core.Exceptions;
using Atlas.Core.Logging.Interfaces;
using Atlas.Core.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Origin.Core.Models;
using Origin.Data.Access.Interfaces;

namespace Atlas.API.Endpoints.Origin
{
    internal static class DocumentEndpoints
    {
        internal static async Task<IResult> GetDocumentConfigs(IDocumentData documentData, IClaimService claimService, ILogService logService, CancellationToken cancellationToken)
        {
            Authorisation? authorisation = null;

            try
            {
                authorisation = await documentData.GetAuthorisationAsync(claimService.GetClaim(), cancellationToken)
                    .ConfigureAwait(false);

                if (authorisation == null
                    || !authorisation.HasPermission(Auth.DOCUMENT_READ))
                {
                    return Results.Unauthorized();
                }

                IEnumerable<DocumentConfig>? documentConfigs = await documentData.GetDocumentConfigsAsync(cancellationToken)
                    .ConfigureAwait(false);

                return Results.Ok(new AuthResult<IEnumerable<DocumentConfig>?> { Authorisation = authorisation, Result = documentConfigs });
            }
            catch (AtlasException ex)
            {
                logService.Log(Core.Logging.Enums.LogLevel.Error, ex.Message, ex, authorisation?.User);

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        internal static async Task<IResult> GetDocumentConfig(int id, IDocumentData documentData, IClaimService claimService, ILogService logService, CancellationToken cancellationToken)
        {
            Authorisation? authorisation = null;

            try
            {
                authorisation = await documentData.GetAuthorisationAsync(claimService.GetClaim(), cancellationToken)
                    .ConfigureAwait(false);

                if (authorisation == null
                    || !authorisation.HasPermission(Auth.DOCUMENT_READ))
                {
                    return Results.Unauthorized();
                }

                DocumentConfig? documentConfig = await documentData.GetDocumentConfigAsync(id, cancellationToken)
                    .ConfigureAwait(false);

                return Results.Ok(documentConfig);
            }
            catch (AtlasException ex)
            {
                logService.Log(Core.Logging.Enums.LogLevel.Error, ex.Message, ex, authorisation?.User);

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        internal static async Task<IResult> CreateDocumentConfig([FromBody] DocumentConfig documentConfig, IValidator<DocumentConfig> validator, IDocumentData documentData, IClaimService claimService, ILogService logService, CancellationToken cancellationToken)
        {
            Authorisation? authorisation = null;

            try
            {
                authorisation = await documentData.GetAuthorisationAsync(claimService.GetClaim(), cancellationToken)
                    .ConfigureAwait(false);

                if (authorisation == null
                    || !authorisation.HasPermission(Auth.DOCUMENT_WRITE))
                {
                    return Results.Unauthorized();
                }

                await validator.ValidateAndThrowAtlasException(documentConfig, "CreateDocumentConfig", cancellationToken).ConfigureAwait(false);

                DocumentConfig? newDocumentConfig = await documentData.CreateDocumentConfigAsync(documentConfig, cancellationToken)
                    .ConfigureAwait(false);

                return Results.Ok(newDocumentConfig);
            }
            catch (AtlasException ex)
            {
                logService.Log(Core.Logging.Enums.LogLevel.Error, ex.Message, ex, authorisation?.User);

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        internal static async Task<IResult> UpdateDocumentConfig([FromBody] DocumentConfig documentConfig, IValidator<DocumentConfig> validator, IDocumentData documentData, IClaimService claimService, ILogService logService, CancellationToken cancellationToken)
        {
            Authorisation? authorisation = null;

            try
            {
                authorisation = await documentData.GetAuthorisationAsync(claimService.GetClaim(), cancellationToken)
                    .ConfigureAwait(false);

                if (authorisation == null
                    || !authorisation.HasPermission(Auth.DOCUMENT_WRITE))
                {
                    return Results.Unauthorized();
                }

                await validator.ValidateAndThrowAtlasException(documentConfig, "UpdateDocumentConfig", cancellationToken).ConfigureAwait(false);

                DocumentConfig? updatedDocumentConfig = await documentData.UpdateDocumentConfigAsync(documentConfig, cancellationToken)
                    .ConfigureAwait(false);

                return Results.Ok(updatedDocumentConfig);
            }
            catch (AtlasException ex)
            {
                logService.Log(Core.Logging.Enums.LogLevel.Error, ex.Message, ex, authorisation?.User);

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        internal static async Task<IResult> DeleteDocumentConfig(int id, IDocumentData documentData, IClaimService claimService, ILogService logService, CancellationToken cancellationToken)
        {
            Authorisation? authorisation = null;

            try
            {
                authorisation = await documentData.GetAuthorisationAsync(claimService.GetClaim(), cancellationToken)
                    .ConfigureAwait(false);

                if (authorisation == null
                    || !authorisation.HasPermission(Auth.DOCUMENT_WRITE))
                {
                    return Results.Unauthorized();
                }

                int affectedRows = await documentData.DeleteDocumentConfigAsync(id, cancellationToken)
                    .ConfigureAwait(false);

                return Results.Ok(affectedRows);
            }
            catch (AtlasException ex)
            {
                logService.Log(Core.Logging.Enums.LogLevel.Error, ex.Message, ex, authorisation?.User);

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        internal static async Task<IResult> GetDocumentParagraphs(IDocumentData documentData, IClaimService claimService, ILogService logService, CancellationToken cancellationToken)
        {
            Authorisation? authorisation = null;

            try
            {
                authorisation = await documentData.GetAuthorisationAsync(claimService.GetClaim(), cancellationToken)
                    .ConfigureAwait(false);

                if (authorisation == null
                    || !authorisation.HasPermission(Auth.DOCUMENT_READ))
                {
                    return Results.Unauthorized();
                }

                IEnumerable<DocumentParagraph>? documentParagraphs = await documentData.GetDocumentParagraphsAsync(cancellationToken)
                    .ConfigureAwait(false);

                return Results.Ok(new AuthResult<IEnumerable<DocumentParagraph>?> { Authorisation = authorisation, Result = documentParagraphs });
            }
            catch (AtlasException ex)
            {
                logService.Log(Core.Logging.Enums.LogLevel.Error, ex.Message, ex, authorisation?.User);

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        internal static async Task<IResult> GetDocumentParagraph(int id, IDocumentData documentData, IClaimService claimService, ILogService logService, CancellationToken cancellationToken)
        {
            Authorisation? authorisation = null;

            try
            {
                authorisation = await documentData.GetAuthorisationAsync(claimService.GetClaim(), cancellationToken)
                    .ConfigureAwait(false);

                if (authorisation == null
                    || !authorisation.HasPermission(Auth.DOCUMENT_READ))
                {
                    return Results.Unauthorized();
                }

                DocumentParagraph? documentParagraph = await documentData.GetDocumentParagraphAsync(id, cancellationToken)
                    .ConfigureAwait(false);

                return Results.Ok(documentParagraph);
            }
            catch (AtlasException ex)
            {
                logService.Log(Core.Logging.Enums.LogLevel.Error, ex.Message, ex, authorisation?.User);

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        internal static async Task<IResult> CreateDocumentParagraph([FromBody] DocumentParagraph documentParagraph, IValidator<DocumentParagraph> validator, IDocumentData documentData, IClaimService claimService, ILogService logService, CancellationToken cancellationToken)
        {
            Authorisation? authorisation = null;

            try
            {
                authorisation = await documentData.GetAuthorisationAsync(claimService.GetClaim(), cancellationToken)
                    .ConfigureAwait(false);

                if (authorisation == null
                    || !authorisation.HasPermission(Auth.DOCUMENT_WRITE))
                {
                    return Results.Unauthorized();
                }

                await validator.ValidateAndThrowAtlasException(documentParagraph, "CreateDocumentParagraph", cancellationToken).ConfigureAwait(false);

                DocumentParagraph? newDocumentParagraph = await documentData.CreateDocumentParagraphAsync(documentParagraph, cancellationToken)
                    .ConfigureAwait(false);

                return Results.Ok(newDocumentParagraph);
            }
            catch (AtlasException ex)
            {
                logService.Log(Core.Logging.Enums.LogLevel.Error, ex.Message, ex, authorisation?.User);

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        internal static async Task<IResult> UpdateDocumentParagraph([FromBody] DocumentParagraph documentParagraph, IValidator<DocumentParagraph> validator, IDocumentData documentData, IClaimService claimService, ILogService logService, CancellationToken cancellationToken)
        {
            Authorisation? authorisation = null;

            try
            {
                authorisation = await documentData.GetAuthorisationAsync(claimService.GetClaim(), cancellationToken)
                    .ConfigureAwait(false);

                if (authorisation == null
                    || !authorisation.HasPermission(Auth.DOCUMENT_WRITE))
                {
                    return Results.Unauthorized();
                }

                await validator.ValidateAndThrowAtlasException(documentParagraph, "UpdateDocumentParagraph", cancellationToken).ConfigureAwait(false);
                
                DocumentParagraph? updatedDocumentParagraph = await documentData.UpdateDocumentParagraphAsync(documentParagraph, cancellationToken)
                    .ConfigureAwait(false);

                return Results.Ok(updatedDocumentParagraph);
            }
            catch (AtlasException ex)
            {
                logService.Log(Core.Logging.Enums.LogLevel.Error, ex.Message, ex, authorisation?.User);

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        internal static async Task<IResult> DeleteDocumentParagraph(int id, IDocumentData documentData, IClaimService claimService, ILogService logService, CancellationToken cancellationToken)
        {
            Authorisation? authorisation = null;

            try
            {
                authorisation = await documentData.GetAuthorisationAsync(claimService.GetClaim(), cancellationToken)
                    .ConfigureAwait(false);

                if (authorisation == null
                    || !authorisation.HasPermission(Auth.DOCUMENT_WRITE))
                {
                    return Results.Unauthorized();
                }

                int affectedRows = await documentData.DeleteDocumentParagraphAsync(id, cancellationToken)
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
