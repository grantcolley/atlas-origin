using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using Origin.Core.Extensions;
using Origin.Core.Interfaces;
using Origin.Core.Model;

namespace Origin.Pdf.Extensions
{
    public static class SectionExtensions
    {
        public static void AddFooter(this Section section, DocumentConfig documentConfig)
        {
            ArgumentNullException.ThrowIfNull(documentConfig);

            DocumentParagraph? footerParagraph = documentConfig.GetFooterParagraph();

            if (footerParagraph == null) return;

            HeaderFooter footer = section.Footers.Primary;

            Paragraph paragraph = footer.AddParagraph();

            paragraph.AddContent(footerParagraph, documentConfig);
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
