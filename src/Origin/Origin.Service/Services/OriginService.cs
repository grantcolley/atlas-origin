using Origin.Extensions;
using Origin.Model;
using Origin.Service.Interface;

namespace Origin.Service.Services
{
    public class OriginService(IDocumentServiceProvider documentServiceProvider) : IOriginService
    {
        private readonly IDocumentServiceProvider _documentServiceProvider = documentServiceProvider ?? throw new ArgumentNullException(nameof(documentServiceProvider));

        public bool TryCreate(DocumentConfig documentArgs, out string fullFilename)
        {
            ArgumentNullException.ThrowIfNull(documentArgs);

            IDocumentService documentService = _documentServiceProvider.GetDocumentService(documentArgs.DocumentServiceType);

            fullFilename = $"{documentArgs.FullFilename()}.{documentService.DocumentExtension}";

            documentService.ValidateOutputLocation(documentArgs.OutputLocation);

            documentService.FileDeleteIfExists(fullFilename);

            documentArgs.BuildDocument();

            return documentService.TryCreateDocument(documentArgs, fullFilename);
        }
    }
}
