using Origin.Core.Model;
using Origin.Service.Interface;

namespace Origin.Service.Providers
{
    public class DocumentServiceProvider : IDocumentServiceProvider
    {
        private readonly Dictionary<DocumentServiceType, IDocumentService> _documentServiceCache = [];

        public DocumentServiceProvider() { }

        public DocumentServiceProvider(IDocumentService[] documentServices) 
        {
            ArgumentNullException.ThrowIfNull(documentServices);

            foreach (IDocumentService documentService in documentServices) 
            {
                _documentServiceCache.Add(documentService.DocumentServiceType, documentService);
            }
        }

        public IDocumentServiceProvider AddDocumentService(IDocumentService documentService)
        {
            ArgumentNullException.ThrowIfNull(documentService);

            _documentServiceCache.Add(documentService.DocumentServiceType, documentService);

            return this;
        }

        public IDocumentService GetDocumentService(DocumentServiceType documentServiceType)
        {
            if (documentServiceType == DocumentServiceType.None) throw new NotSupportedException($"{DocumentServiceType.None}");

            return _documentServiceCache[documentServiceType];
        }
    }
}
