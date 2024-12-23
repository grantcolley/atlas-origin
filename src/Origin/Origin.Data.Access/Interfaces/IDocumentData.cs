using Atlas.Data.Access.Interfaces;
using Origin.Core.Models;

namespace Origin.Data.Access.Interfaces
{
    public interface IDocumentData : IAuthorisationData
    {
        Task<IEnumerable<DocumentConfig>> GetDocumentConfigsAsync(CancellationToken cancellationToken);
        Task<DocumentConfig?> GetDocumentConfigAsync(int id, CancellationToken cancellationToken);
        Task<DocumentConfig> CreateDocumentConfigAsync(DocumentConfig documentConfig, CancellationToken cancellationToken);
        Task<DocumentConfig> UpdateDocumentConfigAsync(DocumentConfig documentConfig, CancellationToken cancellationToken);
        Task<int> DeleteDocumentConfigAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<DocumentParagraph>> GetDocumentParagraphsAsync(CancellationToken cancellationToken);
        Task<DocumentParagraph?> GetDocumentParagraphAsync(int id, CancellationToken cancellationToken);
        Task<DocumentParagraph> CreateDocumentParagraphAsync(DocumentParagraph documentParagraph, CancellationToken cancellationToken);
        Task<DocumentParagraph> UpdateDocumentParagraphAsync(DocumentParagraph documentParagraph, CancellationToken cancellationToken);
        Task<int> DeleteDocumentParagraphAsync(int id, CancellationToken cancellationToken);
    }
}
