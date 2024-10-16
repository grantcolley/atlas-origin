using FluentValidation;
using Origin.Core.Models;

namespace Origin.Core.Validation.Validators
{
    public class DocumentConfigValidator : AbstractValidator<DocumentConfig>
    {
        public DocumentConfigValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(1, 100).WithMessage("Name cannot exceed 100 characters");

            RuleFor(v => v.PageMarginLeft)
                .GreaterThan(-1).WithMessage("PageMarginLeft must be 0 or greater.");

            RuleFor(v => v.PageMarginTop)
                .GreaterThan(-1).WithMessage("PageMarginTop must be 0 or greater.");

            RuleFor(v => v.PageMarginRight)
                .GreaterThan(-1).WithMessage("PageMarginRight must be 0 or greater.");

            RuleFor(v => v.PageMarginBottom)
                .GreaterThan(-1).WithMessage("PageMarginBottom must be 0 or greater.");

            Include(new DocumentParagraphPropertiesValidator());
            Include(new DocumentContentPropertiesValidator());

            RuleForEach(v => v.Substitutes).SetValidator(new DocumentSubstituteValidator());
        }
    }
}
