using Origin.Core.Model;
using Origin.Service.Interface;

namespace Origin.Service.Base
{
    public abstract class DocumentServiceBase : IDocumentService
    {
        public abstract DocumentFileExtension DocumentExtension { get; }
        public abstract DocumentServiceType DocumentServiceType { get; }

        public abstract bool TryCreateDocument(DocumentConfig documentConfig, string fileName);

        public virtual void ValidateOutputLocation(string? outputLocation)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(outputLocation);

            if (!Directory.Exists(outputLocation))
            {
                _ = Directory.CreateDirectory(outputLocation);
            }
        }

        public virtual void FileDeleteIfExists(string? file)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(file);

            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }
    }
}
