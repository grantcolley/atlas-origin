using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using Origin.Core.Converters;
using Origin.Core.Interfaces;
using Origin.Core.Models;
using System.Resources;

namespace Origin.PdfSharp.Extensions
{
    public static class TableExtensions
    {
        public static void ConfigureTable(this Table table, DocumentParagraph documentParagraph)
        {
            ArgumentNullException.ThrowIfNull(documentParagraph);

            // https://stackoverflow.com/questions/24986424/how-to-size-a-table-to-the-page-width-in-migradoc

            List<DocumentTableColumn> documentTableColumns = [.. documentParagraph.Columns.OrderBy(c => c.Number)];

            foreach (DocumentTableColumn documentTableColumn in documentTableColumns)
            {
                Column column = table.AddColumn();

                if(documentTableColumn.Width != null) 
                {
                    column.Width = Unit.FromMillimeter(documentTableColumn.Width.Value);
                }
            }

            foreach (DocumentTableRow documentTableRow in documentParagraph.Rows.OrderBy(r => r.Number))
            {
                Row row = table.AddRow();

                if(documentTableRow.Height != null) 
                {
                    row.Height = Unit.FromMillimeter(documentTableRow.Height.Value);
                }

                for (int i = 0; i < documentTableColumns.Count; i++)
                {
                    DocumentTableCell? documentTableCell = documentParagraph.Cells
                        .FirstOrDefault(c => c.RowNumber == documentTableRow.Number && c.ColumnNumber == documentTableColumns[i].Number);

                    if(documentTableCell != null) 
                    {
                        Cell cell = row.Cells[i];

                        cell.SetTableCellProperties(documentTableCell);

                        foreach (DocumentContent documentContent in documentTableCell.Contents.OrderBy(c => c.Order))
                        {
                            if (documentContent.ContentType == DocumentContentType.Text)
                            {
                                cell.AddTableCellText(documentContent, documentParagraph);
                            }
                            else if (documentContent.ContentType == DocumentContentType.Image)
                            {
                                cell.AddTableCellImage(documentContent);
                            }
                        }
                    }
                }
            }
        }

        public static void SetTableCellProperties(this Cell cell, DocumentTableCell documentTableCell)
        {
            if (documentTableCell.BorderLeft.HasValue
                || documentTableCell.BorderTop.HasValue
                || documentTableCell.BorderRight.HasValue
                || documentTableCell.BorderBottom.HasValue)
            {
                if (documentTableCell.BorderLeft.HasValue)
                {
                    cell.Borders.Left.Width = Unit.FromPoint(documentTableCell.BorderLeft.Value);

                    if (!string.IsNullOrWhiteSpace(documentTableCell.BorderLeftColour))
                    {
                        byte[] rgb = documentTableCell.BorderLeftColour.RgbToByteArray();
                        cell.Borders.Left.Color = new Color(rgb[0], rgb[1], rgb[2]);
                    }
                }

                if (documentTableCell.BorderTop.HasValue)
                {
                    cell.Borders.Top.Width = Unit.FromPoint(documentTableCell.BorderTop.Value);

                    if (!string.IsNullOrWhiteSpace(documentTableCell.BorderTopColour))
                    {
                        byte[] rgb = documentTableCell.BorderTopColour.RgbToByteArray();
                        cell.Borders.Top.Color = new Color(rgb[0], rgb[1], rgb[2]);
                    }
                }

                if (documentTableCell.BorderRight.HasValue)
                {
                    cell.Borders.Right.Width = Unit.FromPoint(documentTableCell.BorderRight.Value);

                    if (!string.IsNullOrWhiteSpace(documentTableCell.BorderRightColour))
                    {
                        byte[] rgb = documentTableCell.BorderRightColour.RgbToByteArray();
                        cell.Borders.Right.Color = new Color(rgb[0], rgb[1], rgb[2]);
                    }
                }

                if (documentTableCell.BorderBottom.HasValue)
                {
                    cell.Borders.Bottom.Width = Unit.FromPoint(documentTableCell.BorderBottom.Value);

                    if (!string.IsNullOrWhiteSpace(documentTableCell.BorderBottomColour))
                    {
                        byte[] rgb = documentTableCell.BorderBottomColour.RgbToByteArray();
                        cell.Borders.Bottom.Color = new Color(rgb[0], rgb[1], rgb[2]);
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(documentTableCell.CellColour))
            {
                byte[] rgb = documentTableCell.CellColour.RgbToByteArray();
                cell.Shading.Color = new Color(rgb[0], rgb[1], rgb[2]);
            }
        }

        public static void AddTableCellImage(this Cell cell, DocumentContent documentContent)
        {
            ArgumentNullException.ThrowIfNull(documentContent);

            if (string.IsNullOrWhiteSpace(documentContent.Image)) return;

            Paragraph p = cell.AddParagraph();

            Image image = p.AddImage(Resources.ResourceManager.GetPngAsBase64(documentContent.Image));

            if (documentContent.ImageHeight.HasValue)
            {
                image.Height = Unit.FromMillimeter(documentContent.ImageHeight.Value);
            }

            if (documentContent.ImageWidth.HasValue)
            {
                image.Width = Unit.FromMillimeter(documentContent.ImageWidth.Value);
            }
        }

        public static void AddTableCellText(this Cell cell, DocumentContent documentContent, IDocumentParagraphProperties documentParagraphProperties)
        {
            ArgumentNullException.ThrowIfNull(documentContent);

            if (string.IsNullOrWhiteSpace(documentContent.Content)) return;

            Paragraph p = cell.AddParagraph();

            if (!documentContent.IgnoreParapgraphSpacing.HasValue
                || !documentContent.IgnoreParapgraphSpacing.Value)
            {
                p.Format.SpaceBefore = Unit.FromMillimeter(documentParagraphProperties.ParagraphSpacingBetweenLinesBefore);
                p.Format.SpaceAfter = Unit.FromMillimeter(documentParagraphProperties.ParagraphSpacingBetweenLinesAfter);
            }

            p.Format.Alignment = documentContent.AlignContent.ToJustification();

            p.AddText(documentContent);
        }
    }
}
