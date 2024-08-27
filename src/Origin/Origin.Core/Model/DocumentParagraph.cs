using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Origin.Core.Model
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

        [Required]
        [StringLength(250)]
        public string? Name { get; set; }

        [Required]
        [StringLength(100)]
        public string? Code { get; set; }

        [NotMapped]
        [JsonIgnore]
        public List<DocumentContent> Contents { get; set; }

        [NotMapped]
        [JsonIgnore]
        public DocumentTable? Table { get; set; }
    }
}
