using Origin.Core.Extensions;
using Origin.Core.Models;
using Origin.Service.Base;
using Origin.Service.Interface;

namespace Origin.Service.Services
{
    public class DocumentWriterService(IDocumentGeneratorProvider documentGeneratorProvider) 
        : DocumentServiceBase<string>(documentGeneratorProvider)
    {
        public override async Task<string> ExecuteAsync(Document document, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(document);
            ArgumentNullException.ThrowIfNull(document.Config);

            if (string.IsNullOrWhiteSpace(document.Target)) throw new NullReferenceException(nameof(document.Target));

            IDocumentGenerator documentGenerator = _documentGeneratorProvider.GetDocumentGenerator(document.DocumentGeneratorType);

            string fullFilename = Path.Combine(document.Target, document.Filename($"{documentGenerator.DocumentFileExtension}"));
            
            if (File.Exists(fullFilename))
            {
                File.Delete(fullFilename);
            }

            byte[] bytes = documentGenerator.Generate(document.Config);

            await File.WriteAllBytesAsync(fullFilename, bytes, cancellationToken).ConfigureAwait(false);

            return fullFilename;
        }
    }
}
