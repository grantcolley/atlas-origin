using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using Origin.Core.Models;
using Origin.PdfSharp.Extensions;
using Origin.Service.Base;
using PdfSharp.Fonts;
using PdfSharp.Pdf;

namespace Origin.PdfSharp.Services
{
    public class PdfDocumentGenerator : DocumentGeneratorBase
    {
        public override DocumentFileExtension DocumentFileExtension => DocumentFileExtension.pdf;
        public override DocumentGeneratorType DocumentGeneratorType => DocumentGeneratorType.PdfSharp;

        protected override byte[] GenerateBytes(DocumentConfig documentConfig)
        {
            ArgumentNullException.ThrowIfNull(documentConfig);

            GlobalFontSettings.FontResolver = new CustomFontResolver();

            MigraDoc.DocumentObjectModel.Document document = new();

            Section section = document.CreateSectionProperties(documentConfig);

            section.AddFooter(documentConfig);

            foreach (DocumentParagraph? documentParagraph in documentConfig.ConfigParagraphs.OrderBy(cp => cp.Order).Select(cp => cp.DocumentParagraph).Where(p => p.DocumentParagraphType != DocumentParagraphType.Footer))
            {
                if (documentParagraph != null)
                {
                    section.AddParagraph(documentParagraph);
                }
            }

            using MemoryStream memoryStream = new();
            using PdfDocument pdfDocument = new();
            {
                var pdfRenderer = new PdfDocumentRenderer
                {
                    Document = document,
                    PdfDocument = pdfDocument
                };

                pdfRenderer.RenderDocument();

                pdfRenderer.PdfDocument.Save(memoryStream);
            }

            return memoryStream.ToArray();
        }
    }
}
