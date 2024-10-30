using Origin.Core.Models;

namespace Origin.Blazor.Web.Models
{
    public class ParagraphSelect
    {
        public bool IsSelected { get; set; }
        public int DocumentParagraphId { get; set; }
        public string? Name { get; set; }
        public DocumentParagraphType DocumentParagraphType { get; set; }
        public string? Content { get; set; }
    }
}
