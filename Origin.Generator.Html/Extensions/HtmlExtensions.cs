using Origin.Core.Converters;
using Origin.Core.Extensions;
using Origin.Core.Interfaces;
using Origin.Core.Models;
using Origin.Resources;
using System.Text;
using System.Web;

namespace Origin.Generator.Html.Extensions
{
    public static class HtmlExtensions
    {
        private const string padding = "padding: 0cm .5pt 0cm .5pt";
        private const string table_margin_left = "margin-left: .5pt";

        public static void AddStyles(this StringBuilder sb, DocumentConfig documentConfig)
        {
            IEnumerable<string> fonts = documentConfig.GetDocumentFonts();

            sb.Append("<style type=\"text/css\">");

            foreach (string font in fonts)
            {
                string faceName = font.ToLower() switch
                {
                    "arial" => "arial",
                    "comic sans ms" => "comic",
                    "courier" => "cour",
                    "times new roman" => "times",
                    "tahoma" => "tahoma",
                    _ => throw new NotImplementedException(font),
                };

                sb.Append("@font-face {{");
                sb.Append($"font-family: \"{font}\";");
                sb.Append($"src: url('data:font/truetype;charset=utf-8;base64,{ResourceManager.GetFontAsBase64(faceName)}') format('truetype');");
                sb.Append("}}");
            }

            foreach (string font in fonts)
            {
                sb.Append("body {{");
                sb.Append($"font-family: \"{font}\";");
                sb.Append("}}");
            }

            sb.Append("</style>");
        }

        public static void AddFooter(this StringBuilder sb, DocumentConfig documentConfig)
        {
            ArgumentNullException.ThrowIfNull(documentConfig);

            DocumentParagraph? footerParagraph = documentConfig.GetFooterParagraph();

            if (footerParagraph == null) return;

            footerParagraph.IgnoreParapgraphSpacing = false;

            sb.AddContent(footerParagraph);
        }

        public static void AddParagraph(this StringBuilder sb, DocumentParagraph documentParagraph)
        {
            ArgumentNullException.ThrowIfNull(documentParagraph);

            if (documentParagraph.DocumentParagraphType == DocumentParagraphType.Table)
            {
                sb.AddTable(documentParagraph);
            }
            else
            {
                sb.AddContent(documentParagraph);
            }
        }

        public static void AddTable(this StringBuilder sb, DocumentParagraph documentParagraph)
        {
            ArgumentNullException.ThrowIfNull(documentParagraph);

            List<DocumentTableColumn> documentTableColumns = [.. documentParagraph.Columns.OrderBy(c => c.Number)];

            string width = documentTableColumns.Sum(tc => tc.Width ?? 0).ToPoints().ToString("#.##");
            string style = $@"width: {width}pt;{table_margin_left};{padding};border-spacing: 0px";
            string tblopen = $@"<table style='{style}'>";

            sb.Append(tblopen);
                        
            foreach (DocumentTableRow documentTableRow in documentParagraph.Rows.OrderBy(r => r.Number))
            {
                DocumentTableCell? cellInPreviousRow = null;
                DocumentTableCell? previousCellInRow = null;

                int previousRowNumber = documentTableRow.Number;

                sb.Append("<tr>");

                for (int i = 0; i < documentTableColumns.Count; i++)
                {
                    DocumentTableCell? documentTableCell = documentParagraph.Cells
                        .FirstOrDefault(c => c.RowNumber == documentTableRow.Number && c.ColumnNumber == documentTableColumns[i].Number);

                    documentTableCell ??= new DocumentTableCell { Contents = [new DocumentContent()] };

                    cellInPreviousRow = documentParagraph.Cells
                        .FirstOrDefault(c => c.RowNumber == previousRowNumber && c.ColumnNumber == documentTableColumns[i].Number);

                    sb.Append($"<td {GetTdStyle(documentTableColumns[i], documentTableCell, previousCellInRow, cellInPreviousRow)}>");

                    foreach (DocumentContent documentContent in documentTableCell.Contents.OrderBy(c => c.Order))
                    {
                        if (documentContent.ContentType == DocumentContentType.Text)
                        {
                            sb.AddTableCellText(documentParagraph, documentContent);
                        }
                        else if (documentContent.ContentType == DocumentContentType.Image)
                        {
                            sb.AddTableCellImage(documentContent);
                        }
                    }

                    sb.Append("</td>");

                    previousCellInRow = documentTableCell;
                }

                sb.Append("</tr>");
            }

            sb.Append("</table>");
        }

        public static string GetTdStyle(DocumentTableColumn documentTableColumn, DocumentTableCell documentTableCell, DocumentTableCell? previousCellInRow, DocumentTableCell? cellInPreviousRow)
        {
            StringBuilder border = new();
            string width = documentTableColumn.Width.HasValue ? $"width: {documentTableColumn.Width.ToPoints().ToString("#.##")}pt;" : string.Empty;
            string background = !string.IsNullOrWhiteSpace(documentTableCell?.CellColour) ? $"background-color: #{documentTableCell.CellColour.RgbToHex()};" : string.Empty;

            if (documentTableCell != null)
            {
                // For multiple cells prevent solid border-left overlapping with previous cell border-right
                if (documentTableCell.BorderLeft.HasValue
                    && (previousCellInRow == null || !(previousCellInRow.BorderRight.HasValue && previousCellInRow.BorderRight.Value > 0)))
                {
                    border.Append($"border-left: {documentTableCell.BorderLeft.Value.ToHalfPoints().ToString("#.##")}pt");

                    if (!string.IsNullOrWhiteSpace(documentTableCell.BorderLeftColour))
                    {
                        border.Append($" solid #{documentTableCell.BorderLeftColour.RgbToHex()}");
                    }

                    border.Append(';');
                }

                // For multiple rows prevent solid border-top overlapping with previous cell border-bottom
                if (documentTableCell.BorderTop.HasValue
                    && (cellInPreviousRow == null || !(cellInPreviousRow.BorderBottom.HasValue && cellInPreviousRow.BorderBottom.Value > 0)))
                {
                    border.Append($"border-top: {documentTableCell.BorderTop.Value.ToHalfPoints().ToString("#.##")}pt");

                    if (!string.IsNullOrWhiteSpace(documentTableCell.BorderTopColour))
                    {
                        border.Append($" solid #{documentTableCell.BorderTopColour.RgbToHex()}");
                    }

                    border.Append(';');
                }

                if (documentTableCell.BorderRight.HasValue)
                {
                    border.Append($"border-right: {documentTableCell.BorderRight.Value.ToHalfPoints().ToString("#.##")}pt");

                    if (!string.IsNullOrWhiteSpace(documentTableCell.BorderRightColour))
                    {
                        border.Append($" solid #{documentTableCell.BorderRightColour.RgbToHex()}");
                    }

                    border.Append(';');
                }

                if (documentTableCell.BorderBottom.HasValue)
                {
                    border.Append($"border-bottom: {documentTableCell.BorderBottom.Value.ToHalfPoints().ToString("#.##")}pt");

                    if (!string.IsNullOrWhiteSpace(documentTableCell.BorderBottomColour))
                    {
                        border.Append($" solid #{documentTableCell.BorderBottomColour.RgbToHex()}");
                    }

                    border.Append(';');
                }
            }

            return $"style='{width}{border}{background}{padding};border=0.5pt'";
        }

        public static void AddTableCellImage(this StringBuilder sb, DocumentContent? documentContent)
        {
            ArgumentNullException.ThrowIfNull(documentContent);
            ArgumentNullException.ThrowIfNull(documentContent.Image);

            string image = ResourceManager.GetPngAsBase64(documentContent.Image);

            string src = $"data:image/png;base64, {image}";
            string width = documentContent.ImageWidth.HasValue ? $"width={documentContent.ImageWidth.ToPoints()};" : string.Empty;
            string height = documentContent.ImageHeight.HasValue ? $"height={documentContent.ImageHeight.ToPoints()};" : string.Empty;

            sb.Append("<p>");
            sb.Append("<span>");
            sb.Append($"<img {width} {height} src=\"{src}\" alt=\"{documentContent.Name}\" />");
            sb.Append("</span>");
            sb.Append("</p>");
        }

        public static void AddTableCellText(this StringBuilder sb, IDocumentParagraphProperties documentParagraphProperties, DocumentContent? documentContent = null)
        {
            ArgumentNullException.ThrowIfNull(documentContent);

            if (string.IsNullOrWhiteSpace(documentContent.Content)) return;

            int marginTop = 0;
            int marginBottom = 0;

            if (!documentContent.IgnoreParapgraphSpacing.HasValue
                || !documentContent.IgnoreParapgraphSpacing.Value)
            {
                marginTop = documentParagraphProperties.ParagraphSpacingBetweenLinesBefore;
                marginBottom = documentParagraphProperties.ParagraphSpacingBetweenLinesAfter;
            }

            sb.Append($"<p style='margin-left: 0cm;margin-top: {marginTop}pt;margin-right: 0cm;margin-bottom: {marginBottom}pt;text-align: {documentContent.AlignContent.ToJustification()}'>");

            sb.AddText(documentContent);

            sb.Append("</p>");
        }

        public static void AddContent(this StringBuilder sb, DocumentParagraph documentParagraph)
        {
            ArgumentNullException.ThrowIfNull(documentParagraph);

            int marginTop = 0;
            int marginBottom = 0;

            if (!documentParagraph.IgnoreParapgraphSpacing.HasValue
                || !documentParagraph.IgnoreParapgraphSpacing.Value)
            {
                marginTop = documentParagraph.ParagraphSpacingBetweenLinesBefore;
                marginBottom = documentParagraph.ParagraphSpacingBetweenLinesAfter;
            }

            sb.Append($"<p style='margin-left: 0cm;margin-top: {marginTop}pt;margin-right: 0cm;margin-bottom: {marginBottom}pt;text-align: {documentParagraph.AlignContent.ToJustification()}'>");

            foreach (DocumentContent text in documentParagraph.Contents.OrderBy(t => t.Order))
            {
                sb.AddText(text);
            }

            sb.Append("</p>");
        }

        public static void AddText(this StringBuilder sb, DocumentContent content)
        {
            ArgumentNullException.ThrowIfNull(content);

            if (content.Bold.HasValue && content.Bold.Value) sb.Append("<b>");
            if (content.Italic.HasValue && content.Italic.Value) sb.Append("<i>");
            if (content.Underscore.HasValue && content.Underscore.Value) sb.Append("<u>");

            string font = string.IsNullOrWhiteSpace(content.Font) ? string.Empty : $"font-family: {content.Font};";
            string fontSize = content.FontSize.HasValue ? $"font-size: {content.FontSize}pt;" : string.Empty;
            string color = string.IsNullOrWhiteSpace(content.Colour) ? string.Empty : $"color:#{content.Colour.RgbToHex()};";

            sb.Append($"<span style='{font} {fontSize} {color}'>");
            sb.Append(string.IsNullOrWhiteSpace(content.Content) ? "&nbsp;" : HttpUtility.HtmlEncode(content.Content));
            sb.Append("</span>");

            if (content.Underscore.HasValue && content.Underscore.Value) sb.Append("</u>");
            if (content.Italic.HasValue && content.Italic.Value) sb.Append("</i>");
            if (content.Bold.HasValue && content.Bold.Value) sb.Append("</b>");
        }
    }
}
