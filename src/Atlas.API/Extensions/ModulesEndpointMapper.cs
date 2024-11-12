using Atlas.API.Extensions.Commercial;
using Atlas.API.Extensions.Origin;

namespace Atlas.API.Extensions
{
    internal static class ModulesEndpointMapper
    {
        internal static WebApplication? MapAtlasModulesEndpoints(this WebApplication app)
        {
            app.MapAtlasOriginEndpoints();
            app.MapAtlasCommercialEndpoints();

            return app;
        }
    }
}
