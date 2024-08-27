using Atlas.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Origin.Core.Model
{
    public class DocumentSubstitute : ModelBase
    {
        public int DocumentSubstituteId { get; set; }

        [Required]
        [StringLength(100)]
        public string? Key { get; set; }

        [Required]
        [StringLength(100)]
        public string? Value { get; set; }

        [StringLength(100)]
        public string? Group { get; set; }
    }
}
