using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using Origin.Core.Models;
using Origin.Pdf.Extensions;
using Origin.Pdf.Helpers;
using Origin.Service.Base;
using PdfSharp.Fonts;
using PdfSharp.Pdf;

namespace Origin.Pdf.Services
{
    public class PdfDocumentService : DocumentServiceBase
    {
        public override DocumentFileExtension DocumentExtension => DocumentFileExtension.pdf;
        public override DocumentServiceType DocumentServiceType => DocumentServiceType.PdfSharp;

        public override bool TryCreateDocument(DocumentConfig documentConfig, string fileName)
        {
            ArgumentNullException.ThrowIfNull(documentConfig);

            if (string.IsNullOrWhiteSpace(fileName)) throw new NullReferenceException(nameof(fileName));

            GlobalFontSettings.FontResolver = new CustomFontResolver();

            Document document = new();

            Section section = document.CreateSectionProperties(documentConfig);

            section.AddFooter(documentConfig);

            foreach (DocumentParagraph documentParagraph in documentConfig.ConfigParagraphs.Select(cp => cp.DocumentParagraph).Where(p => p.DocumentParagraphType != DocumentParagraphType.Footer))
            {
                section.AddParagraph(documentParagraph);
            }

            using PdfDocument pdfDocument = new();

            var pdfRenderer = new PdfDocumentRenderer
            {
                Document = document,
                PdfDocument = pdfDocument
            };

            pdfRenderer.RenderDocument();

            pdfRenderer.PdfDocument.Save(fileName);

            return true;
        }
    }
}
