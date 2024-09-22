using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Origin.Core.Models
{
    public class DocumentContent : DocumentContentProperties
    {
        public int DocumentContentId { get; set; }
        public int Order { get; set; }
        public string? Content { get; set; }
        public bool? Bold { get; set; }
        public bool? Italic { get; set; }
        public bool? Underscore { get; set; }
        public int? ImageHeight { get; set; }
        public int? ImageWidth { get; set; }
        public bool? IgnoreParapgraphSpacing { get; set; }
        public DocumentContentType ContentType { get; set; }
        public DocumentContentAlign AlignContent { get; set; }

        [Required]
        [StringLength(100)]
        public string? Code { get; set; }

        [Required]
        [StringLength(100)]
        public string? RenderCellCode { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [StringLength(500)]
        public string? Source { get; set; }

        [NotMapped]
        [JsonIgnore]
        public string? Tag { get; set; }
    }
}
