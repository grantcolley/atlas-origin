using FluentValidation;
using Origin.Core.Models;

namespace Origin.Core.Validation.Validators
{
    public class DocumentParagraphValidator : AbstractValidator<DocumentParagraph>
    {
        public DocumentParagraphValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(1, 250).WithMessage("Name cannot exceed 250 characters");

            RuleForEach(v => v.Cells).SetValidator(new DocumentTableCellValidator());
            RuleForEach(v => v.Contents).SetValidator(new DocumentContentValidator());

            Include(new DocumentParagraphPropertiesValidator());
            Include(new DocumentContentPropertiesValidator());
        }
    }
}
