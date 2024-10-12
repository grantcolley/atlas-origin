using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Origin.Core.Models
{
    public class DocumentTableCell : DocumentContentProperties
    {
        public DocumentTableCell() 
        {
            Contents = [];
        }

        public int DocumentTableCellId { get; set; }
        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }
        public int? BorderLeft { get; set; }
        public int? BorderTop { get; set; }
        public int? BorderRight { get; set; }
        public int? BorderBottom { get; set; }

        [Required]
        [StringLength(100)]
        public string? CellCode { get; set; }

        /// <summary>
        /// RGB 0-255 e.g. 0,176,240
        /// </summary>
        [StringLength(11)]
        public string? BorderLeftColour { get; set; }

        /// <summary>
        /// RGB 0-255 e.g. 0,176,240
        /// </summary>
        [StringLength(11)]
        public string? BorderTopColour { get; set; }

        /// <summary>
        /// RGB 0-255 e.g. 0,176,240
        /// </summary>
        [StringLength(11)]
        public string? BorderRightColour { get; set; }

        /// <summary>
        /// RGB 0-255 e.g. 0,176,240
        /// </summary>
        [StringLength(11)]
        public string? BorderBottomColour { get; set; }

        /// <summary>
        /// RGB 0-255 e.g. 0,176,240
        /// </summary>
        [StringLength(11)]
        public string? CellColour { get; set; }

        [NotMapped]
        [JsonIgnore]
        public List<DocumentContent> Contents { get; set; }

        public override int? GetId()
        {
            return DocumentTableCellId;
        }
    }

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
