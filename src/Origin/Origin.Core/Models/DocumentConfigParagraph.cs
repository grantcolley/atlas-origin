using Atlas.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Origin.Core.Models
{
    public class DocumentConfigParagraph : ModelBase
    {
        public int DocumentConfigParagraphId { get; set; }

        public int Order { get; set; }

        public DocumentParagraph? DocumentParagraph { get; set; }
    }
}
