using Origin.Core.Models;

namespace Origin.Service.Interface
{
    public interface IDocumentService
    {
        DocumentFileExtension DocumentExtension { get; }
        DocumentServiceType DocumentServiceType { get; }
        byte[] CreateFile(DocumentConfig documentConfig);
        byte[] BuildFile(DocumentConfig documentConfig);
    }
}
