using Origin.Core.Constants;
using Origin.Core.Models;
using Origin.Requests.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Origin.Requests.API
{
    public class OriginDocumentRequests(HttpClient httpClient) : IOriginDocumentRequests
    {
        protected static readonly JsonSerializerOptions _jsonSerializerOptions = new(JsonSerializerDefaults.Web) { ReferenceHandler = ReferenceHandler.IgnoreCycles };
        protected readonly HttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

        public async Task<byte[]> GeneratePdfAsync(DocumentConfig documentConfig)
        {
            ArgumentNullException.ThrowIfNull(documentConfig, nameof(documentConfig));

            using HttpResponseMessage response = await _httpClient.PostAsJsonAsync(OriginAPIEndpoints.GENERATE_PDF, documentConfig, _jsonSerializerOptions)
            .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
        }
    }
}
