namespace Origin.Interfaces
{
    public interface IDocumentContentProperties
    {
        string? Font { get; set; }
        int? FontSize { get; set; }
        string? Colour { get; set; }
        bool? Bold { get; set; }
        bool? Italic { get; set; }
        bool? Underscore { get; set; }
    }
}
