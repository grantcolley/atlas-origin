using Atlas.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Origin.Core.Model
{
    public class DocumentTableColumn : ModelBase
    {
        public int DocumentTableColumnId { get; set; }
        public int Position { get; set; }
        public int? Width { get; set; }

        [Required]
        [StringLength(100)]
        public string? TableCode { get; set; }
    }
}
