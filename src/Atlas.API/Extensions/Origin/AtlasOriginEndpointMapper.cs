using Atlas.API.Endpoints.Origin;
using Atlas.Core.Constants;
using Atlas.Core.Models;
using Origin.Core.Models;

namespace Atlas.API.Extensions.Origin
{
    internal static class AtlasOriginEndpointMapper
    {
        internal static WebApplication? MapAtlasOriginEndpoints(this WebApplication app)
        {
            app.MapGet($"/{AtlasAPIEndpoints.GET_DOCUMENT_CONFIGS}", DocumentEndpoints.GetDocumentConfigs)
                .WithOpenApi()
                .WithName(AtlasAPIEndpoints.GET_DOCUMENT_CONFIGS)
                .WithDescription("Gets a list of document configurations.")
                .Produces<IEnumerable<DocumentConfig>?>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapGet($"/{AtlasAPIEndpoints.GET_DOCUMENT_CONFIG}/{{id:int}}", DocumentEndpoints.GetDocumentConfig)
                .WithOpenApi()
                .WithName(AtlasAPIEndpoints.GET_DOCUMENT_CONFIG)
                .WithDescription("Gets a document configuration for the given id. If id is 0 then returns a new instance of a blank document configuration for creation.")
                .Produces<DocumentConfig>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapPost($"/{AtlasAPIEndpoints.CREATE_DOCUMENT_CONFIG}", DocumentEndpoints.CreateDocumentConfig)
                .WithOpenApi()
                .WithName(AtlasAPIEndpoints.CREATE_DOCUMENT_CONFIG)
                .WithDescription("Create a new document configuration.")
                .Produces<DocumentConfig>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapPut($"/{AtlasAPIEndpoints.UPDATE_DOCUMENT_CONFIG}", DocumentEndpoints.UpdateDocumentConfig)
                .WithOpenApi()
                .WithName(AtlasAPIEndpoints.UPDATE_DOCUMENT_CONFIG)
                .WithDescription("Updates the document configuration.")
                .Produces<DocumentConfig>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapDelete($"/{AtlasAPIEndpoints.DELETE_DOCUMENT_CONFIG}/{{id:int}}", DocumentEndpoints.DeleteDocumentConfig)
                .WithOpenApi()
                .WithName(AtlasAPIEndpoints.DELETE_DOCUMENT_CONFIG)
                .WithDescription("Delete's a document configuration of the given id and returns the number of affected rows.")
                .Produces<int>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapGet($"/{AtlasAPIEndpoints.GET_DOCUMENT_PARAGRAPHS}", DocumentEndpoints.GetDocumentParagraphs)
                .WithOpenApi()
                .WithName(AtlasAPIEndpoints.GET_DOCUMENT_PARAGRAPHS)
                .WithDescription("Gets a list of document paragraphs.")
                .Produces<IEnumerable<DocumentParagraph>?>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapGet($"/{AtlasAPIEndpoints.GET_DOCUMENT_PARAGRAPH}/{{id:int}}", DocumentEndpoints.GetDocumentParagraph)
                .WithOpenApi()
                .WithName(AtlasAPIEndpoints.GET_DOCUMENT_PARAGRAPH)
                .WithDescription("Gets a document paragraph for the given id. If id is 0 then returns a new instance of a blank document paragraph for creation.")
                .Produces<DocumentParagraph>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapPost($"/{AtlasAPIEndpoints.CREATE_DOCUMENT_PARAGRAPH}", DocumentEndpoints.CreateDocumentParagraph)
                .WithOpenApi()
                .WithName(AtlasAPIEndpoints.CREATE_DOCUMENT_PARAGRAPH)
                .WithDescription("Create a new document paragraph.")
                .Produces<DocumentParagraph>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapPut($"/{AtlasAPIEndpoints.UPDATE_DOCUMENT_PARAGRAPH}", DocumentEndpoints.UpdateDocumentParagraph)
                .WithOpenApi()
                .WithName(AtlasAPIEndpoints.UPDATE_DOCUMENT_PARAGRAPH)
                .WithDescription("Updates the document paragraph.")
                .Produces<DocumentParagraph>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapDelete($"/{AtlasAPIEndpoints.DELETE_DOCUMENT_PARAGRAPH}/{{id:int}}", DocumentEndpoints.DeleteDocumentParagraph)
                .WithOpenApi()
                .WithName(AtlasAPIEndpoints.DELETE_DOCUMENT_PARAGRAPH)
                .WithDescription("Delete's a document paragraph of the given id and returns the number of affected rows.")
                .Produces<int>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapPost($"/{AtlasAPIEndpoints.GET_ORIGIN_OPTIONS}", OriginOptionsEndpoints.GetOriginOptions)
                .WithOpenApi()
                .WithName(AtlasAPIEndpoints.GET_ORIGIN_OPTIONS)
                .WithDescription("Gets origin option items for the specified origin options code")
                .Produces<IEnumerable<OptionItem>?>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapPost($"/{AtlasAPIEndpoints.GET_GENERIC_ORIGIN_OPTIONS}", OriginOptionsEndpoints.GetGenericOriginOptions)
                .WithOpenApi()
                .WithName(AtlasAPIEndpoints.GET_GENERIC_ORIGIN_OPTIONS)
                .WithDescription("Gets serialized generic origin options list for the specified origin options code")
                .Produces<string>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            return app;
        }
    }
}
