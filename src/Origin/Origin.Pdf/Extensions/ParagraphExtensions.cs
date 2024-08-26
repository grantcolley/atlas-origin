using MigraDoc.DocumentObjectModel;
using Origin.Converters;
using Origin.Interfaces;
using Origin.Model;

namespace Origin.Pdf.Extensions
{
    public static class ParagraphExtensions
    {
        public static void AddContent(this Paragraph p, DocumentParagraph documentParagraph, IDocumentProperties documentProperties)
        {
            ArgumentNullException.ThrowIfNull(documentParagraph);
            ArgumentNullException.ThrowIfNull(documentProperties);

            if (!documentParagraph.IgnoreParapgraphSpacing.HasValue
                || !documentParagraph.IgnoreParapgraphSpacing.Value)
            {
                p.Format.SpaceBefore = Unit.FromMillimeter(documentProperties.ParagraphSpacingBetweenLinesBefore);
                p.Format.SpaceAfter = Unit.FromMillimeter(documentProperties.ParagraphSpacingBetweenLinesAfter);
            }

            foreach (DocumentContent text in documentParagraph.Contents.OrderBy(t => t.Order))
            {
                p.AddText(text);
            }
        }

        public static void AddText(this Paragraph p, DocumentContent content)
        {
            ArgumentNullException.ThrowIfNull(content);

            FormattedText text = p.AddFormattedText();

            if (!string.IsNullOrWhiteSpace(content.Font)) text.Font = new Font(content.Font);
            if (content.FontSize.HasValue) text.Size = content.FontSize.Value;
            if (content.Bold.HasValue) text.Bold = content.Bold.Value;
            if (content.Italic.HasValue) text.Italic = content.Italic.Value;
            if (content.Underscore.HasValue && content.Underscore.Value) text.Underline = Underline.Single;

            if (!string.IsNullOrWhiteSpace(content.Colour))
            {
                byte[] rgb = content.Colour.RgbToByteArray();
                text.Color = new Color(rgb[0], rgb[1], rgb[2]);
            }

            text.AddText(content.Content ?? string.Empty);
        }
    }
}
