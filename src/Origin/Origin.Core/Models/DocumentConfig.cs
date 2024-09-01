using FluentValidation;
using Origin.Core.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Origin.Core.Models
{
    public class DocumentConfig : DocumentContentPropertiesBase, IDocumentProperties
    {
        public DocumentConfig() 
        {
            Substitutes = [];
            Contents = [];
            Paragraphs = [];
            Tables = [];
        }

        public int DocumentConfigId { get; set; }

        public List<DocumentSubstitute> Substitutes { get; set; }
        public List<DocumentContent> Contents { get; set; }
        public List<DocumentParagraph> Paragraphs { get; set; }
        public List<DocumentTable> Tables { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [Required]
        [StringLength(1)]
        public string? SubstituteStart { get; set; }

        [Required]
        [StringLength(1)]
        public string? SubstituteEnd { get; set; }

        [Required]
        [StringLength(150)]
        public string? FilenameTemplate { get; set; }

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

        [Required]
        public int ParagraphSpacingBetweenLinesAfter { get; set; } = 10;

        [Required]
        public int ParagraphSpacingBetweenLinesBefore { get; set; } = 10;

        [NotMapped]
        public DocumentServiceType DocumentServiceType { get; set; }
        [NotMapped]
        public string? OutputLocation { get; set; }
    }

    public class DocumentConfigValidator : AbstractValidator<DocumentConfig>
    {
        public DocumentConfigValidator()
        {
            RuleFor(v => v.Name)
                .NotNull().WithMessage("Name is required")
                .Length(1, 100).WithMessage("Name cannot exceed 100 characters");

            RuleFor(v => v.SubstituteStart)
                .NotNull().WithMessage("SubstituteStart requires a single character. Consider using the open square bracket [")
                .Length(1).WithMessage("SubstituteStart requires a single character. Consider using the open square bracket [");

            RuleFor(v => v.SubstituteEnd)
                .NotNull().WithMessage("SubstituteEnd requires a single character. Consider using the closed square bracket ]")
                .Length(1).WithMessage("SubstituteEnd requires a single character. Consider using the closed square bracket ]");

            RuleFor(v => v.FilenameTemplate)
                .NotNull().WithMessage("FilenameTemplate is required. Do not include a file extension.")
                .Length(1, 150).WithMessage("FilenameTemplate cannot exceed 120 characters. Do not include a file extension.");
        }
    }
}
