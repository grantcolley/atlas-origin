using FluentValidation;
using Origin.Core.Models;

namespace Origin.Core.Validation.Validators
{
    public class DocumentFontValidator : AbstractValidator<DocumentFont>
    {
        public DocumentFontValidator()
        {
            RuleFor(v => v.Font)
                .NotEmpty().WithMessage("Font")
                .Length(1, 150).WithMessage("Font cannot exceed 150 characters");
        }
    }
}
