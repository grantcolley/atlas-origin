using Origin.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Origin.Core.Models
{
    public abstract class DocumentParagraphProperties : DocumentContentProperties, IDocumentParagraphProperties
    {
        [Required]
        public int ParagraphSpacingBetweenLinesAfter { get; set; } = 10;

        [Required]
        public int ParagraphSpacingBetweenLinesBefore { get; set; } = 10;
    }
}
