using Origin.Model;

namespace Origin.Service.Interface
{
    public interface IDocumentService
    {
        DocumentFileExtension DocumentExtension { get; }
        DocumentServiceType DocumentServiceType { get; }
        bool TryCreateDocument(DocumentConfig documentArgs, string fileName);
        void ValidateOutputLocation(string? outputLocation);
        void FileDeleteIfExists(string? file);
    }
}
