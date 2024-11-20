using Atlas.Requests.Base;
using Atlas.Requests.Interfaces;
using Origin.Core.Constants;
using Origin.Core.Models;
using Origin.Requests.Interfaces;
using System.Net;
using System.Net.Http.Json;

namespace Origin.Requests.API
{
    public class OriginDocumentRequests(HttpClient httpClient) : RequestBase(httpClient), IOriginDocumentRequests
    {
        public async Task<IResponse<Document>> GetCustomerProductDocumentAsync(int productId)
        {
            if(productId <= 0)
            {
                throw new ArgumentException($"Invalid {nameof(productId)} {productId}");
            }

            using HttpResponseMessage httpResponseMessage 
                = await _httpClient.GetAsync($"{OriginAPIEndpoints.GET_CUSTOMER_PRODUCT_DOCUMENT}/{productId}")
                .ConfigureAwait(false);

            return await GetResponseAsync<Document>(httpResponseMessage)
                .ConfigureAwait(false);
        }

        public async Task<byte[]> GeneratePdfAsync(DocumentConfig documentConfig)
        {
            ArgumentNullException.ThrowIfNull(documentConfig, nameof(documentConfig));

            using HttpResponseMessage response 
                = await _httpClient.PostAsJsonAsync(OriginAPIEndpoints.GENERATE_PDF, documentConfig, _jsonSerializerOptions)
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
        }
    }
}
