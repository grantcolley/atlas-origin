using Origin.Core.Models;
using Origin.Service.Base;

namespace Origin.Tests.Helpers
{
    public class TestDocxDocumentService : DocumentServiceBase
    {
        public override DocumentFileExtension DocumentExtension => DocumentFileExtension.docx;
        public override DocumentServiceType DocumentServiceType => DocumentServiceType.OpenXmlDocument;

        public override bool TryCreateDocument(DocumentConfig? documentArgs, string fileName)
        {
            return true;
        }

        public override void FileDeleteIfExists(string? file)
        {
            return;
        }

        public override void ValidateOutputLocation(string? outputLocation)
        {
            return;
        }
    }
}
