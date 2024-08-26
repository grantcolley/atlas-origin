using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Origin.Converters;
using Origin.Extensions;
using Origin.Interfaces;
using Origin.Model;

namespace Origin.OpenXml.Extensions
{
    public static class MainPartExtensions
    {

        public static Body AddBody(this MainDocumentPart mainPart, IDocumentProperties? documentProperties)
        {
            ArgumentNullException.ThrowIfNull(documentProperties);

            mainPart.Document = new Document();

            Body body = mainPart.Document.AppendChild(new Body());

            PageMargin margin = new()
            {
                Top = Int32Value.FromInt32(documentProperties.PageMarginTop.ToTwips()),
                Left = UInt32Value.FromUInt32(documentProperties.PageMarginLeft.ToUTwips()),
                Right = UInt32Value.FromUInt32(documentProperties.PageMarginRight.ToUTwips()),
                Bottom = Int32Value.FromInt32(documentProperties.PageMarginTop.ToTwips())
            };

            SectionProperties sectionProperties = new();
            sectionProperties.Append(margin);

            body.Append(sectionProperties);

            return body;
        }

        public static void AddFooter(this MainDocumentPart mainPart, DocumentConfig documentArgs)
        {
            ArgumentNullException.ThrowIfNull(documentArgs);

            DocumentParagraph? footerParagraph = documentArgs.GetFooterParagraph();

            if (footerParagraph == null) return;

            FooterPart? footerPart = mainPart.AddNewPart<FooterPart>();

            if (footerPart == null) throw new NullReferenceException(nameof(footerPart));

            string footerPartId = mainPart.GetIdOfPart(footerPart);

            Footer footer = new();

            Paragraph p = footer.AppendChild(new Paragraph());

            p.AddContent(footerParagraph, documentArgs);

            footerPart.Footer = footer;

            IEnumerable<SectionProperties>? sections = mainPart.Document.Body?.Elements<SectionProperties>();

            if (sections != null)
            {
                foreach (var section in sections)
                {
                    section.RemoveAllChildren<FooterReference>();

                    section.PrependChild<FooterReference>(new FooterReference() { Id = footerPartId });
                }
            }
        }

        public static void AddResources(this MainDocumentPart mainPart, DocumentConfig documentArgs)
        {
            ArgumentNullException.ThrowIfNull(documentArgs);

            List<DocumentContent> images = documentArgs.GetImages();
            
            foreach (DocumentContent image in images)
            {
                mainPart.AddImage(image);
            }
        }
    }
}
