using Atlas.Core.Models;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Origin.Core.Models
{
    public class DocumentSubstitute : ModelBase
    {
        public int DocumentSubstituteId { get; set; }

        [Required]
        [StringLength(100)]
        public string? Key { get; set; }

        [StringLength(100)]
        public string? Group { get; set; }

        [NotMapped]
        public string? Value { get; set; }
    }

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
