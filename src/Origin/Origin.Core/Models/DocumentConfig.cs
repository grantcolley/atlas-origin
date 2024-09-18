using FluentValidation;
using Origin.Core.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Origin.Core.Models
{
    public class DocumentConfig : DocumentParagraphProperties, IDocumentProperties
    {
        public DocumentConfig() 
        {
            Substitutes = [];
            Paragraphs = [];
        }

        public int DocumentConfigId { get; set; }

        public List<DocumentSubstitute> Substitutes { get; set; }
        public List<DocumentParagraph> Paragraphs { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [Required]
        public int PageMarginLeft { get; set; } = 10;

        [Required]
        public int PageMarginTop { get; set; } = 10;

        [Required]
        public int PageMarginRight { get; set; } = 10;

        /// <summary>
        /// Note: extra margin creates space for the content of the footer in MigraDocs.
        /// </summary>
        [Required]
        public int PageMarginBottom { get; set; } = 55;

        [NotMapped]
        public DocumentServiceType DocumentServiceType { get; set; }

        [NotMapped]
        public string? OutputLocation { get; set; }

        [NotMapped]
        public string? FilenameTemplate { get; set; }
    }

    public class DocumentConfigValidator : AbstractValidator<DocumentConfig>
    {
        public DocumentConfigValidator()
        {
            RuleFor(v => v.Name)
                .NotNull().WithMessage("Name is required")
                .Length(1, 100).WithMessage("Name cannot exceed 100 characters");

            RuleFor(v => v.PageMarginLeft)
                .GreaterThan(-1).WithMessage("PageMarginLeft must be 0 or greater.");

            RuleFor(v => v.PageMarginTop)
                .GreaterThan(-1).WithMessage("PageMarginTop must be 0 or greater.");

            RuleFor(v => v.PageMarginRight)
                .GreaterThan(-1).WithMessage("PageMarginRight must be 0 or greater.");

            RuleFor(v => v.PageMarginBottom)
                .GreaterThan(-1).WithMessage("PageMarginBottom must be 0 or greater.");

            RuleFor(v => v.ParagraphSpacingBetweenLinesBefore)
                .GreaterThan(-1).WithMessage("ParagraphSpacingBetweenLinesBefore must be 0 or greater.");

            RuleFor(v => v.ParagraphSpacingBetweenLinesAfter)
                .GreaterThan(-1).WithMessage("ParagraphSpacingBetweenLinesAfter must be 0 or greater.");
        }
    }
}
