using Origin.Interfaces;

namespace Origin.Extensions
{
    public static class DocumentContentExtensions
    {
        public static void InheritProperties(this IDocumentContentProperties documentContentProperties, IDocumentContentProperties inherit)
        {
            ArgumentNullException.ThrowIfNull(inherit);

            if (string.IsNullOrWhiteSpace(documentContentProperties.Font)) documentContentProperties.Font = inherit.Font;
            if (string.IsNullOrWhiteSpace(documentContentProperties.Colour)) documentContentProperties.Colour = inherit.Colour;
            if (!documentContentProperties.FontSize.HasValue) documentContentProperties.FontSize = inherit.FontSize;
            if (!documentContentProperties.Bold.HasValue) documentContentProperties.Bold = inherit.Bold;
            if (!documentContentProperties.Italic.HasValue) documentContentProperties.Italic = inherit.Italic;
            if (!documentContentProperties.Underscore.HasValue) documentContentProperties.Underscore = inherit.Underscore;
        }
    }
}
