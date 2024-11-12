using Atlas.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Commercial.Core.Models
{
    public class Customer : ModelBase
    {
        public Customer()
        {
            Products = [];
        }

        public int CustomerId { get; set; }
        public List<Product> Products { get; set; }

        [Required]
        [StringLength(5)]
        public string? Title { get; set; }

        [Required]
        [StringLength(50)]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string? Surname { get; set; }

        [StringLength(15)]
        public string? Telephone { get; set; }

        [StringLength(50)]
        public string? Email { get; set; }

        [MaxLength(8)]
        [RegularExpression(@"^(\d){6}$")]
        public string? SortCode { get; set; }

        [MaxLength(8)]
        [RegularExpression(@"^(\d){8}$")]
        public string? AccountNumber { get; set; }

        [StringLength(50)]
        public string? Address1 { get; set; }

        [StringLength(50)]
        public string? Address2 { get; set; }

        [StringLength(50)]
        public string? Address3 { get; set; }

        [StringLength(50)]
        public string? Address4 { get; set; }

        [StringLength(50)]
        public string? Address5 { get; set; }

        [StringLength(50)]
        public string? Country { get; set; }

        [StringLength(10)]
        public string? PostCode { get; set; }
    }
}
