using Origin.Core.Models;

namespace Origin.Service.Interface
{
    public interface IDocumentGeneratorProvider
    {
        IDocumentGenerator GetDocumentGenerator(DocumentGeneratorType documentGeneratorType);
    }
}
