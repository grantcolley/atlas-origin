using Atlas.Requests.Interfaces;
using Origin.Core.Models;

namespace Origin.Requests.Interfaces
{
    public interface IOriginDocumentRequests
    {
        Task<IResponse<Document>> GetCustomerProductDocumentAsync(int customerId);
        Task<byte[]> GeneratePdfAsync(DocumentConfig documentConfig);
    }
}
