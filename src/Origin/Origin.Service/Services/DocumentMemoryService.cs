using Origin.Core.Models;
using Origin.Service.Base;
using Origin.Service.Interface;

namespace Origin.Service.Services
{
    public class DocumentMemoryService(IDocumentGeneratorProvider documentGeneratorProvider) 
        : DocumentServiceBase<byte[]>(documentGeneratorProvider)
    {
        public override Task<byte[]> ExecuteAsync(Document document, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(document);
            ArgumentNullException.ThrowIfNull(document.Config);

            IDocumentGenerator documentGenerator = _documentGeneratorProvider.GetDocumentGenerator(document.DocumentGeneratorType);

            byte[] bytes = documentGenerator.Generate(document.Config);

            return Task.FromResult(bytes);
        }
    }
}
