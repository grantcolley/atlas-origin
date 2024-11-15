using Origin.Core.Models;

namespace Origin.Service.Interface
{
    public interface IDocumentService<T>
    {
        Task<T> ExecuteAsync(Document document, CancellationToken cancellationToken);
        Task ExecuteAsync(IEnumerable<Document> documents, ParallelOptions parallelOptions, CancellationToken cancellationToken);
    }
}