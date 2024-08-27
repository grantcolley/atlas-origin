using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using Origin.Core.Converters;
using Origin.Core.Interfaces;
using Origin.Core.Model;

namespace Origin.OpenXml.Extensions
{
    public static class ParagraphExtensions
    {
        public static void AddContent(this Paragraph p, DocumentParagraph documentParagraph, IDocumentProperties documentProperties)
        {
            ArgumentNullException.ThrowIfNull(documentParagraph);
            ArgumentNullException.ThrowIfNull(documentProperties);

            ParagraphProperties pPr = p.AppendChild(new ParagraphProperties());
            SpacingBetweenLines spacingBetweenLines = pPr.AppendChild(new SpacingBetweenLines());

            if (documentParagraph.IgnoreParapgraphSpacing.HasValue
                && documentParagraph.IgnoreParapgraphSpacing.Value)
            {
                spacingBetweenLines.Before = "0";
                spacingBetweenLines.After = "0";
            }
            else
            {
                spacingBetweenLines.Before = $"{documentProperties.ParagraphSpacingBetweenLinesBefore.ToTwips()}";
                spacingBetweenLines.After = $"{documentProperties.ParagraphSpacingBetweenLinesAfter.ToTwips()}";
            }

            pPr.Append(new Justification { Val = documentParagraph.AlignContent.ToJustification() });

            foreach (DocumentContent text in documentParagraph.Contents.OrderBy(t => t.Order))
            {
                p.AddText(text);
            }
        }

        public static void AddText(this Paragraph p, DocumentContent content)
        {
            ArgumentNullException.ThrowIfNull(content);

            SdtBlock sdtBlock = p.AppendChild(new SdtBlock());
            SdtContentBlock sdtContent = sdtBlock.AppendChild(new SdtContentBlock());
            Run r = sdtContent.AppendChild(new Run());

            RunProperties runProperties = r.AppendChild(new RunProperties());

            if (!string.IsNullOrWhiteSpace(content.Font)) runProperties.Append(new RunFonts { Ascii = content.Font, HighAnsi = content.Font });
            if (!string.IsNullOrWhiteSpace(content.Colour)) runProperties.Append(new Color { Val = content.Colour.RgbToHex() });
            if (content.FontSize.HasValue) runProperties.Append(new FontSize { Val = $"{content.FontSize.ToHalfPointEquivalent()}" });
            if (content.Bold.HasValue && content.Bold.Value) runProperties.Append(new Bold());
            if (content.Italic.HasValue && content.Italic.Value) runProperties.Append(new Italic());
            if (content.Underscore.HasValue && content.Underscore.Value) runProperties.Append(new Underline { Val = UnderlineValues.Single });

            Text t = r.AppendChild(new Text());
            t.Space = SpaceProcessingModeValues.Preserve;
            t.Text = content.Content ?? string.Empty;
        }
    }
}