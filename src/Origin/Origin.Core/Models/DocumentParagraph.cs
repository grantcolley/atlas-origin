using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Origin.Core.Models
{
    public class DocumentParagraph : DocumentParagraphProperties
    {
        public DocumentParagraph() 
        {
            Rows = [];
            Columns = [];
            Cells = [];
            Contents = [];
        }

        public int DocumentParagraphId { get; set; }
        public DocumentParagraphType DocumentParagraphType { get; set; }
        public List<DocumentTableRow> Rows { get; set; }
        public List<DocumentTableColumn> Columns { get; set; }
        public List<DocumentTableCell> Cells { get; set; }
        public List<DocumentContent> Contents { get; set; }

        [Required]
        [StringLength(250)]
        public string? Name { get; set; }

        public override int? GetId()
        {
            return DocumentParagraphId;
        }
    }
}
