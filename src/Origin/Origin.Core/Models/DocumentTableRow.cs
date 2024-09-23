using Atlas.Core.Models;

namespace Origin.Core.Models
{
    public class DocumentTableRow : ModelBase
    {
        public int DocumentTableRowId { get; set; }
        public int Number { get; set; }
        public int? Height { get; set; }
    }
}
