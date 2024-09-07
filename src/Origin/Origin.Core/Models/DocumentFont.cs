using System.ComponentModel.DataAnnotations;

namespace Origin.Core.Models
{
    public class DocumentFont
    {
        [Required]
        [StringLength(150)]
        public string? Font { get; set; }
    }
}
