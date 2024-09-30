using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Origin.Core.Models
{
    public class DocumentParagraph : DocumentParagraphProperties
    {
        public DocumentParagraph() 
        {
            Rows = [];
            Columns = [];
            Cells = [];
            Contents = [];
            DocumentConfigs = [];
        }

        public int DocumentParagraphId { get; set; }
        public int Order { get; set; }
        public DocumentContentAlign AlignContent { get; set; }
        public DocumentParagraphType DocumentParagraphType { get; set; }
        public List<DocumentTableRow> Rows { get; set; }
        public List<DocumentTableColumn> Columns { get; set; }
        public List<DocumentTableCell> Cells { get; set; }
        public List<DocumentContent> Contents { get; set; }
        public List<DocumentConfig> DocumentConfigs { get; set; }

        [Required]
        [StringLength(250)]
        public string? Name { get; set; }

        public override int? GetId()
        {
            return DocumentParagraphId;
        }
    }

    public class DocumentParagraphValidator : AbstractValidator<DocumentParagraph>
    {
        public DocumentParagraphValidator()
        {
            RuleFor(v => v.Name)
                .NotNull().WithMessage("Name is required")
                .Length(1, 250).WithMessage("Name cannot exceed 250 characters");
        }
    }
}
