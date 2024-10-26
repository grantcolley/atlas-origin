using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Origin.Core.Models
{
    public class DocumentParagraph : DocumentParagraphProperties
    {
        private DocumentParagraphType _documentParagraphType;

        public DocumentParagraph() 
        {
            Rows = [];
            Columns = [];
            Cells = [];
            Contents = [];
        }

        public int DocumentParagraphId { get; set; }
        public List<DocumentTableRow> Rows { get; set; }
        public List<DocumentTableColumn> Columns { get; set; }
        public List<DocumentTableCell> Cells { get; set; }
        public List<DocumentContent> Contents { get; set; }

        [Required]
        [StringLength(250)]
        public string? Name { get; set; }

        public DocumentParagraphType DocumentParagraphType 
        {
            get { return _documentParagraphType; }
            set
            {
                if(_documentParagraphType != value)
                {
                    _documentParagraphType = value;

                    if (_documentParagraphType != DocumentParagraphType.Table)
                    {
                        Rows.Clear();
                        Columns.Clear();
                        Cells.Clear();
                        foreach (DocumentContent content in Contents)
                        {
                            content.RenderCellCode = null;
                        }
                    }
                }
            }
        }

        public override int? GetId()
        {
            return DocumentParagraphId;
        }

        public string? DisplayContent()
        {
            if (DocumentParagraphType == DocumentParagraphType.Table) return Name;

            StringBuilder contents = new();

            foreach (DocumentContent content in Contents)
            {
                if (content.ContentType == DocumentContentType.Text)
                {
                    contents.Append(content.Content);
                }
                else
                {
                    contents.Append(content.Name);
                }
            }

            return contents.ToString();
        }

        protected override void PropagateReadOnly()
        {
            foreach (DocumentContent content in Contents)
            {
                content.IsReadOnly = IsReadOnly;
            }

            foreach (DocumentTableColumn column in Columns)
            {
                column.IsReadOnly = IsReadOnly;
            }

            foreach (DocumentTableRow row in Rows)
            {
                row.IsReadOnly = IsReadOnly;
            }

            foreach (DocumentTableCell cell in Cells)
            {
                cell.IsReadOnly = IsReadOnly;
            }
        }
    }
}
