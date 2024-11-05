using Origin.Core.Models;
using Origin.Service.Base;

namespace Origin.Tests.Helpers
{
    public class TestDocxDocumentService : DocumentServiceBase
    {
        public override DocumentFileExtension DocumentExtension => DocumentFileExtension.docx;
        public override DocumentServiceType DocumentServiceType => DocumentServiceType.OpenXmlDocument;

        public override byte[] CreateFile(DocumentConfig documentConfig)
        {
            throw new NotImplementedException();
        }
    }
}
