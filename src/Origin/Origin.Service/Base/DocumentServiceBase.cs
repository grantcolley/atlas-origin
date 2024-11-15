using Origin.Core.Models;
using Origin.Service.Interface;

namespace Origin.Service.Base
{
    public abstract class DocumentServiceBase<T>(IDocumentGeneratorProvider documentGeneratorProvider) : IDocumentService<T>
    {
        protected readonly IDocumentGeneratorProvider _documentGeneratorProvider = documentGeneratorProvider ?? throw new ArgumentNullException(nameof(documentGeneratorProvider));

        public abstract Task<T> ExecuteAsync(Document document, CancellationToken cancellationToken);

        public virtual async Task ExecuteAsync(IEnumerable<Document> documents, ParallelOptions parallelOptions, CancellationToken cancellationToken)
        {
            await Parallel.ForEachAsync(documents, parallelOptions, async (document, token) =>
            {
                _ = await ExecuteAsync(document, token).ConfigureAwait(false);
            });
        }
    }
}
