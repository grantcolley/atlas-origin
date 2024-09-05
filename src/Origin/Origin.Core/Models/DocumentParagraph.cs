using System.ComponentModel.DataAnnotations;

namespace Origin.Core.Models
{
    public class DocumentParagraph : DocumentContentPropertiesBase
    {
        public DocumentParagraph() 
        {
            Rows = [];
            Columns = [];
            Cells = [];
            Contents = [];
            DocumentConfigs = [];
        }

        public int DocumentParagraphId { get; set; }
        public int Order { get; set; }
        public bool? IgnoreParapgraphSpacing { get; set; }
        public DocumentContentAlign AlignContent { get; set; }
        public DocumentParagraphType DocumentParagraphType { get; set; }
        public List<DocumentTableRow> Rows { get; set; }
        public List<DocumentTableColumn> Columns { get; set; }
        public List<DocumentTableCell> Cells { get; set; }
        public List<DocumentContent> Contents { get; set; }
        public List<DocumentConfig> DocumentConfigs { get; set; }

        [Required]
        [StringLength(250)]
        public string? Name { get; set; }

        [Required]
        [StringLength(100)]
        public string? Code { get; set; }
    }
}
