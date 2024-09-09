using Atlas.Requests.API;
using Origin.Core.Constants;
using Origin.Requests.Interfaces;

namespace Origin.Requests.API
{
    public class OriginOptionsRequests(HttpClient httpClient) : OptionsRequests(httpClient), IOriginOptionsRequests
    {
        public override string GetOptionsEndpoint { get; } = OriginAPIEndpoints.GET_ORIGIN_OPTIONS;
        public override string GetGenericOptionsEndpoint { get; } = OriginAPIEndpoints.GET_GENERIC_ORIGIN_OPTIONS;
    }
}
