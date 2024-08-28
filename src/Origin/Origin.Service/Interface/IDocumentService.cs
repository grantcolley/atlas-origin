using Origin.Core.Models;

namespace Origin.Service.Interface
{
    public interface IDocumentService
    {
        DocumentFileExtension DocumentExtension { get; }
        DocumentServiceType DocumentServiceType { get; }
        bool TryCreateDocument(DocumentConfig documentConfig, string fileName);
        void ValidateOutputLocation(string? outputLocation);
        void FileDeleteIfExists(string? file);
    }
}
