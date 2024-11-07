using Origin.Core.Extensions;
using Origin.Core.Models;
using Origin.Service.Interface;

namespace Origin.Service.Base
{
    public abstract class DocumentServiceBase : IDocumentService
    {
        public abstract DocumentFileExtension DocumentExtension { get; }
        public abstract DocumentServiceType DocumentServiceType { get; }
        public abstract byte[] CreateFile(DocumentConfig documentConfig);

        public byte[] BuildFile(DocumentConfig documentConfig)
        {
            ArgumentNullException.ThrowIfNull(documentConfig);

            documentConfig.ConstructDocumentConfig();

            if (documentConfig.ApplySubstitutes)
            {
                documentConfig.ApplySubstitutesToDocumentContent();
            }

            return CreateFile(documentConfig);
        }
    }
}
