using FluentValidation;
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

    public class DocumentParagraphPropertiesValidator : AbstractValidator<DocumentParagraphProperties>
    {
        public DocumentParagraphPropertiesValidator()
        {
            RuleFor(v => v.ParagraphSpacingBetweenLinesBefore)
                .GreaterThan(-1).WithMessage("ParagraphSpacingBetweenLinesBefore must be 0 or greater.");

            RuleFor(v => v.ParagraphSpacingBetweenLinesAfter)
                .GreaterThan(-1).WithMessage("ParagraphSpacingBetweenLinesAfter must be 0 or greater.");
        }
    }
}
