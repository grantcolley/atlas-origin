using Origin.Core.Models;

namespace Origin.Core.Extensions
{
    public static class DocumentParagraphExtensions
    {
        public static void Clone(this DocumentParagraph documentParagraph)
        {
            documentParagraph.DocumentParagraphId = 0;
            documentParagraph.Name = null;
            documentParagraph.CreatedBy = null;
            documentParagraph.CreatedDate = null;
            documentParagraph.ModifiedBy = null;
            documentParagraph.ModifiedDate = null;

            foreach (DocumentContent documentContent in documentParagraph.Contents)
            {
                documentContent.DocumentContentId = 0;
                documentContent.CreatedBy = null;
                documentContent.CreatedDate = null;
                documentContent.ModifiedBy = null;
                documentContent.ModifiedDate = null;
            }

            foreach (DocumentTableColumn documentTableColumn in documentParagraph.Columns)
            {
                documentTableColumn.DocumentTableColumnId = 0;
                documentTableColumn.CreatedBy = null;
                documentTableColumn.CreatedDate = null;
                documentTableColumn.ModifiedBy = null;
                documentTableColumn.ModifiedDate = null;
            }

            foreach (DocumentTableRow documentTableRow in documentParagraph.Rows)
            {
                documentTableRow.DocumentTableRowId = 0;
                documentTableRow.CreatedBy = null;
                documentTableRow.CreatedDate = null;
                documentTableRow.ModifiedBy = null;
                documentTableRow.ModifiedDate = null;
            }

            foreach (DocumentTableCell documentTableCell in documentParagraph.Cells)
            {
                documentTableCell.DocumentTableCellId = 0;
                documentTableCell.CreatedBy = null;
                documentTableCell.CreatedDate = null;
                documentTableCell.ModifiedBy = null;
                documentTableCell.ModifiedDate = null;
            }
        }
    }
}