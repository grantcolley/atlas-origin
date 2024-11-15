using Origin.Core.Models;
using Origin.Service.Base;
using Origin.Service.Interface;

namespace Origin.Service.Services
{
    public class DocumentPackageService(IDocumentGeneratorProvider documentGeneratorProvider) 
        : DocumentServiceBase<string>(documentGeneratorProvider)
    {
        public override Task<string> ExecuteAsync(Document document, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override Task ExecuteAsync(IEnumerable<Document> documents, ParallelOptions parallelOptions, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
