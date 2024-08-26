using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using Origin.Model;
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

        public override bool TryCreateDocument(DocumentConfig documentArgs, string fileName)
        {
            ArgumentNullException.ThrowIfNull(documentArgs);

            if (string.IsNullOrWhiteSpace(fileName)) throw new NullReferenceException(nameof(fileName));

            GlobalFontSettings.FontResolver = new CustomFontResolver();

            Document document = new();

            Section section = document.CreateSectionProperties(documentArgs);

            section.AddFooter(documentArgs);

            foreach (DocumentParagraph documentParagraph in documentArgs.Paragraphs.Where(p => p.DocumentParagraphType != DocumentParagraphType.Footer))
            {
                section.AddParagraph(documentParagraph, documentArgs);
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
