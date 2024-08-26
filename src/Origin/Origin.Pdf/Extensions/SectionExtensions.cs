using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using Origin.Extensions;
using Origin.Interfaces;
using Origin.Model;

namespace Origin.Pdf.Extensions
{
    public static class SectionExtensions
    {
        public static void AddFooter(this Section section, DocumentConfig documentArgs)
        {
            ArgumentNullException.ThrowIfNull(documentArgs);

            DocumentParagraph? footerParagraph = documentArgs.GetFooterParagraph();

            if (footerParagraph == null) return;

            HeaderFooter footer = section.Footers.Primary;

            Paragraph paragraph = footer.AddParagraph();

            paragraph.AddContent(footerParagraph, documentArgs);
        }

        public static void AddTable(this Section section, DocumentParagraph documentParagraph, IDocumentProperties documentProperties)
        {
            ArgumentNullException.ThrowIfNull(documentParagraph);

            Table tbl = section.AddTable();

            tbl.ConfigureTable(documentParagraph, documentProperties);
        }

        public static void AddParagraph(this Section section, DocumentParagraph documentParagraph, IDocumentProperties documentProperties)
        {
            ArgumentNullException.ThrowIfNull(documentParagraph);
            ArgumentNullException.ThrowIfNull(documentProperties);

            if (documentParagraph.DocumentParagraphType == DocumentParagraphType.Table)
            {
                section.AddTable(documentParagraph, documentProperties);
            }
            else
            {
                Paragraph paragraph = section.AddParagraph();

                paragraph.AddContent(documentParagraph, documentProperties);
            }
        }
    }
}
