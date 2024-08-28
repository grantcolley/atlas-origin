using System.ComponentModel.DataAnnotations;

namespace Origin.Core.Models
{
    public class DocumentTableRow
    {
        public int DocumentTableRowId { get; set; }
        public int Position { get; set; }
        public int? Height { get; set; }

        [Required]
        [StringLength(100)]
        public string? TableCode { get; set; }

    }
}
