using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using Origin.Converters;
using Origin.Interfaces;
using Origin.Model;

namespace Origin.OpenXml.Extensions
{
    public static class TableExtensions
    {
        public static void ConfigureTable(this Table tbl, DocumentParagraph documentParagraph, IDocumentProperties documentProperties)
        {
            ArgumentNullException.ThrowIfNull(documentParagraph);

            if(documentParagraph.Table == null) throw new NullReferenceException(nameof(documentParagraph.Table));

            List<DocumentTableColumn> documentTableColumns = [.. documentParagraph.Table.Columns.OrderBy(c => c.Position)];

            TableProperties tblPr = tbl.AppendChild(new TableProperties());
            TableWidth tblW = tblPr.AppendChild(new TableWidth());
            tblW.Type = TableWidthUnitValues.Dxa;

            tblW.Width = $"{documentTableColumns.Sum(tc => tc.Width ?? 0).ToTwips()}";

            TableGrid tblGrid = tbl.AppendChild(new TableGrid());

            foreach (DocumentTableColumn documentTableColumn in documentTableColumns)
            {
                tblGrid.AddGridColumn(documentTableColumn);
            }

            foreach (DocumentTableRow documentTableRow in documentParagraph.Table.Rows.OrderBy(r => r.Position))
            {
                TableRow tr = tbl.CreateTableRow(documentTableRow);

                for (int i = 0; i < documentTableColumns.Count; i++)
                {
                    DocumentTableCell? documentTableCell = documentParagraph.Table.Cells
                        .FirstOrDefault(c => c.Row == documentTableRow.Position && c.Column == documentTableColumns[i].Position);

                    string cellWidth = string.Empty;

                    if (documentTableCell == null) 
                    {
                        tr.AddTableCellNoContent(cellWidth);
                    }
                    else
                    {
                        if (documentTableColumns[i].Width.HasValue)
                        {
                            cellWidth = $"{documentTableColumns[i].Width.ToTwips()}";
                        }

                        TableCell tc = tr.CreateTableCell(documentTableCell, cellWidth);

                        if (documentTableCell.Contents.Count == 0)
                        {
                            tc.TableCellNoText();
                        }
                        else
                        {
                            foreach (DocumentContent documentContent in documentTableCell.Contents.OrderBy(c => c.Order))
                            {
                                if (documentContent.ContentType == DocumentContentType.Text)
                                {
                                    tc.AddTableCellText(documentContent, documentProperties);
                                }
                                else if (documentContent.ContentType == DocumentContentType.Image)
                                {
                                    tc.AddTableCellImage(documentContent);
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void AddGridColumn(this TableGrid tblGrid, DocumentTableColumn documentTableColumn)
        {
            ArgumentNullException.ThrowIfNull(documentTableColumn);

            GridColumn gridColumn = tblGrid.AppendChild(new GridColumn());

            if (documentTableColumn.Width.HasValue)
            {
                gridColumn.Width = $"{documentTableColumn.Width.ToTwips()}";
            }
        }

        public static TableRow CreateTableRow(this Table tbl, DocumentTableRow documentTableRow)
        {
            TableRow tr = tbl.AppendChild(new TableRow());
            TableRowProperties trPr = tr.AppendChild(new TableRowProperties());

            if(documentTableRow.Height.HasValue) 
            {
                TableRowHeight trH = trPr.AppendChild(new TableRowHeight());
                trH.Val = documentTableRow.Height.Value.ToUTwips();
            }

            return tr;
        }

        public static TableCell CreateTableCell(this TableRow tr, DocumentTableCell? documentTableCell, string cellWidth)
        {
            ArgumentNullException.ThrowIfNull(documentTableCell);
            ArgumentException.ThrowIfNullOrWhiteSpace(cellWidth);

            TableCell tc = tr.CreateTableCell(cellWidth);

            if (tc.TableCellProperties != null
                && (documentTableCell.BorderLeft.HasValue
                || documentTableCell.BorderTop.HasValue
                || documentTableCell.BorderRight.HasValue
                || documentTableCell.BorderBottom.HasValue))
            {
                TableCellBorders tableCellBorders = new();
                tc.TableCellProperties.AddChild(tableCellBorders);

                // Table borders are line borders (see the val attribute below), and so the width is specified in eighths of a point,
                // with a minimum value of two (1/4 of a point) and a maximum value of 96 (twelve points)
                // http://officeopenxml.com/WPtableBorders.php

                if (documentTableCell.BorderLeft.HasValue)
                {
                    LeftBorder leftBorder = new() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = new UInt32Value((uint)documentTableCell.BorderLeft.Value * 8) };

                    if (!string.IsNullOrWhiteSpace(documentTableCell.BorderLeftColour)) leftBorder.Color = new StringValue(documentTableCell.BorderLeftColour.RgbToHex());

                    tableCellBorders.AddChild(leftBorder);
                }

                if (documentTableCell.BorderTop.HasValue)
                {
                    TopBorder topBorder = new() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = new UInt32Value((uint)documentTableCell.BorderTop.Value * 8) };

                    if (!string.IsNullOrWhiteSpace(documentTableCell.BorderTopColour)) topBorder.Color = new StringValue(documentTableCell.BorderTopColour.RgbToHex());

                    tableCellBorders.AddChild(topBorder);
                }

                if (documentTableCell.BorderRight.HasValue)
                {
                    RightBorder rightBorder = new() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = new UInt32Value((uint)documentTableCell.BorderRight.Value * 8) };

                    if (!string.IsNullOrWhiteSpace(documentTableCell.BorderRightColour)) rightBorder.Color = new StringValue(documentTableCell.BorderRightColour.RgbToHex());

                    tableCellBorders.AddChild(rightBorder);
                }

                if (documentTableCell.BorderBottom.HasValue)
                {
                    BottomBorder bottomBorder = new() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = new UInt32Value((uint)documentTableCell.BorderBottom.Value * 8) };

                    if (!string.IsNullOrWhiteSpace(documentTableCell.BorderBottomColour)) bottomBorder.Color = new StringValue(documentTableCell.BorderBottomColour.RgbToHex());

                    tableCellBorders.AddChild(bottomBorder);
                }
            }

            if (tc.TableCellProperties != null
                && !string.IsNullOrWhiteSpace(documentTableCell.CellColour))
            {
                var shading = new Shading()
                {
                    Color = "auto",
                    Fill = documentTableCell.CellColour.RgbToHex(),
                    Val = ShadingPatternValues.Clear
                };

                tc.TableCellProperties.AddChild(shading);
            }

            return tc;
        }

        public static TableCell CreateTableCell(this TableRow tr, string cellWidth)
        {
            ArgumentNullException.ThrowIfNull(cellWidth);

            TableCell tc = new();
            TableCellProperties tcPr = tc.AppendChild(new TableCellProperties());

            if (!string.IsNullOrWhiteSpace(cellWidth))
            {
                TableCellWidth tcW = tcPr.AppendChild(new TableCellWidth());
                tcW.Width = cellWidth;
                tcW.Type = TableWidthUnitValues.Dxa;
            }

            tr.Append(tc);

            return tc;
        }

        public static void AddTableCellNoContent(this TableRow tr, string cellWidth)
        {
            ArgumentNullException.ThrowIfNull(cellWidth);

            TableCell tc = CreateTableCell(tr, cellWidth);
            tc.TableCellNoText();
        }

        public static void TableCellNoText(this TableCell tc)
        {
            // https://learn.microsoft.com/en-us/dotnet/api/documentformat.openxml.wordprocessing.tablecell?view=openxml-3.0.1
            // If a table cell does not include at least one block-level element, then this document shall be considered corrupt.

            Paragraph p = tc.AppendChild(new Paragraph());

            ParagraphProperties pPr = p.AppendChild(new ParagraphProperties());
            SpacingBetweenLines spacingBetweenLines = pPr.AppendChild(new SpacingBetweenLines());

            spacingBetweenLines.Before = "0";
            spacingBetweenLines.After = "0";
        }

        public static void AddTableCellImage(this TableCell tc, DocumentContent documentContent)
        {
            ArgumentNullException.ThrowIfNull(documentContent);

            Paragraph p = tc.AppendChild(new Paragraph());

            ParagraphProperties pPr = p.AppendChild(new ParagraphProperties());
            SpacingBetweenLines spacingBetweenLines = pPr.AppendChild(new SpacingBetweenLines());
            spacingBetweenLines.Before = "0";
            spacingBetweenLines.After = "0";

            Run r = p.AppendChild(new Run());
            r.AddImageElement(documentContent);
        }

        public static void AddTableCellText(this TableCell tc, DocumentContent documentContent, IDocumentProperties documentProperties)
        {
            ArgumentNullException.ThrowIfNull(documentContent);

            if (string.IsNullOrWhiteSpace(documentContent.Content)) return;

            Paragraph p = tc.AppendChild(new Paragraph());

            ParagraphProperties pPr = p.AppendChild(new ParagraphProperties());
            SpacingBetweenLines spacingBetweenLines = pPr.AppendChild(new SpacingBetweenLines());

            if (documentContent.IgnoreParapgraphSpacing.HasValue
                && documentContent.IgnoreParapgraphSpacing.Value)
            {
                spacingBetweenLines.Before = "0";
                spacingBetweenLines.After = "0";
            }
            else
            {
                spacingBetweenLines.Before = $"{documentProperties.ParagraphSpacingBetweenLinesBefore.ToTwips()}";
                spacingBetweenLines.After = $"{documentProperties.ParagraphSpacingBetweenLinesAfter.ToTwips()}";
            }

            pPr.Append(new Justification() { Val = documentContent.AlignContent.ToJustification() });

            p.AddText(documentContent);
        }
    }
}
