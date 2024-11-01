using Origin.Core.Models;

namespace Origin.Core.Extensions
{
    public static class DocumentParagraphExtensions
    {
        /// <summary>
        /// Takes a <see cref="DocumentParagraph"/> and sets it's name to null and it's <see cref="DocumentParagraph.DocumentParagraphId"/> to zero.
        /// It also rests all id's for its <see cref="DocumentTableCell"/>'s, <see cref="DocumentTableColumn"/>'s, 
        /// <see cref="DocumentTableRow"/>'s, and <see cref="DocumentContent"/>'s.
        /// </summary>
        /// <param name="documentParagraph"></param>
        public static void ResetIds(this DocumentParagraph documentParagraph)
        {
            documentParagraph.DocumentParagraphId = 0;
            documentParagraph.Name = null;

            foreach (DocumentContent documentContent in documentParagraph.Contents)
            {
                documentContent.DocumentContentId = 0;
            }

            foreach (DocumentTableColumn documentTableColumn in documentParagraph.Columns)
            {
                documentTableColumn.DocumentTableColumnId = 0;
            }

            foreach (DocumentTableRow documentTableRow in documentParagraph.Rows)
            {
                documentTableRow.DocumentTableRowId = 0;
            }

            foreach (DocumentTableCell documentTableCell in documentParagraph.Cells)
            {
                documentTableCell.DocumentTableCellId = 0;
            }
        }
    }
}
