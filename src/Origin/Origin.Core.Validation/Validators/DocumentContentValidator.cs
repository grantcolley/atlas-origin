using FluentValidation;
using Origin.Core.Models;

namespace Origin.Core.Validation.Validators
{
    public class DocumentContentValidator : AbstractValidator<DocumentContent>
    {
        public DocumentContentValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(1, 100).WithMessage("Cell Code cannot exceed 100 characters");

            RuleFor(v => v.RenderCellCode)
                .MaximumLength(100).WithMessage("Render Cell Code cannot exceed 100 characters");

            RuleFor(v => v.Image)
                .MaximumLength(100).WithMessage("Image cannot exceed 100 characters");

            Include(new DocumentContentPropertiesValidator());
        }
    }
}
