using Atlas.Data.Context;
using Origin.Core.Models;

namespace Origin.Data.Access.Interfaces
{
    public interface IDocumentPropertiesData : IAuthorisationData
    {
        Task<IEnumerable<DocumentFont>> GetDocumentFontsAsync(CancellationToken cancellationToken);
        Task<DocumentFont?> GetDocumentFontAsync(int id, CancellationToken cancellationToken);
        Task<DocumentFont> CreateDocumentFontAsync(DocumentFont documentFont, CancellationToken cancellationToken);
        Task<DocumentFont> UpdateDocumentFontAsync(DocumentFont documentFont, CancellationToken cancellationToken);
        Task<int> DeleteDocumentFontAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<DocumentColour>> GetDocumentColoursAsync(CancellationToken cancellationToken);
        Task<DocumentColour?> GetDocumentColourAsync(int id, CancellationToken cancellationToken);
        Task<DocumentColour> CreateDocumentColourAsync(DocumentColour documentColour, CancellationToken cancellationToken);
        Task<DocumentColour> UpdateDocumentColourAsync(DocumentColour documentColour, CancellationToken cancellationToken);
        Task<int> DeleteDocumentColourAsync(int id, CancellationToken cancellationToken);
    }
}
