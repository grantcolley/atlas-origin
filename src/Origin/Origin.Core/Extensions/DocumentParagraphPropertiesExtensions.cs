using Origin.Core.Interfaces;

namespace Origin.Core.Extensions
{
    public static class DocumentParagraphPropertiesExtensions
    {
        public static void InheritParagraphProperties(this IDocumentParagraphProperties documentParagraphProperties, IDocumentParagraphProperties inherit)
        {
            ArgumentNullException.ThrowIfNull(inherit);

            if(!documentParagraphProperties.IgnoreParapgraphSpacing.HasValue) documentParagraphProperties.IgnoreParapgraphSpacing = inherit.IgnoreParapgraphSpacing;

            if (documentParagraphProperties.ParagraphSpacingBetweenLinesBefore <= 0 && inherit.ParagraphSpacingBetweenLinesBefore > 0)
            {
                documentParagraphProperties.ParagraphSpacingBetweenLinesBefore = inherit.ParagraphSpacingBetweenLinesBefore;
            }

            if (documentParagraphProperties.ParagraphSpacingBetweenLinesAfter <= 0 && inherit.ParagraphSpacingBetweenLinesAfter > 0)
            {
                documentParagraphProperties.ParagraphSpacingBetweenLinesAfter = inherit.ParagraphSpacingBetweenLinesAfter;
            }

            documentParagraphProperties.InheritContentProperties(inherit);
        }
    }
}
