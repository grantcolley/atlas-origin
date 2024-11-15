using Origin.Core.Extensions;
using Origin.Core.Models;
using Origin.Service.Interface;

namespace Origin.Service.Base
{
    public abstract class DocumentGeneratorBase : IDocumentGenerator
    {
        public abstract DocumentFileExtension DocumentFileExtension { get; }
        public abstract DocumentGeneratorType DocumentGeneratorType { get; }

        protected abstract byte[] GenerateBytes(DocumentConfig documentConfig);

        public byte[] Generate(DocumentConfig documentConfig)
        {
            ArgumentNullException.ThrowIfNull(documentConfig);

            documentConfig.ConstructDocumentConfig();

            if (documentConfig.ApplySubstitutes)
            {
                documentConfig.ApplySubstitutesToDocumentContent();
            }

            return GenerateBytes(documentConfig);
        }
    }
}
