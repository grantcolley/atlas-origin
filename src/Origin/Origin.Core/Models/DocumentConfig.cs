using Origin.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

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
        public DocumentServiceType DocumentServiceType { get; set; }

        public List<DocumentSubstitute> Substitutes { get; set; }
        public List<DocumentContent> Contents { get; set; }
        public List<DocumentParagraph> Paragraphs { get; set; }
        public List<DocumentTable> Tables { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [StringLength(500)]
        public string? OutputLocation { get; set; }

        [Required]
        [StringLength(1)]
        public char? SubstituteStart { get; set; }

        [Required]
        [StringLength(1)]
        public char? SubstituteEnd { get; set; }

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
    }
}
