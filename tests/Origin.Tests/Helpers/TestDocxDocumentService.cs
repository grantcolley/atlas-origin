using Origin.Core.Models;
using Origin.Service.Base;

namespace Origin.Tests.Helpers
{
    public class TestDocxDocumentService : DocumentGeneratorBase
    {
        public override DocumentFileExtension DocumentFileExtension => DocumentFileExtension.docx;
        public override DocumentGeneratorType DocumentGeneratorType => DocumentGeneratorType.OpenXmlDocument;

        protected override byte[] GenerateBytes(DocumentConfig documentConfig)
        {
            throw new NotImplementedException();
        }
    }
}
