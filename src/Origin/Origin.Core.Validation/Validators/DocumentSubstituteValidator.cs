using FluentValidation;
using Origin.Core.Models;

namespace Origin.Core.Validation.Validators
{
    public class DocumentSubstituteValidator : AbstractValidator<DocumentSubstitute>
    {
        public DocumentSubstituteValidator()
        {
            RuleFor(v => v.Key)
                .NotEmpty().WithMessage("Key is required")
                .Length(1, 100).WithMessage("Key cannot exceed 100 characters");

            RuleFor(v => v.Group)
                .MaximumLength(100).WithMessage("Group cannot exceed 100 characters");
        }
    }
}
