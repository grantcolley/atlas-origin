using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
        }

        public int DocumentParagraphId { get; set; }
        public DocumentParagraphType DocumentParagraphType { get; set; }
        public List<DocumentTableRow> Rows { get; set; }
        public List<DocumentTableColumn> Columns { get; set; }
        public List<DocumentTableCell> Cells { get; set; }
        public List<DocumentContent> Contents { get; set; }

        [Required]
        [StringLength(250)]
        public string? Name { get; set; }

        public override int? GetId()
        {
            return DocumentParagraphId;
        }

        public string? DisplayContent()
        {
            if (DocumentParagraphType == DocumentParagraphType.Table) return Name;

            StringBuilder contents = new();

            foreach (DocumentContent content in Contents)
            {
                if(content.ContentType == DocumentContentType.Text)
                {
                    contents.Append(content.Content);
                }
                else
                {
                    contents.Append(content.Name);
                }
            }

            return contents.ToString();
        }
    }

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
