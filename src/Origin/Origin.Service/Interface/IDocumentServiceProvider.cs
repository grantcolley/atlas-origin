using Origin.Core.Models;

namespace Origin.Service.Interface
{
    public interface IDocumentServiceProvider
    {
        IDocumentServiceProvider AddDocumentService(IDocumentService documentService);
        IDocumentService GetDocumentService(DocumentServiceType documentServiceType);
    }
}
