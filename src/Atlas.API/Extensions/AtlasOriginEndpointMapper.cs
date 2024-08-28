using Atlas.API.Endpoints;
using Atlas.Core.Constants;
using Atlas.Core.Models;

namespace Atlas.API.Extensions
{
    internal static class AtlasOriginEndpointMapper
    {
        internal static WebApplication? MapAtlasOriginEndpoints(this WebApplication app)
        {
            app.MapGet($"/{AtlasAPIEndpoints.GET_DOCUMENT_CONFIGS}", DocumentEndpoints.GetDocumentConfigs)
                .WithOpenApi()
                .WithName(AtlasAPIEndpoints.GET_DOCUMENT_CONFIGS)
                .WithDescription("Gets a list of document configurations.")
                .Produces<IEnumerable<Module>?>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapGet($"/{AtlasAPIEndpoints.GET_DOCUMENT_CONFIG}/{{id:int}}", DocumentEndpoints.GetDocumentConfig)
                .WithOpenApi()
                .WithName(AtlasAPIEndpoints.GET_DOCUMENT_CONFIG)
                .WithDescription("Gets a document configuration for the given id. If id is 0 then returns a new instance of a blank document configuration for creation.")
                .Produces<Module>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapPost($"/{AtlasAPIEndpoints.CREATE_DOCUMENT_CONFIG}", DocumentEndpoints.CreateDocumentConfig)
                .WithOpenApi()
                .WithName(AtlasAPIEndpoints.CREATE_DOCUMENT_CONFIG)
                .WithDescription("Create a new document configuration.")
                .Produces<Module>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapPut($"/{AtlasAPIEndpoints.UPDATE_DOCUMENT_CONFIG}", DocumentEndpoints.UpdateDocumentConfig)
                .WithOpenApi()
                .WithName(AtlasAPIEndpoints.UPDATE_DOCUMENT_CONFIG)
                .WithDescription("Updates the document configuration.")
                .Produces<Module>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapDelete($"/{AtlasAPIEndpoints.DELETE_DOCUMENT_CONFIG}/{{id:int}}", DocumentEndpoints.DeleteDocumentConfig)
                .WithOpenApi()
                .WithName(AtlasAPIEndpoints.DELETE_DOCUMENT_CONFIG)
                .WithDescription("Delete's a document configuration of the given id.")
                .Produces<Module>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            return app;
        }
    }
}
