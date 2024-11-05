using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Origin.Core.Models;
using Origin.OpenXml.Extensions;
using Origin.Service.Base;

namespace Origin.OpenXml.Sevices
{
    public class DocXDocumentService : DocumentServiceBase
    {
        public override DocumentFileExtension DocumentExtension => DocumentFileExtension.docx;
        public override DocumentServiceType DocumentServiceType => DocumentServiceType.OpenXmlDocument;

        public override byte[] CreateFile(DocumentConfig documentConfig)
        {
            ArgumentNullException.ThrowIfNull(documentConfig);

            using MemoryStream memoryStream = new();
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(memoryStream, WordprocessingDocumentType.Document, true))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();

                Body body = mainPart.AddBody(documentConfig);

                mainPart.AddResources(documentConfig);

                mainPart.AddFooter(documentConfig);

                foreach (DocumentParagraph? documentParagraph in documentConfig.ConfigParagraphs.Select(cp => cp.DocumentParagraph).Where(p => p.DocumentParagraphType != DocumentParagraphType.Footer))
                {
                    if (documentParagraph != null)
                    {
                        body.AddParagraph(documentParagraph);
                    }
                }

                wordDocument.Save();
            }

            return memoryStream.ToArray();
        }
    }
}
