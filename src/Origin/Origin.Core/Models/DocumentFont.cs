using Atlas.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Origin.Core.Models
{
    public class DocumentFont : ModelBase
    {
        public int DocumentFontId { get; set; }

        [Required]
        [StringLength(150)]
        public string? Font { get; set; }
    }
}
