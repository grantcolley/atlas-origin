using Origin.Core.Extensions;
using Origin.Core.Models;
using Origin.Service.Interface;

namespace Origin.Service.Services
{
    public class OriginService(IDocumentServiceProvider documentServiceProvider) : IOriginService
    {
        private readonly IDocumentServiceProvider _documentServiceProvider = documentServiceProvider ?? throw new ArgumentNullException(nameof(documentServiceProvider));

        public void CreateFile(DocumentConfig documentConfig, out string fullFilename)
        {
            ArgumentNullException.ThrowIfNull(documentConfig);

            if (string.IsNullOrWhiteSpace(documentConfig.OutputLocation)) throw new NullReferenceException(nameof(documentConfig.OutputLocation));

            IDocumentService documentService = _documentServiceProvider.GetDocumentService(documentConfig.DocumentServiceType);

            fullFilename = $"{documentConfig.FullFilename()}.{documentService.DocumentExtension}";

            if (!Directory.Exists(documentConfig.OutputLocation))
            {
                _ = Directory.CreateDirectory(documentConfig.OutputLocation);
            }

            if (File.Exists(fullFilename))
            {
                File.Delete(fullFilename);
            }

            byte[] bytes = documentService.BuildFile(documentConfig);

            File.WriteAllBytes(fullFilename, bytes);
        }
    }
}
