using Origin.Core.Interfaces;

namespace Origin.Core.Extensions
{
    public static class DoocumentContentPropertiesExtensions
    {
        public static void InheritContentProperties(this IDocumentContentProperties documentContentProperties, IDocumentContentProperties inherit)
        {
            ArgumentNullException.ThrowIfNull(inherit);

            if (string.IsNullOrWhiteSpace(documentContentProperties.Font)) documentContentProperties.Font = inherit.Font;
            if (string.IsNullOrWhiteSpace(documentContentProperties.Colour)) documentContentProperties.Colour = inherit.Colour;
            if (!documentContentProperties.FontSize.HasValue) documentContentProperties.FontSize = inherit.FontSize;
        }
    }
}
