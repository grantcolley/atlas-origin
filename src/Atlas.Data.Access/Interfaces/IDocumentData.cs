using Origin.Core.Models;

namespace Atlas.Data.Access.Interfaces
{
    public interface IDocumentData : IAuthorisationData
    {
        Task<IEnumerable<DocumentConfig>> GetDocumentConfigsAsync(CancellationToken cancellationToken);
        Task<DocumentConfig?> GetDocumentConfigAsync(int id, CancellationToken cancellationToken);
        Task<DocumentConfig> CreateDocumentConfigAsync(DocumentConfig documentConfig, CancellationToken cancellationToken);
        Task<DocumentConfig> UpdateDocumentConfigAsync(DocumentConfig documentConfig, CancellationToken cancellationToken);
        Task<int> DeleteDocumentConfigAsync(int id, CancellationToken cancellationToken);
    }
}
