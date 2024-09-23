using Atlas.Core.Models;

namespace Origin.Core.Models
{
    public class DocumentTableColumn : ModelBase
    {
        public int DocumentTableColumnId { get; set; }
        public int Number { get; set; }
        public int? Width { get; set; }
    }
}
