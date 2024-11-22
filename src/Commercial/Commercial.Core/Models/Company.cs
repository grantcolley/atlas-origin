using Atlas.Core.Models;

namespace Commercial.Core.Models
{
    public class Company : ModelBase
    {
        public int CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyAddress1 { get; set; }
        public string? CompanyAddress2 { get; set; }
        public string? CompanyAddress3 { get; set; }
        public string? CompanyPhoneNumber { get; set; }
        public string? CompanyEmail { get; set; }
        public string? Signee { get; set; }
        public string? SigneeTitle { get; set; }
    }
}