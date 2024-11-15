namespace Origin.Core.Models
{
    public class Document
    {
        public int DocumentId { get; set; }
        public DocumentGeneratorType DocumentGeneratorType { get; set; }
        public string? Target { get; set; }
        public string? FilenameTemplate { get; set; }
        public string? Filename { get; set; }
        public DocumentConfig? Config { get; set; }
    }
}
