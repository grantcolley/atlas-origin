using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Origin.Core.Model;
using Origin.OpenXml.Extensions;
using Origin.Service.Base;

namespace Origin.OpenXml.Sevices
{
    public class DocXDocumentService : DocumentServiceBase
    {
        public override DocumentFileExtension DocumentExtension => DocumentFileExtension.docx;
        public override DocumentServiceType DocumentServiceType => DocumentServiceType.OpenXmlDocument;

        public override bool TryCreateDocument(DocumentConfig documentConfig, string fileName)
        {
            ArgumentNullException.ThrowIfNull(documentConfig);
            
            if (string.IsNullOrWhiteSpace(fileName)) throw new NullReferenceException(nameof(fileName));

            using WordprocessingDocument wordDocument = WordprocessingDocument.Create(fileName, WordprocessingDocumentType.Document);

            MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();

            Body body = mainPart.AddBody(documentConfig);

            mainPart.AddResources(documentConfig);

            mainPart.AddFooter(documentConfig);

            foreach(DocumentParagraph documentParagraph  in documentConfig.Paragraphs.Where(p => p.DocumentParagraphType != DocumentParagraphType.Footer))
            {
                body.AddParagraph(documentParagraph, documentConfig);
            }

            return true;
        }
    }
}
