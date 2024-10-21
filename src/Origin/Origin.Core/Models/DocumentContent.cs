using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Origin.Core.Models
{
    public class DocumentContent : DocumentContentProperties
    {
        private DocumentContentType _contentType;

        public int DocumentContentId { get; set; }
        public int Order { get; set; }
        public string? Content { get; set; }
        public bool? Bold { get; set; }
        public bool? Italic { get; set; }
        public bool? Underscore { get; set; }
        public int? ImageHeight { get; set; }
        public int? ImageWidth { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [StringLength(100)]
        public string? RenderCellCode { get; set; }

        [StringLength(500)]
        public string? Source { get; set; }

        public DocumentContentType ContentType 
        {
            get { return _contentType; }
            set
            {
                if(_contentType != value)
                {
                    _contentType = value;

                    if(_contentType == DocumentContentType.Text)
                    {
                        Source = null;
                        ImageHeight = null;
                        ImageWidth = null;
                    }
                    else if(_contentType == DocumentContentType.Image)
                    {
                        Content = null;
                        Bold = null;
                        Italic = null;
                        Underscore = null;
                        IgnoreParapgraphSpacing = null;
                        Font = null;
                        FontSize = null;
                        Colour = null;
                        SubstituteStart = null;
                        SubstituteEnd = null;
                    }
                }
            }
        }

        [NotMapped]
        [JsonIgnore]
        public string? Tag { get; set; }
    }
}
