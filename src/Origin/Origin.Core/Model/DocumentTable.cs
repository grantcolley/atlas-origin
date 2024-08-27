using Atlas.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Origin.Core.Model
{
    public class DocumentTable : ModelBase
    {
        public DocumentTable() 
        {
            Rows = [];
            Columns = [];
            Cells = [];
        }

        public int DocumentTableId { get; set; }

        public List<DocumentTableRow> Rows { get; set; }
        public List<DocumentTableColumn> Columns { get; set; }
        public List<DocumentTableCell> Cells { get; set; }

        [Required]
        [StringLength(250)]
        public string? Name { get; set; }

        [Required]
        [StringLength(100)]
        public string? Code { get; set; }

        [Required]
        [StringLength(100)]
        public string? RenderElementCode { get; set; }
    }
}
