using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Origin.Core.Models
{
    public class DocumentTableCell : DocumentContentPropertiesBase
    {
        public DocumentTableCell() 
        {
            Contents = [];
        }

        public int DocumentTableCellId { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int? BorderLeft { get; set; }
        public int? BorderTop { get; set; }
        public int? BorderRight { get; set; }
        public int? BorderBottom { get; set; }
        public string? BorderLeftColour { get; set; }
        public string? BorderTopColour { get; set; }
        public string? BorderRightColour { get; set; }
        public string? BorderBottomColour { get; set; }
        public string? CellColour { get; set; }

        [Required]
        [StringLength(100)]
        public string? Code { get; set; }

        [Required]
        [StringLength(100)]
        public string? RenderElementCode { get; set; }

        [NotMapped]
        [JsonIgnore]
        public List<DocumentContent> Contents { get; set; }
    }
}
