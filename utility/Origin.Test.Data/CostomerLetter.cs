using Origin.Core.Models;

namespace Origin.Test.Data
{
    public static class CostomerLetter
    {
        public static DocumentConfig GetCustomerDocumentArgs()
        {
            DocumentConfig documentConfig = new()
            {
                Name = "Customer Letter",
                SubstituteStart = "[",
                SubstituteEnd = "]",
                Substitutes = GetDocumentSubstitutes()
            };

            documentConfig.ConfigParagraphs =
            [
                new DocumentConfigParagraph()
                {
                    Order = 1,
                    DocumentParagraph = new DocumentParagraph
                    {
                        DocumentParagraphType = DocumentParagraphType.Footer,
                        Name = "CUSTOMER LETTER FOOTER",
                        FontSize = 8,
                        IgnoreParapgraphSpacing = true,
                        Contents =
                        [
                            new DocumentContent { Order = 1, Name = ContentNames.FOOTER_TEXT, Content = MockLetter.FOOTER_CONTENT }
                        ]
                    }
                },
                new DocumentConfigParagraph()
                {
                    Order = 2,
                    DocumentParagraph = new DocumentParagraph
                    {
                        DocumentParagraphType = DocumentParagraphType.Table,
                        Name = "CUSTOMER LETTER HEADING",
                        Rows =
                        [
                            new DocumentTableRow { Number = 1 },
                            new DocumentTableRow { Number = 2 },
                            new DocumentTableRow { Number = 3 }
                        ],
                        Columns =
                        [
                            new DocumentTableColumn { Number = 1, Width = 62 },
                            new DocumentTableColumn { Number = 2, Width = 63 },
                            new DocumentTableColumn { Number = 3, Width = 55 }
                        ],
                        Cells =
                        [
                            new DocumentTableCell{ RowNumber = 1, ColumnNumber = 1, CellCode = TableCellCodes.COMPANY_LOGO },
                            new DocumentTableCell{ RowNumber = 2, ColumnNumber = 3, CellCode = TableCellCodes.COMPANY_DETAILS },
                            new DocumentTableCell{ RowNumber = 3, ColumnNumber = 1, CellCode = TableCellCodes.CUSTOMER_DETAILS }
                        ],
                        Contents =
                        [
                            new DocumentContent { Order = 1, Name = ContentNames.LOGO, RenderCellCode = TableCellCodes.COMPANY_LOGO, ContentType = DocumentContentType.Image, Image = "logo.png", ImageHeight = 15, ImageWidth = 35 },
                            new DocumentContent { Order = 2, Name = ContentNames.COMPANY_NAME, RenderCellCode = TableCellCodes.COMPANY_DETAILS, Content = CustomerLetterContent.COMPANY_NAME_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 3, Name = ContentNames.COMPANY_ADDRESS_1, RenderCellCode = TableCellCodes.COMPANY_DETAILS, Content = CustomerLetterContent.COMPANY_ADDRESS_1_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 4, Name = ContentNames.COMPANY_ADDRESS_2, RenderCellCode = TableCellCodes.COMPANY_DETAILS, Content = CustomerLetterContent.COMPANY_ADDRESS_2_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 5, Name = ContentNames.COMPANY_ADDRESS_3, RenderCellCode = TableCellCodes.COMPANY_DETAILS, Content = CustomerLetterContent.COMPANY_ADDRESS_3_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 6, Name = ContentNames.COMPANY_PHONE_NUMBER, RenderCellCode = TableCellCodes.COMPANY_DETAILS, Content = CustomerLetterContent.COMPANY_PHONE_NUMBER_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 7, Name = ContentNames.COMPANY_EMAIL, RenderCellCode = TableCellCodes.COMPANY_DETAILS, Content = CustomerLetterContent.COMPANY_EMAIL_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 8, Name = ContentNames.CUSTOMER_NAME, RenderCellCode = TableCellCodes.CUSTOMER_DETAILS, Content = CustomerLetterContent.CUSTOMER_NAME_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 9, Name = ContentNames.CUSTOMER_ADDRESS_1, RenderCellCode = TableCellCodes.CUSTOMER_DETAILS, Content = CustomerLetterContent.CUSTOMER_ADDRESS_1_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 10, Name = ContentNames.CUSTOMER_ADDRESS_2, RenderCellCode = TableCellCodes.CUSTOMER_DETAILS, Content = CustomerLetterContent.CUSTOMER_ADDRESS_2_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 11, Name = ContentNames.CUSTOMER_ADDRESS_3, RenderCellCode = TableCellCodes.CUSTOMER_DETAILS, Content = CustomerLetterContent.CUSTOMER_ADDRESS_3_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 12, Name = ContentNames.CUSTOMER_ADDRESS_4, RenderCellCode = TableCellCodes.CUSTOMER_DETAILS, Content = CustomerLetterContent.CUSTOMER_ADDRESS_4_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 13, Name = ContentNames.CUSTOMER_ADDRESS_5, RenderCellCode = TableCellCodes.CUSTOMER_DETAILS, Content = CustomerLetterContent.CUSTOMER_ADDRESS_5_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 14, Name = ContentNames.CUSTOMER_ADDRESS_6, RenderCellCode = TableCellCodes.CUSTOMER_DETAILS, Content = CustomerLetterContent.CUSTOMER_ADDRESS_6_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 15, Name = ContentNames.CUSTOMER_ADDRESS_7, RenderCellCode = TableCellCodes.CUSTOMER_DETAILS, Content = CustomerLetterContent.CUSTOMER_ADDRESS_7_CONTENT, IgnoreParapgraphSpacing = true }
                        ]
                    }
                },
                new DocumentConfigParagraph()
                {
                    Order = 3,
                    DocumentParagraph = new DocumentParagraph
                    {
                        Name = "CUSTOMER LETTER TITLE",
                        Font = "Courier",
                        FontSize = 16,
                        Colour = "0,176,240",
                        Contents =
                        [
                            new DocumentContent { Order = 1, Name = ContentNames.LETTER_TITLE, Content = CustomerLetterContent.LETTER_TITLE_CONTENT }
                        ]
                    }
                },
                new DocumentConfigParagraph()
                {
                    Order = 4,
                    DocumentParagraph = new DocumentParagraph
                    {
                        Name = "CUSTOMER LETTER PARAGRAPH 1",
                        Font = "Courier",
                        FontSize = 11,
                        Contents =
                        [
                            new DocumentContent { Order = 20, Name = MockLetter.PARAGRAPH_1_TEXT, RenderCellCode = MockLetter.PARAGRAPH_1, Content = CustomerLetterContent.LOREM_IPSUM_IS_NONSENSE_MICROSOFT }
                        ]
                    }
                },
                new DocumentConfigParagraph()
                {
                    Order = 5,
                    DocumentParagraph = new DocumentParagraph
                    {
                        DocumentParagraphType = DocumentParagraphType.Table,
                        Name = "CUSTOMER LETTER PRODUCT TABLE",
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
                            new DocumentTableCell{ RowNumber = 1, ColumnNumber = 1, CellCode = CustomerLetterTableCellConstants.TABLE_ROW_1_COL_1_CODE, CellColour = "150,200,250", BorderLeft = 1, BorderLeftColour = "150,200,250", BorderTop = 1, BorderTopColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250", BorderBottom = 1, BorderBottomColour = "150,200,250" },
                            new DocumentTableCell{ RowNumber = 1, ColumnNumber = 2, CellCode = CustomerLetterTableCellConstants.TABLE_ROW_1_COL_2_CODE, CellColour = "150,200,250", BorderLeft = 1, BorderLeftColour = "150,200,250", BorderTop = 1, BorderTopColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250", BorderBottom = 1, BorderBottomColour = "150,200,250" },
                            new DocumentTableCell{ RowNumber = 2, ColumnNumber = 1, CellCode = CustomerLetterTableCellConstants.TABLE_ROW_2_COL_1_CODE, BorderLeft = 1, BorderLeftColour = "150,200,250", BorderTop = 1, BorderTopColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250" },
                            new DocumentTableCell{ RowNumber = 2, ColumnNumber = 2, CellCode = CustomerLetterTableCellConstants.TABLE_ROW_2_COL_2_CODE, BorderLeft = 1, BorderLeftColour = "150,200,250", BorderTop = 1, BorderTopColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250" },
                            new DocumentTableCell{ RowNumber = 3, ColumnNumber = 1, CellCode = CustomerLetterTableCellConstants.TABLE_ROW_3_COL_1_CODE, BorderLeft = 1, BorderLeftColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250" },
                            new DocumentTableCell{ RowNumber = 3, ColumnNumber = 2, CellCode = CustomerLetterTableCellConstants.TABLE_ROW_3_COL_2_CODE, BorderLeft = 1, BorderLeftColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250" },
                            new DocumentTableCell{ RowNumber = 4, ColumnNumber = 1, CellCode = CustomerLetterTableCellConstants.TABLE_ROW_4_COL_1_CODE, BorderLeft = 1, BorderLeftColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250" },
                            new DocumentTableCell{ RowNumber = 4, ColumnNumber = 2, CellCode = CustomerLetterTableCellConstants.TABLE_ROW_4_COL_2_CODE, BorderLeft = 1, BorderLeftColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250" },
                            new DocumentTableCell{ RowNumber = 5, ColumnNumber = 1, CellCode = CustomerLetterTableCellConstants.TABLE_ROW_5_COL_1_CODE, BorderLeft = 1, BorderLeftColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250" },
                            new DocumentTableCell{ RowNumber = 5, ColumnNumber = 2, CellCode = CustomerLetterTableCellConstants.TABLE_ROW_5_COL_2_CODE, BorderLeft = 1, BorderLeftColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250" },
                            new DocumentTableCell{ RowNumber = 6, ColumnNumber = 1, CellCode = CustomerLetterTableCellConstants.TABLE_ROW_6_COL_1_CODE, BorderLeft = 1, BorderLeftColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250", BorderBottom = 1, BorderBottomColour = "150,200,250" },
                            new DocumentTableCell{ RowNumber = 6, ColumnNumber = 2, CellCode = CustomerLetterTableCellConstants.TABLE_ROW_6_COL_2_CODE, BorderLeft = 1, BorderLeftColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250", BorderBottom = 1, BorderBottomColour = "150,200,250" }
                        ],
                        Contents =
                        [
                            new DocumentContent { Order = 1, Name = CustomerLetterContent.TABLE_ROW_1_COL_1_CONTENT, RenderCellCode = CustomerLetterTableCellConstants.TABLE_ROW_1_COL_1_CODE, Content = CustomerLetterContent.TABLE_SUMMARY_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 2, Name = CustomerLetterContent.TABLE_ROW_2_COL_1_CONTENT, RenderCellCode = CustomerLetterTableCellConstants.TABLE_ROW_2_COL_1_CODE, Content = CustomerLetterContent.TABLE_ROW_2_COL_1_TEXT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 3, Name = CustomerLetterContent.TABLE_ROW_2_COL_2_CONTENT, RenderCellCode = CustomerLetterTableCellConstants.TABLE_ROW_2_COL_2_CODE, Content = CustomerLetterContent.TABLE_ROW_2_COL_2_TEXT, AlignContent = DocumentContentAlign.End, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 4, Name = CustomerLetterContent.TABLE_ROW_3_COL_1_CONTENT, RenderCellCode = CustomerLetterTableCellConstants.TABLE_ROW_3_COL_1_CODE, Content = CustomerLetterContent.TABLE_ROW_3_COL_1_TEXT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 5, Name = CustomerLetterContent.TABLE_ROW_3_COL_2_CONTENT, RenderCellCode = CustomerLetterTableCellConstants.TABLE_ROW_3_COL_2_CODE, Content = CustomerLetterContent.TABLE_ROW_3_COL_2_TEXT, AlignContent = DocumentContentAlign.End, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 6, Name = CustomerLetterContent.TABLE_ROW_4_COL_1_CONTENT, RenderCellCode = CustomerLetterTableCellConstants.TABLE_ROW_4_COL_1_CODE, Content = CustomerLetterContent.TABLE_ROW_4_COL_1_TEXT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 7, Name = CustomerLetterContent.TABLE_ROW_4_COL_2_CONTENT, RenderCellCode = CustomerLetterTableCellConstants.TABLE_ROW_4_COL_2_CODE, Content = CustomerLetterContent.TABLE_ROW_4_COL_2_TEXT, AlignContent = DocumentContentAlign.End, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 8, Name = CustomerLetterContent.TABLE_ROW_5_COL_1_CONTENT, RenderCellCode = CustomerLetterTableCellConstants.TABLE_ROW_5_COL_1_CODE, Content = CustomerLetterContent.TABLE_ROW_5_COL_1_TEXT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 9, Name = CustomerLetterContent.TABLE_ROW_5_COL_2_CONTENT, RenderCellCode = CustomerLetterTableCellConstants.TABLE_ROW_5_COL_2_CODE, Content = CustomerLetterContent.TABLE_ROW_5_COL_2_TEXT, AlignContent = DocumentContentAlign.End, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 10, Name = CustomerLetterContent.TABLE_ROW_6_COL_1_CONTENT, RenderCellCode = CustomerLetterTableCellConstants.TABLE_ROW_6_COL_1_CODE, Content = CustomerLetterContent.TABLE_ROW_6_COL_1_TEXT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 11, Name = CustomerLetterContent.TABLE_ROW_6_COL_2_CONTENT, RenderCellCode = CustomerLetterTableCellConstants.TABLE_ROW_6_COL_2_CODE, Content = CustomerLetterContent.TABLE_ROW_6_COL_2_TEXT, AlignContent = DocumentContentAlign.End, IgnoreParapgraphSpacing = true }
                        ]
                    }
                },
                new DocumentConfigParagraph()
                {
                    Order = 6,
                    DocumentParagraph = new DocumentParagraph
                    {
                        DocumentParagraphType = DocumentParagraphType.Table,
                        Name = "CUSTOMER LETTER CLOSE",
                        Rows =
                        [
                            new DocumentTableRow { Number = 1 }
                        ],
                        Columns =
                        [
                            new DocumentTableColumn { Number = 1, Width = 35 }
                        ],
                        Cells =
                        [
                            new DocumentTableCell { RowNumber = 1, ColumnNumber = 1, CellCode = TableCellCodes.SIGNATURE_DETAILS }
                        ],
                        Contents =
                        [
                            new DocumentContent { Order = 1, Name = ContentNames.SIGNATURE, RenderCellCode = TableCellCodes.SIGNATURE_DETAILS, ContentType = DocumentContentType.Image, Image = "signature.png", ImageHeight = 15, ImageWidth = 35 },
                            new DocumentContent { Order = 2, Name = ContentNames.SIGNEE, RenderCellCode = TableCellCodes.SIGNATURE_DETAILS, Content = MockLetter.SIGNEE_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 3, Name = ContentNames.SIGNEE_TITLE, RenderCellCode = TableCellCodes.SIGNATURE_DETAILS, Content = MockLetter.SIGNEE_TITLE_CONTENT, IgnoreParapgraphSpacing = true }
                        ]
                    }
                }
            ];

            return documentConfig;
        }

        public static List<DocumentSubstitute> GetDocumentSubstitutes()
        {
            List<DocumentSubstitute> documentSubstitutes = [];

            documentSubstitutes.Add(new DocumentSubstitute { Key = CustomerLetterSubstitutes.CUSTOMER_ID });
            documentSubstitutes.Add(new DocumentSubstitute { Key = CustomerLetterSubstitutes.CUSTOMER_TITLE });
            documentSubstitutes.Add(new DocumentSubstitute { Key = CustomerLetterSubstitutes.CUSTOMER_FIRST_NAME });
            documentSubstitutes.Add(new DocumentSubstitute { Key = CustomerLetterSubstitutes.CUSTOMER_SURNAME });
            documentSubstitutes.Add(new DocumentSubstitute { Key = CustomerLetterSubstitutes.CUSTOMER_ADDRESS_1, Group = CustomerLetterSubstitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = CustomerLetterSubstitutes.CUSTOMER_ADDRESS_2, Group = CustomerLetterSubstitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = CustomerLetterSubstitutes.CUSTOMER_ADDRESS_3, Group = CustomerLetterSubstitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = CustomerLetterSubstitutes.CUSTOMER_ADDRESS_4, Group = CustomerLetterSubstitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = CustomerLetterSubstitutes.CUSTOMER_ADDRESS_5, Group = CustomerLetterSubstitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = CustomerLetterSubstitutes.CUSTOMER_ADDRESS_6, Group = CustomerLetterSubstitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = CustomerLetterSubstitutes.CUSTOMER_ADDRESS_7, Group = CustomerLetterSubstitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = CustomerLetterSubstitutes.CUSTOMER_SORT_CODE });
            documentSubstitutes.Add(new DocumentSubstitute { Key = CustomerLetterSubstitutes.CUSTOMER_ACCOUNT_NUMBER });

            documentSubstitutes.Add(new DocumentSubstitute { Key = CustomerLetterSubstitutes.COMPANY_NAME });
            documentSubstitutes.Add(new DocumentSubstitute { Key = CustomerLetterSubstitutes.COMPANY_ADDRESS_1 });
            documentSubstitutes.Add(new DocumentSubstitute { Key = CustomerLetterSubstitutes.COMPANY_ADDRESS_2 });
            documentSubstitutes.Add(new DocumentSubstitute { Key = CustomerLetterSubstitutes.COMPANY_ADDRESS_3 });
            documentSubstitutes.Add(new DocumentSubstitute { Key = CustomerLetterSubstitutes.COMPANY_PHONE_NUMBER });
            documentSubstitutes.Add(new DocumentSubstitute { Key = CustomerLetterSubstitutes.COMPANY_EMAIL });
            documentSubstitutes.Add(new DocumentSubstitute { Key = CustomerLetterSubstitutes.SIGNEE });
            documentSubstitutes.Add(new DocumentSubstitute { Key = CustomerLetterSubstitutes.SIGNEE_TITLE });

            //if (!templateOnly)
            //{
            //    documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_2, Value = skipSubValues ? null : "\"Lorem ipsum dolor sit amet consectetuer\"" });
            //    documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_4, Value = skipSubValues ? null : "Actually, it is nonsense" });
            //    documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_6, Value = skipSubValues ? null : "The phrase has been used for several centuries by typographers" });
            //    documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.TABLE_SUMMARY, Value = skipSubValues ? null : "Present Test Data in Table" });
            //    documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.TABLE_LINE_1, Value = skipSubValues ? null : "Line item no. 1" });
            //    documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.TABLE_LINE_1_AMOUNT, Value = skipSubValues ? null : "£70" });
            //    documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.TABLE_LINE_2, Value = skipSubValues ? null : "Line item no. 2" });
            //    documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.TABLE_LINE_2_AMOUNT, Value = skipSubValues ? null : "£30" });
            //    documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.TABLE_TOTAL, Value = skipSubValues ? null : "Total" });
            //    documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.TABLE_TOTAL_AMOUNT, Value = skipSubValues ? null : "£100" });
            //}

            return documentSubstitutes;
        }
    }
}
