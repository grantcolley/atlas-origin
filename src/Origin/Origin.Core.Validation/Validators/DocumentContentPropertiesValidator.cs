using FluentValidation;
using Origin.Core.Models;

namespace Origin.Core.Validation.Validators
{
    public class DocumentContentPropertiesValidator : AbstractValidator<DocumentContentProperties>
    {
        public DocumentContentPropertiesValidator()
        {
            RuleFor(v => v.Font)
                .MaximumLength(100).WithMessage("Code cannot exceed 100 characters");

            RuleFor(v => v.Colour)
                .MaximumLength(11).WithMessage("Colour cannot exceed 11 characters and must be in the format: RGB 0-255 e.g. 0,176,240");

            RuleFor(v => v.SubstituteStart)
                .MaximumLength(1).WithMessage("Substitute Start can only be 1 character");

            RuleFor(v => v.SubstituteEnd)
                .MaximumLength(1).WithMessage("Substitute End can only be 1 character");
        }
    }
}
