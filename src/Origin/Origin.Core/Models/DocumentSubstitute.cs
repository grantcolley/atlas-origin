using Atlas.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Origin.Core.Models
{
    public class DocumentSubstitute : ModelBase
    {
        public int DocumentSubstituteId { get; set; }

        [Required]
        [StringLength(100)]
        public string? Key { get; set; }

        [StringLength(100)]
        public string? Group { get; set; }

        [NotMapped]
        public string? Value { get; set; }
    }
}
