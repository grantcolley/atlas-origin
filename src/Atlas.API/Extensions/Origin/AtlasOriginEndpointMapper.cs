using Atlas.API.Endpoints.Origin;
using Atlas.Core.Constants;
using Atlas.Core.Models;
using Origin.Core.Constants;
using Origin.Core.Models;

namespace Atlas.API.Extensions.Origin
{
    internal static class AtlasOriginEndpointMapper
    {
        internal static WebApplication? MapAtlasOriginEndpoints(this WebApplication app)
        {
            app.MapGet($"/{OriginAPIEndpoints.GET_DOCUMENT_CONFIGS}", DocumentEndpoints.GetDocumentConfigs)
                .WithOpenApi()
                .WithName(OriginAPIEndpoints.GET_DOCUMENT_CONFIGS)
                .WithDescription("Gets a list of document configurations.")
                .Produces<IEnumerable<DocumentConfig>?>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapGet($"/{OriginAPIEndpoints.GET_DOCUMENT_CONFIG}/{{id:int}}", DocumentEndpoints.GetDocumentConfig)
                .WithOpenApi()
                .WithName(OriginAPIEndpoints.GET_DOCUMENT_CONFIG)
                .WithDescription("Gets a document configuration for the given id. If id is 0 then returns a new instance of a blank document configuration for creation.")
                .Produces<DocumentConfig>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapPost($"/{OriginAPIEndpoints.CREATE_DOCUMENT_CONFIG}", DocumentEndpoints.CreateDocumentConfig)
                .WithOpenApi()
                .WithName(OriginAPIEndpoints.CREATE_DOCUMENT_CONFIG)
                .WithDescription("Create a new document configuration.")
                .Produces<DocumentConfig>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapPut($"/{OriginAPIEndpoints.UPDATE_DOCUMENT_CONFIG}", DocumentEndpoints.UpdateDocumentConfig)
                .WithOpenApi()
                .WithName(OriginAPIEndpoints.UPDATE_DOCUMENT_CONFIG)
                .WithDescription("Updates the document configuration.")
                .Produces<DocumentConfig>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapDelete($"/{OriginAPIEndpoints.DELETE_DOCUMENT_CONFIG}/{{id:int}}", DocumentEndpoints.DeleteDocumentConfig)
                .WithOpenApi()
                .WithName(OriginAPIEndpoints.DELETE_DOCUMENT_CONFIG)
                .WithDescription("Delete's a document configuration of the given id and returns the number of affected rows.")
                .Produces<int>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapGet($"/{OriginAPIEndpoints.GET_DOCUMENT_PARAGRAPHS}", DocumentEndpoints.GetDocumentParagraphs)
                .WithOpenApi()
                .WithName(OriginAPIEndpoints.GET_DOCUMENT_PARAGRAPHS)
                .WithDescription("Gets a list of document paragraphs.")
                .Produces<IEnumerable<DocumentParagraph>?>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapGet($"/{OriginAPIEndpoints.GET_DOCUMENT_PARAGRAPH}/{{id:int}}", DocumentEndpoints.GetDocumentParagraph)
                .WithOpenApi()
                .WithName(OriginAPIEndpoints.GET_DOCUMENT_PARAGRAPH)
                .WithDescription("Gets a document paragraph for the given id. If id is 0 then returns a new instance of a blank document paragraph for creation.")
                .Produces<DocumentParagraph>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapPost($"/{OriginAPIEndpoints.CREATE_DOCUMENT_PARAGRAPH}", DocumentEndpoints.CreateDocumentParagraph)
                .WithOpenApi()
                .WithName(OriginAPIEndpoints.CREATE_DOCUMENT_PARAGRAPH)
                .WithDescription("Create a new document paragraph.")
                .Produces<DocumentParagraph>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapPut($"/{OriginAPIEndpoints.UPDATE_DOCUMENT_PARAGRAPH}", DocumentEndpoints.UpdateDocumentParagraph)
                .WithOpenApi()
                .WithName(OriginAPIEndpoints.UPDATE_DOCUMENT_PARAGRAPH)
                .WithDescription("Updates the document paragraph.")
                .Produces<DocumentParagraph>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapDelete($"/{OriginAPIEndpoints.DELETE_DOCUMENT_PARAGRAPH}/{{id:int}}", DocumentEndpoints.DeleteDocumentParagraph)
                .WithOpenApi()
                .WithName(OriginAPIEndpoints.DELETE_DOCUMENT_PARAGRAPH)
                .WithDescription("Delete's a document paragraph of the given id and returns the number of affected rows.")
                .Produces<int>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapGet($"/{OriginAPIEndpoints.GET_DOCUMENT_FONTS}", DocumentPropertiesEndpoints.GetDocumentFonts)
                .WithOpenApi()
                .WithName(OriginAPIEndpoints.GET_DOCUMENT_FONTS)
                .WithDescription("Gets a list of document fonts.")
                .Produces<IEnumerable<DocumentFont>?>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapGet($"/{OriginAPIEndpoints.GET_DOCUMENT_FONT}/{{id:int}}", DocumentPropertiesEndpoints.GetDocumentFont)
                .WithOpenApi()
                .WithName(OriginAPIEndpoints.GET_DOCUMENT_FONT)
                .WithDescription("Gets a document font for the given id.")
                .Produces<DocumentFont>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapPost($"/{OriginAPIEndpoints.CREATE_DOCUMENT_FONT}", DocumentPropertiesEndpoints.CreateDocumentFont)
                .WithOpenApi()
                .WithName(OriginAPIEndpoints.CREATE_DOCUMENT_FONT)
                .WithDescription("Create a new document font.")
                .Produces<DocumentFont>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapPut($"/{OriginAPIEndpoints.UPDATE_DOCUMENT_FONT}", DocumentPropertiesEndpoints.UpdateDocumentFont)
                .WithOpenApi()
                .WithName(OriginAPIEndpoints.UPDATE_DOCUMENT_FONT)
                .WithDescription("Updates the document font.")
                .Produces<DocumentFont>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapDelete($"/{OriginAPIEndpoints.DELETE_DOCUMENT_FONT}/{{id:int}}", DocumentPropertiesEndpoints.DeleteDocumentFont)
                .WithOpenApi()
                .WithName(OriginAPIEndpoints.DELETE_DOCUMENT_FONT)
                .WithDescription("Delete's a document font of the given id and returns the number of affected rows.")
                .Produces<int>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapGet($"/{OriginAPIEndpoints.GET_DOCUMENT_COLOURS}", DocumentPropertiesEndpoints.GetDocumentColours)
                .WithOpenApi()
                .WithName(OriginAPIEndpoints.GET_DOCUMENT_COLOURS)
                .WithDescription("Gets a list of document colours.")
                .Produces<IEnumerable<DocumentColour>?>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapGet($"/{OriginAPIEndpoints.GET_DOCUMENT_COLOUR}/{{id:int}}", DocumentPropertiesEndpoints.GetDocumentColour)
                .WithOpenApi()
                .WithName(OriginAPIEndpoints.GET_DOCUMENT_COLOUR)
                .WithDescription("Gets a document colour for the given id.")
                .Produces<DocumentColour>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapPost($"/{OriginAPIEndpoints.CREATE_DOCUMENT_COLOUR}", DocumentPropertiesEndpoints.CreateDocumentColour)
                .WithOpenApi()
                .WithName(OriginAPIEndpoints.CREATE_DOCUMENT_COLOUR)
                .WithDescription("Create a new document colour.")
                .Produces<DocumentColour>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapPut($"/{OriginAPIEndpoints.UPDATE_DOCUMENT_COLOUR}", DocumentPropertiesEndpoints.UpdateDocumentColour)
                .WithOpenApi()
                .WithName(OriginAPIEndpoints.UPDATE_DOCUMENT_COLOUR)
                .WithDescription("Updates the document colour.")
                .Produces<DocumentColour>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapDelete($"/{OriginAPIEndpoints.DELETE_DOCUMENT_COLOUR}/{{id:int}}", DocumentPropertiesEndpoints.DeleteDocumentColour)
                .WithOpenApi()
                .WithName(OriginAPIEndpoints.DELETE_DOCUMENT_COLOUR)
                .WithDescription("Delete's a document colour of the given id and returns the number of affected rows.")
                .Produces<int>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapPost($"/{OriginAPIEndpoints.GET_ORIGIN_OPTIONS}", OriginOptionsEndpoints.GetOriginOptions)
                .WithOpenApi()
                .WithName(OriginAPIEndpoints.GET_ORIGIN_OPTIONS)
                .WithDescription("Gets origin option items for the specified origin options code")
                .Produces<IEnumerable<OptionItem>?>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapPost($"/{OriginAPIEndpoints.GET_GENERIC_ORIGIN_OPTIONS}", OriginOptionsEndpoints.GetGenericOriginOptions)
                .WithOpenApi()
                .WithName(OriginAPIEndpoints.GET_GENERIC_ORIGIN_OPTIONS)
                .WithDescription("Gets serialized generic origin options list for the specified origin options code")
                .Produces<string>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            return app;
        }
    }
}
