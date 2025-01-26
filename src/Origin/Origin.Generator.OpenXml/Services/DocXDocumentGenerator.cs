using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Origin.Core.Models;
using Origin.Generator.OpenXml.Extensions;
using Origin.Service.Base;

namespace Origin.Generator.OpenXml.Services
{
    public class DocXDocumentGenerator : DocumentGeneratorBase
    {
        public override DocumentFileExtension DocumentFileExtension => DocumentFileExtension.docx;
        public override DocumentGeneratorType DocumentGeneratorType => DocumentGeneratorType.OpenXmlDocument;

        protected override byte[] GenerateBytes(DocumentConfig documentConfig)
        {
            ArgumentNullException.ThrowIfNull(documentConfig);

            using MemoryStream memoryStream = new();
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(memoryStream, WordprocessingDocumentType.Document, true))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();

                Body body = mainPart.AddBody(documentConfig);

                mainPart.AddResources(documentConfig);

                mainPart.AddFooter(documentConfig);

                foreach (DocumentParagraph? documentParagraph in documentConfig.ConfigParagraphs.Select(cp => cp.DocumentParagraph).Where(p => p != null && p.DocumentParagraphType != DocumentParagraphType.Footer))
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
