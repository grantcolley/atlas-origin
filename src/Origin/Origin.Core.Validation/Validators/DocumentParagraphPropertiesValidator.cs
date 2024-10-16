using FluentValidation;
using Origin.Core.Models;

namespace Origin.Core.Validation.Validators
{
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
