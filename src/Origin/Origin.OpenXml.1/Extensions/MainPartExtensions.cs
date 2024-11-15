using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Origin.Core.Converters;
using Origin.Core.Extensions;
using Origin.Core.Interfaces;
using Origin.Core.Models;

namespace Origin.OpenXml.Extensions
{
    public static class MainPartExtensions
    {
        public static Body AddBody(this MainDocumentPart mainPart, IDocumentProperties? documentProperties)
        {
            ArgumentNullException.ThrowIfNull(documentProperties);

            mainPart.Document = new DocumentFormat.OpenXml.Wordprocessing.Document();

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

        public static void AddFooter(this MainDocumentPart mainPart, DocumentConfig documentConfig)
        {
            ArgumentNullException.ThrowIfNull(documentConfig);

            DocumentParagraph? footerParagraph = documentConfig.GetFooterParagraph();

            if (footerParagraph == null) return;

            FooterPart? footerPart = mainPart.AddNewPart<FooterPart>();

            if (footerPart == null) throw new NullReferenceException(nameof(footerPart));

            string footerPartId = mainPart.GetIdOfPart(footerPart);

            Footer footer = new();

            Paragraph p = footer.AppendChild(new Paragraph());

            p.AddContent(footerParagraph);

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

        public static void AddResources(this MainDocumentPart mainPart, DocumentConfig documentConfig)
        {
            ArgumentNullException.ThrowIfNull(documentConfig);

            List<DocumentContent> images 
                = documentConfig.ConfigParagraphs
                                .Select(cp => cp.DocumentParagraph)
                                .SelectMany(p => p.Contents)
                                .Where(c => c.ContentType == DocumentContentType.Image)
                                .ToList();
            
            foreach (DocumentContent image in images)
            {
                mainPart.AddImage(image);
            }
        }
    }
}
