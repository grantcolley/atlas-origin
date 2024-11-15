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
            ConfigParagraphs = [];
        }

        public int DocumentConfigId { get; set; }

        public List<DocumentSubstitute> Substitutes { get; set; }
        public List<DocumentConfigParagraph> ConfigParagraphs { get; set; }

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
        public bool ApplySubstitutes { get; set; }

        protected override void PropagateReadOnly()
        {
            foreach(DocumentSubstitute substitute in Substitutes)
            {
                substitute.IsReadOnly = IsReadOnly;
            }

            foreach(DocumentConfigParagraph configParagraph in ConfigParagraphs)
            {
                if(configParagraph.DocumentParagraph != null)
                {
                    configParagraph.DocumentParagraph.IsReadOnly = IsReadOnly;
                }
            }
        }
    }
}
