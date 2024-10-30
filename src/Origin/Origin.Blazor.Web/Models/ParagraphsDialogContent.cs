using Origin.Core.Models;

namespace Origin.Blazor.Web.Models
{
    public class ParagraphsDialogContent
    {
        public ParagraphsDialogContent() 
        {
            Paragraphs = [];
        }

        public List<DocumentParagraph> Paragraphs { get; set; }
    }
}
