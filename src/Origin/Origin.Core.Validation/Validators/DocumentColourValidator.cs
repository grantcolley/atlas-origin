using FluentValidation;
using Origin.Core.Models;

namespace Origin.Core.Validation.Validators
{
    public class DocumentColourValidator : AbstractValidator<DocumentColour>
    {
        public DocumentColourValidator()
        {
            RuleFor(v => v.Colour)
                .NotEmpty().WithMessage("Colour")
                .Length(1, 100).WithMessage("Colour cannot exceed 100 characters");

            RuleFor(v => v.Rgb)
                .NotEmpty().WithMessage("RGB")
                .Length(5, 11).WithMessage("RGB must be between 5 and 11 characters in the format 0,176,240");
        }
    }
}
