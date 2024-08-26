using System.ComponentModel.DataAnnotations;

namespace Origin.Model
{
    public class DocumentParagraph : DocumentContentPropertiesBase
    {
        public DocumentParagraph() 
        {
            Contents = [];
        }

        public int DocumentParagraphId { get; set; }
        public int Order { get; set; }
        public bool? IgnoreParapgraphSpacing { get; set; }
        public DocumentContentAlign AlignContent { get; set; }
        public DocumentParagraphType DocumentParagraphType { get; set; }
        public DocumentTable? Table { get; set; }
        public List<DocumentContent> Contents { get; set; }

        [Required]
        [StringLength(100)]
        public string? Code { get; set; }
    }
}
