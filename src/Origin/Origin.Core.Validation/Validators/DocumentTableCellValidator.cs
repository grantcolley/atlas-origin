using FluentValidation;
using Origin.Core.Models;

namespace Origin.Core.Validation.Validators
{
    public class DocumentTableCellValidator : AbstractValidator<DocumentTableCell>
    {
        public DocumentTableCellValidator()
        {
            RuleFor(v => v.CellCode)
                .NotEmpty().WithMessage("Cell Code is required")
                .Length(1, 100).WithMessage("Cell Code cannot exceed 100 characters");

            RuleFor(v => v.BorderLeftColour)
                .MaximumLength(11).WithMessage("Border Left Colour cannot exceed 11 characters and must be in the format: RGB 0-255 e.g. 0,176,240");

            RuleFor(v => v.BorderTopColour)
                .MaximumLength(11).WithMessage("Border Top Colour cannot exceed 11 characters and must be in the format: RGB 0-255 e.g. 0,176,240");

            RuleFor(v => v.BorderRightColour)
                .MaximumLength(11).WithMessage("Border Right Colour cannot exceed 11 characters and must be in the format: RGB 0-255 e.g. 0,176,240");

            RuleFor(v => v.BorderBottomColour)
                .MaximumLength(11).WithMessage("Border Bottom Colour cannot exceed 11 characters and must be in the format: RGB 0-255 e.g. 0,176,240");

            RuleFor(v => v.CellColour)
                .MaximumLength(11).WithMessage("Cell Colour cannot exceed 11 characters and must be in the format: RGB 0-255 e.g. 0,176,240");

            Include(new DocumentContentPropertiesValidator());
        }
    }
}
