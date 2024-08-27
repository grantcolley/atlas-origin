using Origin.Core.Extensions;
using Origin.Core.Model;
using Origin.Service.Interface;

namespace Origin.Service.Services
{
    public class OriginService(IDocumentServiceProvider documentServiceProvider) : IOriginService
    {
        private readonly IDocumentServiceProvider _documentServiceProvider = documentServiceProvider ?? throw new ArgumentNullException(nameof(documentServiceProvider));

        public bool TryCreate(DocumentConfig documentConfig, out string fullFilename)
        {
            ArgumentNullException.ThrowIfNull(documentConfig);

            IDocumentService documentService = _documentServiceProvider.GetDocumentService(documentConfig.DocumentServiceType);

            fullFilename = $"{documentConfig.FullFilename()}.{documentService.DocumentExtension}";

            documentService.ValidateOutputLocation(documentConfig.OutputLocation);

            documentService.FileDeleteIfExists(fullFilename);

            documentConfig.BuildDocument();

            return documentService.TryCreateDocument(documentConfig, fullFilename);
        }
    }
}
