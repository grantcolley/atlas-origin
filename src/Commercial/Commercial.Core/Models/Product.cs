using Atlas.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Commercial.Core.Models
{
    public class Product : ModelBase
    {
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        [Required]
        [StringLength(10)]
        public string? ProductType { get; set; }

        [Required]
        [StringLength(10)]
        public string? RateType { get; set; }

        [Required]
        [StringLength(20)]
        public string? RepaymentType { get; set; }

        [Required]
        [StringLength(50)]
        public string? Name { get; set; }

        [Required]
        public DateTime? StartDate { get; set; }

        [Range(3, 360)]
        public int? Duration { get; set; }

        [Column(TypeName = "decimal(4, 2)")]
        public decimal? Rate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Value { get; set; }
    }
}
