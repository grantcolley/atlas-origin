using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Origin.Model;
using Origin.OpenXml.Extensions;
using Origin.Service.Base;

namespace Origin.OpenXml.DocX.Sevices
{
    public class DocXDocumentService : DocumentServiceBase
    {
        public override DocumentFileExtension DocumentExtension => DocumentFileExtension.docx;
        public override DocumentServiceType DocumentServiceType => DocumentServiceType.OpenXmlDocument;

        public override bool TryCreateDocument(DocumentConfig documentArgs, string fileName)
        {
            ArgumentNullException.ThrowIfNull(documentArgs);
            
            if (string.IsNullOrWhiteSpace(fileName)) throw new NullReferenceException(nameof(fileName));

            using WordprocessingDocument wordDocument = WordprocessingDocument.Create(fileName, WordprocessingDocumentType.Document);

            MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();

            Body body = mainPart.AddBody(documentArgs);

            mainPart.AddResources(documentArgs);

            mainPart.AddFooter(documentArgs);

            foreach(DocumentParagraph documentParagraph  in documentArgs.Paragraphs.Where(p => p.DocumentParagraphType != DocumentParagraphType.Footer))
            {
                body.AddParagraph(documentParagraph, documentArgs);
            }

            return true;
        }
    }
}
