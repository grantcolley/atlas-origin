using Origin.Service.Interface;

namespace Origin.Service.Extensions
{
    public static class DocumentServiceProviderExtensions
    {
        public static IDocumentServiceProvider AddProviders(this IDocumentServiceProvider documentServiceProvider, IEnumerable<IDocumentService> documentServices)
        {
            ArgumentNullException.ThrowIfNull(documentServices);

            foreach (IDocumentService documentService in documentServices)
            {
                _ = documentServiceProvider.AddDocumentService(documentService);
            }

            return documentServiceProvider;
        }
    }
}
