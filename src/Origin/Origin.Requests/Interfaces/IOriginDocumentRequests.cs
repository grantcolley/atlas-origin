using Origin.Core.Models;

namespace Origin.Requests.Interfaces
{
    public interface IOriginDocumentRequests
    {
        Task<byte[]> GeneratePdfAsync(DocumentConfig documentConfig);
    }
}
