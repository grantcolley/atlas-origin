using Atlas.Requests.Interfaces;
using Origin.Core.Models;

namespace Origin.Requests.Interfaces
{
    public interface IOriginDocumentRequests
    {
        Task<IResponse<Document>> GetCustomerDocumentAsync(int customerId);
        Task<byte[]> GeneratePdfAsync(DocumentConfig documentConfig);
    }
}
