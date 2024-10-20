using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using Origin.Core.Extensions;
using Origin.Core.Models;

namespace Origin.PdfSharp.Extensions
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

            paragraph.AddContent(footerParagraph);
        }

        public static void AddTable(this Section section, DocumentParagraph documentParagraph)
        {
            ArgumentNullException.ThrowIfNull(documentParagraph);

            Table tbl = section.AddTable();

            tbl.ConfigureTable(documentParagraph);
        }

        public static void AddParagraph(this Section section, DocumentParagraph documentParagraph)
        {
            ArgumentNullException.ThrowIfNull(documentParagraph);

            if (documentParagraph.DocumentParagraphType == DocumentParagraphType.Table)
            {
                section.AddTable(documentParagraph);
            }
            else
            {
                Paragraph paragraph = section.AddParagraph();

                paragraph.AddContent(documentParagraph);
            }
        }
    }
}
