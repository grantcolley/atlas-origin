using Origin.Core.Models;

namespace Origin.Service.Interface
{
    public interface IDocumentGenerator
    {
        DocumentFileExtension DocumentFileExtension { get; }
        DocumentGeneratorType DocumentGeneratorType { get; }
        byte[] Generate(DocumentConfig documentConfig);
    }
}
