using System.ComponentModel.DataAnnotations;

namespace Origin.Core.Interfaces
{
    public interface IDocumentContentProperties
    {
        string? Font { get; set; }
        int? FontSize { get; set; }
        string? SubstituteStart { get; set; }
        string? SubstituteEnd { get; set; }

        /// <summary>
        /// RGB 0-255 e.g. 0,176,240
        /// </summary>
        string? Colour { get; set; }
    }
}
