using Origin.Core.Extensions;
using Origin.Core.Models;

namespace Origin.Test.Data
{
    public static class CustomerProductLetter
    {
        public static DocumentConfig BuildCustomerProduct(DocumentConfig cloneConfig)
        {

            DocumentConfig documentConfig = cloneConfig.Clone();
            documentConfig.Name = "Customer Product Letter";
            documentConfig.Substitutes.AddRange(GetDocumentSubstitutes());

            documentConfig.ConfigParagraphs.Add(new DocumentConfigParagraph()
            {
                Order = 3,
                DocumentParagraph = new DocumentParagraph
                {
                    Name = "Customer Product Letter Paragraph",
                    Font = "Courier",
                    FontSize = 11,
                    Contents =
                        [
                            new DocumentContent { Order = 20, Name = MockLetter.PARAGRAPH_1_TEXT, RenderCellCode = MockLetter.PARAGRAPH_1, Content = CustomerProductLetterContent.LOREM_IPSUM_IS_NONSENSE_MICROSOFT }
                        ]
                }
            });

            documentConfig.ConfigParagraphs.Add(new DocumentConfigParagraph()
            {
                Order = 4,
                DocumentParagraph = new DocumentParagraph
                {
                    DocumentParagraphType = DocumentParagraphType.Table,
                    Name = "Customer Product Letter Product Table",
                    Rows =
                        [
                            new DocumentTableRow { Number = 1 },
                            new DocumentTableRow { Number = 2 },
                            new DocumentTableRow { Number = 3 },
                            new DocumentTableRow { Number = 4 },
                            new DocumentTableRow { Number = 5 },
                            new DocumentTableRow { Number = 6 }
                        ],
                    Columns =
                        [
                            new DocumentTableColumn { Number = 1, Width = 160 },
                            new DocumentTableColumn { Number = 2, Width = 30 }
                        ],
                    Cells =
                        [
                            new DocumentTableCell{ RowNumber = 1, ColumnNumber = 1, CellCode = CustomerProductLetterTableCellConstants.TABLE_ROW_1_COL_1_CODE, CellColour = "150,200,250", BorderLeft = 1, BorderLeftColour = "150,200,250", BorderTop = 1, BorderTopColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250", BorderBottom = 1, BorderBottomColour = "150,200,250" },
                            new DocumentTableCell{ RowNumber = 1, ColumnNumber = 2, CellCode = CustomerProductLetterTableCellConstants.TABLE_ROW_1_COL_2_CODE, CellColour = "150,200,250", BorderLeft = 1, BorderLeftColour = "150,200,250", BorderTop = 1, BorderTopColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250", BorderBottom = 1, BorderBottomColour = "150,200,250" },
                            new DocumentTableCell{ RowNumber = 2, ColumnNumber = 1, CellCode = CustomerProductLetterTableCellConstants.TABLE_ROW_2_COL_1_CODE, BorderLeft = 1, BorderLeftColour = "150,200,250", BorderTop = 1, BorderTopColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250" },
                            new DocumentTableCell{ RowNumber = 2, ColumnNumber = 2, CellCode = CustomerProductLetterTableCellConstants.TABLE_ROW_2_COL_2_CODE, BorderLeft = 1, BorderLeftColour = "150,200,250", BorderTop = 1, BorderTopColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250" },
                            new DocumentTableCell{ RowNumber = 3, ColumnNumber = 1, CellCode = CustomerProductLetterTableCellConstants.TABLE_ROW_3_COL_1_CODE, BorderLeft = 1, BorderLeftColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250" },
                            new DocumentTableCell{ RowNumber = 3, ColumnNumber = 2, CellCode = CustomerProductLetterTableCellConstants.TABLE_ROW_3_COL_2_CODE, BorderLeft = 1, BorderLeftColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250" },
                            new DocumentTableCell{ RowNumber = 4, ColumnNumber = 1, CellCode = CustomerProductLetterTableCellConstants.TABLE_ROW_4_COL_1_CODE, BorderLeft = 1, BorderLeftColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250" },
                            new DocumentTableCell{ RowNumber = 4, ColumnNumber = 2, CellCode = CustomerProductLetterTableCellConstants.TABLE_ROW_4_COL_2_CODE, BorderLeft = 1, BorderLeftColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250" },
                            new DocumentTableCell{ RowNumber = 5, ColumnNumber = 1, CellCode = CustomerProductLetterTableCellConstants.TABLE_ROW_5_COL_1_CODE, BorderLeft = 1, BorderLeftColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250" },
                            new DocumentTableCell{ RowNumber = 5, ColumnNumber = 2, CellCode = CustomerProductLetterTableCellConstants.TABLE_ROW_5_COL_2_CODE, BorderLeft = 1, BorderLeftColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250" },
                            new DocumentTableCell{ RowNumber = 6, ColumnNumber = 1, CellCode = CustomerProductLetterTableCellConstants.TABLE_ROW_6_COL_1_CODE, BorderLeft = 1, BorderLeftColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250", BorderBottom = 1, BorderBottomColour = "150,200,250" },
                            new DocumentTableCell{ RowNumber = 6, ColumnNumber = 2, CellCode = CustomerProductLetterTableCellConstants.TABLE_ROW_6_COL_2_CODE, BorderLeft = 1, BorderLeftColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250", BorderBottom = 1, BorderBottomColour = "150,200,250" }
                        ],
                    Contents =
                        [
                            new DocumentContent { Order = 1, Name = CustomerProductLetterContent.TABLE_ROW_1_COL_1_CONTENT, RenderCellCode = CustomerProductLetterTableCellConstants.TABLE_ROW_1_COL_1_CODE, Content = CustomerProductLetterContent.TABLE_SUMMARY_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 2, Name = CustomerProductLetterContent.TABLE_ROW_2_COL_1_CONTENT, RenderCellCode = CustomerProductLetterTableCellConstants.TABLE_ROW_2_COL_1_CODE, Content = CustomerProductLetterContent.TABLE_ROW_2_COL_1_TEXT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 3, Name = CustomerProductLetterContent.TABLE_ROW_2_COL_2_CONTENT, RenderCellCode = CustomerProductLetterTableCellConstants.TABLE_ROW_2_COL_2_CODE, Content = CustomerProductLetterContent.TABLE_ROW_2_COL_2_TEXT, AlignContent = DocumentContentAlign.End, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 4, Name = CustomerProductLetterContent.TABLE_ROW_3_COL_1_CONTENT, RenderCellCode = CustomerProductLetterTableCellConstants.TABLE_ROW_3_COL_1_CODE, Content = CustomerProductLetterContent.TABLE_ROW_3_COL_1_TEXT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 5, Name = CustomerProductLetterContent.TABLE_ROW_3_COL_2_CONTENT, RenderCellCode = CustomerProductLetterTableCellConstants.TABLE_ROW_3_COL_2_CODE, Content = CustomerProductLetterContent.TABLE_ROW_3_COL_2_TEXT, AlignContent = DocumentContentAlign.End, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 6, Name = CustomerProductLetterContent.TABLE_ROW_4_COL_1_CONTENT, RenderCellCode = CustomerProductLetterTableCellConstants.TABLE_ROW_4_COL_1_CODE, Content = CustomerProductLetterContent.TABLE_ROW_4_COL_1_TEXT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 7, Name = CustomerProductLetterContent.TABLE_ROW_4_COL_2_CONTENT, RenderCellCode = CustomerProductLetterTableCellConstants.TABLE_ROW_4_COL_2_CODE, Content = CustomerProductLetterContent.TABLE_ROW_4_COL_2_TEXT, AlignContent = DocumentContentAlign.End, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 8, Name = CustomerProductLetterContent.TABLE_ROW_5_COL_1_CONTENT, RenderCellCode = CustomerProductLetterTableCellConstants.TABLE_ROW_5_COL_1_CODE, Content = CustomerProductLetterContent.TABLE_ROW_5_COL_1_TEXT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 9, Name = CustomerProductLetterContent.TABLE_ROW_5_COL_2_CONTENT, RenderCellCode = CustomerProductLetterTableCellConstants.TABLE_ROW_5_COL_2_CODE, Content = CustomerProductLetterContent.TABLE_ROW_5_COL_2_TEXT, AlignContent = DocumentContentAlign.End, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 10, Name = CustomerProductLetterContent.TABLE_ROW_6_COL_1_CONTENT, RenderCellCode = CustomerProductLetterTableCellConstants.TABLE_ROW_6_COL_1_CODE, Content = CustomerProductLetterContent.TABLE_ROW_6_COL_1_TEXT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 11, Name = CustomerProductLetterContent.TABLE_ROW_6_COL_2_CONTENT, RenderCellCode = CustomerProductLetterTableCellConstants.TABLE_ROW_6_COL_2_CODE, Content = CustomerProductLetterContent.TABLE_ROW_6_COL_2_TEXT, AlignContent = DocumentContentAlign.End, IgnoreParapgraphSpacing = true }
                        ]
                }
            });

            return documentConfig;
        }

        private static List<DocumentSubstitute> GetDocumentSubstitutes()
        {
            List<DocumentSubstitute> documentSubstitutes = [];
            documentSubstitutes.Add(new DocumentSubstitute { Key = CustomerProductLetterSubstitutes.TABLE_PRODUCT_NAME });
            documentSubstitutes.Add(new DocumentSubstitute { Key = CustomerProductLetterSubstitutes.TABLE_PRODUCT_STARTDATE });
            documentSubstitutes.Add(new DocumentSubstitute { Key = CustomerProductLetterSubstitutes.TABLE_PRODUCT_DURATION });
            documentSubstitutes.Add(new DocumentSubstitute { Key = CustomerProductLetterSubstitutes.TABLE_PRODUCT_RATE });
            documentSubstitutes.Add(new DocumentSubstitute { Key = CustomerProductLetterSubstitutes.TABLE_PRODUCT_VALUE });
            return documentSubstitutes;
        }
    }
}
