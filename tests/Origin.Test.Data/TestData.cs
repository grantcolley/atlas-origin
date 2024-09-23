using Origin.Core.Models;

namespace Origin.Test.Data
{
    public static class TestData
    {
        public static DocumentConfig GetDocumentConfigOpenXml(string outputLocation)
        {
            DocumentConfig args = GetDocumentArgs(outputLocation);
            args.DocumentServiceType = DocumentServiceType.OpenXmlDocument;
            return args;
        }

        public static DocumentConfig GetDocumentArgsConfigSharp(string outputLocation)
        {
            DocumentConfig args = GetDocumentArgs(outputLocation);
            args.DocumentServiceType = DocumentServiceType.PdfSharp;
            return args;
        }

        public static DocumentConfig GetDocumentArgs(string outputLocation)
        {
            DocumentConfig documentConfig = new()
            {
                SubstituteStart = "[",
                SubstituteEnd = "]",
                OutputLocation = outputLocation,
                FilenameTemplate = $"TestDocx_[{Substitutes.CUSTOMER_ID}]",
                Substitutes = GetDocumentSubstitutes()
            };

            documentConfig.Paragraphs.Add(new DocumentParagraph
            {
                DocumentParagraphId = 1,
                Order = 0,
                DocumentParagraphType = DocumentParagraphType.Footer,
                Name = ContentCodes.FOOTER_PARAGRAPH,
                FontSize = 10,
                IgnoreParapgraphSpacing = true,
                Contents =
                [
                    new DocumentContent { DocumentContentId = 32, Order = 32, Code = ContentCodes.FOOTER_TEXT, RenderCellCode = ContentCodes.FOOTER_PARAGRAPH, Content = MockLetter.FOOTER_CONTENT }
                ]
            });

            documentConfig.Paragraphs.Add(new DocumentParagraph
            {
                DocumentParagraphId = 2,
                Order = 1,
                DocumentParagraphType = DocumentParagraphType.Table,
                Name = ContentCodes.LETTER_HEAD_TABLE,
                Rows =
                [
                    new DocumentTableRow { DocumentTableRowId = 1, Number = 1 },
                    new DocumentTableRow { DocumentTableRowId = 2, Number = 2 },
                    new DocumentTableRow { DocumentTableRowId = 3, Number = 3 }
                ],
                Columns =
                [
                    new DocumentTableColumn { DocumentTableColumnId = 1, Number = 1, Width = 62 },
                    new DocumentTableColumn { DocumentTableColumnId = 2, Number = 2, Width = 63 },
                    new DocumentTableColumn { DocumentTableColumnId = 3, Number = 3, Width = 55 }
                ],
                Cells =
                [
                    new DocumentTableCell{ DocumentTableCellId = 1, RowNumber = 1, ColumnNumber = 1, Code = ContentCodes.COMPANY_LOGO },
                    new DocumentTableCell{ DocumentTableCellId = 2, RowNumber = 2, ColumnNumber = 3, Code = ContentCodes.COMPANY_DETAILS },
                    new DocumentTableCell{ DocumentTableCellId = 3, RowNumber = 3, ColumnNumber = 1, Code = ContentCodes.CUSTOMER_DETAILS }
                ],
                Contents = 
                [
                    new DocumentContent { DocumentContentId = 1, Order = 1, Code = ContentCodes.LOGO, RenderCellCode = ContentCodes.COMPANY_LOGO, ContentType = DocumentContentType.Image, Name = "Logo", Source = Path.Combine(outputLocation, "logo.png"), ImageHeight = 15, ImageWidth = 35 },
                    new DocumentContent { DocumentContentId = 2, Order = 2, Code = ContentCodes.COMPANY_NAME, RenderCellCode = ContentCodes.COMPANY_DETAILS, Content = MockLetter.COMPANY_NAME_CONTENT, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 3, Order = 3, Code = ContentCodes.COMPANY_ADDRESS_1, RenderCellCode = ContentCodes.COMPANY_DETAILS, Content = MockLetter.COMPANY_ADDRESS_1_CONTENT, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 4, Order = 4, Code = ContentCodes.COMPANY_ADDRESS_2, RenderCellCode = ContentCodes.COMPANY_DETAILS, Content = MockLetter.COMPANY_ADDRESS_2_CONTENT, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 5, Order = 5, Code = ContentCodes.COMPANY_ADDRESS_3, RenderCellCode = ContentCodes.COMPANY_DETAILS, Content = MockLetter.COMPANY_ADDRESS_3_CONTENT, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 6, Order = 6, Code = ContentCodes.COMPANY_PHONE_NUMBER, RenderCellCode = ContentCodes.COMPANY_DETAILS, Content = MockLetter.COMPANY_PHONE_NUMBER_CONTENT, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 7, Order = 7, Code = ContentCodes.COMPANY_EMAIL, RenderCellCode = ContentCodes.COMPANY_DETAILS, Content = MockLetter.COMPANY_EMAIL_CONTENT, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 8, Order = 8, Code = ContentCodes.CUSTOMER_NAME, RenderCellCode = ContentCodes.CUSTOMER_DETAILS, Content = MockLetter.CUSTOMER_NAME_CONTENT, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 9, Order = 9, Code = ContentCodes.CUSTOMER_ADDRESS_1, RenderCellCode = ContentCodes.CUSTOMER_DETAILS, Content = MockLetter.CUSTOMER_ADDRESS_1_CONTENT, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 10, Order = 10, Code = ContentCodes.CUSTOMER_ADDRESS_2, RenderCellCode = ContentCodes.CUSTOMER_DETAILS, Content = MockLetter.CUSTOMER_ADDRESS_2_CONTENT, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 11, Order = 11, Code = ContentCodes.CUSTOMER_ADDRESS_3, RenderCellCode = ContentCodes.CUSTOMER_DETAILS, Content = MockLetter.CUSTOMER_ADDRESS_3_CONTENT, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 12, Order = 12, Code = ContentCodes.CUSTOMER_ADDRESS_4, RenderCellCode = ContentCodes.CUSTOMER_DETAILS, Content = MockLetter.CUSTOMER_ADDRESS_4_CONTENT, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 13, Order = 13, Code = ContentCodes.CUSTOMER_ADDRESS_5, RenderCellCode = ContentCodes.CUSTOMER_DETAILS, Content = MockLetter.CUSTOMER_ADDRESS_5_CONTENT, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 14, Order = 14, Code = ContentCodes.CUSTOMER_ADDRESS_6, RenderCellCode = ContentCodes.CUSTOMER_DETAILS, Content = MockLetter.CUSTOMER_ADDRESS_6_CONTENT, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 15, Order = 15, Code = ContentCodes.CUSTOMER_ADDRESS_7, RenderCellCode = ContentCodes.CUSTOMER_DETAILS, Content = MockLetter.CUSTOMER_ADDRESS_7_CONTENT, IgnoreParapgraphSpacing = true }
                ]
            });

            documentConfig.Paragraphs.Add(new DocumentParagraph
            {
                DocumentParagraphId = 3,
                Order = 2,
                Name = ContentCodes.LETTER_TITLE_PARAGRAPH,
                Font = "Courier",
                FontSize = 16,
                Colour = "0,176,240",
                Contents = 
                [
                    new DocumentContent { DocumentContentId = 16, Order = 16, Code = ContentCodes.LETTER_TITLE, RenderCellCode = ContentCodes.LETTER_TITLE_PARAGRAPH, Content = MockLetter.LETTER_TITLE_CONTENT }
                ]
            });

            documentConfig.Paragraphs.Add(new DocumentParagraph
            {
                DocumentParagraphId = 4,
                Order = 3,
                Name = MockLetter.PARAGRAPH_1,
                Font = "Courier",
                FontSize = 11,
                Contents = 
                [
                    new DocumentContent { DocumentContentId = 20, Order = 20, Code = MockLetter.PARAGRAPH_1_TEXT, RenderCellCode = MockLetter.PARAGRAPH_1, Content = MockLetter.LOREM_IPSUM_STANDARD_1500 }
                ]
            });

            documentConfig.Paragraphs.Add(new DocumentParagraph
            {
                DocumentParagraphId = 5,
                Order = 4,
                Name = MockLetter.PARAGRAPH_2,
                Font = "Times New Roman",
                FontSize = 12,
                Colour = "65,105,225",
                Contents =
                [
                    new DocumentContent { DocumentContentId = 21, Order = 21, Code = MockLetter.PARAGRAPH_2_TEXT_1, RenderCellCode = MockLetter.PARAGRAPH_2, Content = MockLetter.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_1_CONTENT },
                    new DocumentContent { DocumentContentId = 22, Order = 22, Code = MockLetter.PARAGRAPH_2_TEXT_2, RenderCellCode = MockLetter.PARAGRAPH_2, Content = MockLetter.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_2_CONTENT, Font = "Arial", FontSize = 14, Bold = true },
                    new DocumentContent { DocumentContentId = 23, Order = 23, Code = MockLetter.PARAGRAPH_2_TEXT_3, RenderCellCode = MockLetter.PARAGRAPH_2, Content = MockLetter.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_3_CONTENT },
                    new DocumentContent { DocumentContentId = 24, Order = 24, Code = MockLetter.PARAGRAPH_2_TEXT_4, RenderCellCode = MockLetter.PARAGRAPH_2, Content = MockLetter.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_4_CONTENT, Font = "Comic Sans MS", FontSize = 14, Colour = "255,0,0" },
                    new DocumentContent { DocumentContentId = 25, Order = 25, Code = MockLetter.PARAGRAPH_2_TEXT_5, RenderCellCode = MockLetter.PARAGRAPH_2, Content = MockLetter.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_5_CONTENT },
                    new DocumentContent { DocumentContentId = 26, Order = 26, Code = MockLetter.PARAGRAPH_2_TEXT_6, RenderCellCode = MockLetter.PARAGRAPH_2, Content = MockLetter.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_6_CONTENT, FontSize = 14, Colour = "192,0,0", Italic = true, Underscore = true },
                    new DocumentContent { DocumentContentId = 27, Order = 27, Code = MockLetter.PARAGRAPH_2_TEXT_7, RenderCellCode = MockLetter.PARAGRAPH_2, Content = MockLetter.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_7_CONTENT }
                ]
            });

            documentConfig.Paragraphs.Add(new DocumentParagraph
            {
                DocumentParagraphId = 6,
                Order = 5,
                DocumentParagraphType = DocumentParagraphType.Table,
                Name = MockLetter.LETTER_SUMMARY_TABLE,
                Rows =
                [
                    new DocumentTableRow { DocumentTableRowId = 1, Number = 1 },
                    new DocumentTableRow { DocumentTableRowId = 2, Number = 2 },
                    new DocumentTableRow { DocumentTableRowId = 3, Number = 3 },
                    new DocumentTableRow { DocumentTableRowId = 4, Number = 4 }
                ],
                Columns =
                [
                    new DocumentTableColumn { DocumentTableColumnId = 1, Number = 1, Width = 160 },
                    new DocumentTableColumn { DocumentTableColumnId = 2, Number = 2, Width = 30 }
                ],
                Cells =
                [
                    new DocumentTableCell{ DocumentTableCellId = 1, RowNumber = 1, ColumnNumber = 1, Code = MockLetter.TABLE_SUMMARY_1, CellColour = "150,200,250", BorderLeft = 1, BorderLeftColour = "150,200,250", BorderTop = 1, BorderTopColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250", BorderBottom = 1, BorderBottomColour = "150,200,250" },
                    new DocumentTableCell{ DocumentTableCellId = 2, RowNumber = 1, ColumnNumber = 2, Code = MockLetter.TABLE_SUMMARY_2, CellColour = "150,200,250", BorderLeft = 1, BorderLeftColour = "150,200,250", BorderTop = 1, BorderTopColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250", BorderBottom = 1, BorderBottomColour = "150,200,250" },
                    new DocumentTableCell{ DocumentTableCellId = 3, RowNumber = 2, ColumnNumber = 1, Code = MockLetter.TABLE_LINE_1, BorderLeft = 1, BorderLeftColour = "150,200,250", BorderTop = 1, BorderTopColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250" },
                    new DocumentTableCell{ DocumentTableCellId = 4, RowNumber = 2, ColumnNumber = 2, Code = MockLetter.TABLE_LINE_1_AMOUNT, BorderLeft = 1, BorderLeftColour = "150,200,250", BorderTop = 1, BorderTopColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250" },
                    new DocumentTableCell{ DocumentTableCellId = 5, RowNumber = 3, ColumnNumber = 1, Code = MockLetter.TABLE_LINE_2, BorderLeft = 1, BorderLeftColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250" },
                    new DocumentTableCell{ DocumentTableCellId = 6, RowNumber = 3, ColumnNumber = 2, Code = MockLetter.TABLE_LINE_2_AMOUNT, BorderLeft = 1, BorderLeftColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250" },
                    new DocumentTableCell{ DocumentTableCellId = 7, RowNumber = 4, ColumnNumber = 1, Code = MockLetter.TABLE_TOTAL, BorderLeft = 1, BorderLeftColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250", BorderBottom = 1, BorderBottomColour = "150,200,250" },
                    new DocumentTableCell{ DocumentTableCellId = 8, RowNumber = 4, ColumnNumber = 2, Code = MockLetter.TABLE_TOTAL_AMOUNT, BorderLeft = 1, BorderLeftColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250", BorderBottom = 1, BorderBottomColour = "150,200,250" }
                ],
                Contents =
                [
                    new DocumentContent { DocumentContentId = 33, Order = 33, Code = MockLetter.TABLE_SUMMARY_TEXT, RenderCellCode = MockLetter.TABLE_SUMMARY_1, Content = MockLetter.TABLE_SUMMARY_CONTENT, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 34, Order = 34, Code = MockLetter.TABLE_LINE_1_TEXT, RenderCellCode = MockLetter.TABLE_LINE_1, Content = MockLetter.TABLE_LINE_1_CONTENT, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 35, Order = 35, Code = MockLetter.TABLE_LINE_1_AMOUNT_TEXT, RenderCellCode = MockLetter.TABLE_LINE_1_AMOUNT, Content = MockLetter.TABLE_LINE_1_AMOUNT_CONTENT, AlignContent = DocumentContentAlign.End, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 36, Order = 36, Code = MockLetter.TABLE_LINE_2_TEXT, RenderCellCode = MockLetter.TABLE_LINE_2, Content = MockLetter.TABLE_LINE_2_CONTENT, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 37, Order = 37, Code = MockLetter.TABLE_LINE_2_AMOUNT_TEXT, RenderCellCode = MockLetter.TABLE_LINE_2_AMOUNT, Content = MockLetter.TABLE_LINE_2_AMOUNT_CONTENT, AlignContent = DocumentContentAlign.End, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 38, Order = 38, Code = MockLetter.TABLE_TOTAL_TEXT, RenderCellCode = MockLetter.TABLE_TOTAL, Content = MockLetter.TABLE_TOTAL_CONTENT, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 39, Order = 39, Code = MockLetter.TABLE_TOTAL_AMOUNT_TEXT, RenderCellCode = MockLetter.TABLE_TOTAL_AMOUNT, Content = MockLetter.TABLE_TOTAL_AMOUNT_CONTENT, AlignContent = DocumentContentAlign.End, IgnoreParapgraphSpacing = true }
                ]
            });

            documentConfig.Paragraphs.Add(new DocumentParagraph 
            {
                DocumentParagraphId = 7, 
                Order = 6, 
                Name = MockLetter.PARAGRAPH_3,
                Contents =
                [
                    new DocumentContent { DocumentContentId = 28, Order = 28, Code = MockLetter.PARAGRAPH_3_TEXT, RenderCellCode = MockLetter.PARAGRAPH_3, Content = MockLetter.LOREM_IPSUM_SECTION_1_10_32 }
                ]
            });

            documentConfig.Paragraphs.Add(new DocumentParagraph 
            {
                DocumentParagraphId = 8, 
                Order = 7, 
                Name = MockLetter.PARAGRAPH_4,
                Contents =
                [
                    new DocumentContent { DocumentContentId = 29, Order = 29, Code = MockLetter.PARAGRAPH_4_TEXT, RenderCellCode = MockLetter.PARAGRAPH_4, Content = MockLetter.LOREM_IPSUM_SECTION_1_10_32_HACKMAN_1914 }
                ]
            });

            documentConfig.Paragraphs.Add(new DocumentParagraph 
            {
                DocumentParagraphId = 9,
                Order = 8,
                Name = MockLetter.PARAGRAPH_5,
                Contents =
                [
                    new DocumentContent { DocumentContentId = 30, Order = 30, Code = MockLetter.PARAGRAPH_5_TEXT, RenderCellCode = MockLetter.PARAGRAPH_5, Content = MockLetter.LOREM_IPSUM_SECTION_1_10_33 }
                ]
            });

            documentConfig.Paragraphs.Add(new DocumentParagraph 
            {
                DocumentParagraphId = 10, 
                Order = 9, 
                Name = MockLetter.PARAGRAPH_6,
                Contents =
                [
                    new DocumentContent { DocumentContentId = 31, Order = 31, Code = MockLetter.PARAGRAPH_6_TEXT, RenderCellCode = MockLetter.PARAGRAPH_6, Content = MockLetter.LOREM_IPSUM_SECTION_1_10_33_HACKMAN_1914 }
                ]
            });

            documentConfig.Paragraphs.Add(new DocumentParagraph
            {
                DocumentParagraphId = 11,
                Order = 10,
                DocumentParagraphType = DocumentParagraphType.Table,
                Name = ContentCodes.SIGNATURE_TABLE,
                Rows =
                [
                    new DocumentTableRow { DocumentTableRowId = 1, Number = 1 }
                ],
                Columns =
                [
                    new DocumentTableColumn { DocumentTableColumnId = 1, Number = 1, Width = 35 }
                ],
                Cells =
                [
                    new DocumentTableCell { DocumentTableCellId = 1, RowNumber = 1, ColumnNumber = 1, Code = ContentCodes.SIGNATURE_DETAILS }
                ],
                Contents =
                [
                    new DocumentContent { DocumentContentId = 17, Order = 17, Code = ContentCodes.SIGNATURE, RenderCellCode = ContentCodes.SIGNATURE_DETAILS, ContentType = DocumentContentType.Image, Name = "Signature", Source = Path.Combine(outputLocation, "signature.png"), ImageHeight = 15, ImageWidth = 35 },
                    new DocumentContent { DocumentContentId = 18, Order = 18, Code = ContentCodes.SIGNEE, RenderCellCode = ContentCodes.SIGNATURE_DETAILS, Content = MockLetter.SIGNEE_CONTENT, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 19, Order = 19, Code = ContentCodes.SIGNEE_TITLE, RenderCellCode = ContentCodes.SIGNATURE_DETAILS, Content = MockLetter.SIGNEE_TITLE_CONTENT, IgnoreParapgraphSpacing = true }
                ]
            });

            return documentConfig;
        }

        private static List<DocumentSubstitute> GetDocumentSubstitutes()
        {
            List<DocumentSubstitute> documentSubstitutes = [];

            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_ID, Value = "123" });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.COMPANY_NAME, Value = "Global Banking Corp." });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.COMPANY_ADDRESS_1, Value = "9 Cherry Tree Lane" });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.COMPANY_ADDRESS_2, Value = "Canary Wharf" });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.COMPANY_ADDRESS_3, Value = "E14 5HQ" });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.COMPANY_PHONE_NUMBER, Value = "+44 071 946-0241" });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.COMPANY_EMAIL, Value = "gbc@email.com" });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_NAME, Value = "Mrs Jane Masters" });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_ADDRESS_1, Value = "142 Middle Street", Group = Substitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_ADDRESS_2, Value = "", Group = Substitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_ADDRESS_3, Value = "", Group = Substitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_ADDRESS_4, Value = "Brockham", Group = Substitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_ADDRESS_5, Value = "", Group = Substitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_ADDRESS_6, Value = "Surrey", Group = Substitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_ADDRESS_7, Value = "KT20 3AD", Group = Substitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_REFERENCE, Value = "MASTERS/ABC-123" });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.SIGNEE, Value = "Mrs Peggy Olson" });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.SIGNEE_TITLE, Value = "Managing Director" });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_2, Value = "\"Lorem ipsum dolor sit amet consectetuer\"" });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_4, Value = "Actually, it is nonsense" });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_6, Value = "The phrase has been used for several centuries by typographers" });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.TABLE_SUMMARY, Value = "Present Test Data in Table" });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.TABLE_LINE_1, Value = "Line item no. 1" });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.TABLE_LINE_1_AMOUNT, Value = "£70" });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.TABLE_LINE_2, Value = "Line item no. 2" });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.TABLE_LINE_2_AMOUNT, Value = "£30" });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.TABLE_TOTAL, Value = "Total" });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.TABLE_TOTAL_AMOUNT, Value = "£100" });

            return documentSubstitutes;
        }
    }
}
