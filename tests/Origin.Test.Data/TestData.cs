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
                Code = ContentCodes.FOOTER_PARAGRAPH,
                FontSize = 10,
                IgnoreParapgraphSpacing = true,
                Contents =
                [
                    new DocumentContent { DocumentContentId = 32, Order = 32, Code = ContentCodes.FOOTER_TEXT, RenderElementCode = ContentCodes.FOOTER_PARAGRAPH, Content = MockLetter.FOOTER_CONTENT }
                ]
            });

            documentConfig.Paragraphs.Add(new DocumentParagraph
            {
                DocumentParagraphId = 2,
                Order = 1,
                DocumentParagraphType = DocumentParagraphType.Table,
                Code = ContentCodes.LETTER_HEAD_TABLE,
                Rows =
                [
                    new DocumentTableRow { DocumentTableRowId = 1, Position = 1, TableCode =  ContentCodes.LETTER_HEAD_TABLE  },
                    new DocumentTableRow { DocumentTableRowId = 2, Position = 2, TableCode =  ContentCodes.LETTER_HEAD_TABLE  },
                    new DocumentTableRow { DocumentTableRowId = 3, Position = 3, TableCode =  ContentCodes.LETTER_HEAD_TABLE  }
                ],
                Columns =
                [
                    new DocumentTableColumn { DocumentTableColumnId = 1, Position = 1, Width = 62, TableCode = ContentCodes.LETTER_HEAD_TABLE  },
                    new DocumentTableColumn { DocumentTableColumnId = 2, Position = 2, Width = 63, TableCode = ContentCodes.LETTER_HEAD_TABLE  },
                    new DocumentTableColumn { DocumentTableColumnId = 3, Position = 3, Width = 55, TableCode = ContentCodes.LETTER_HEAD_TABLE  }
                ],
                Cells =
                [
                    new DocumentTableCell{ DocumentTableCellId = 1, Row = 1, Column = 1, Code = ContentCodes.COMPANY_LOGO, RenderElementCode = ContentCodes.LETTER_HEAD_TABLE },
                    new DocumentTableCell{ DocumentTableCellId = 2, Row = 2, Column = 3, Code = ContentCodes.COMPANY_DETAILS, RenderElementCode = ContentCodes.LETTER_HEAD_TABLE },
                    new DocumentTableCell{ DocumentTableCellId = 3, Row = 3, Column = 1, Code = ContentCodes.CUSTOMER_DETAILS, RenderElementCode = ContentCodes.LETTER_HEAD_TABLE }
                ],
                Contents = 
                [
                    new DocumentContent { DocumentContentId = 1, Order = 1, Code = ContentCodes.LOGO, RenderElementCode = ContentCodes.COMPANY_LOGO, ContentType = DocumentContentType.Image, Name = "Logo", Source = Path.Combine(outputLocation, "logo.png"), ImageHeight = 15, ImageWidth = 35 },
                    new DocumentContent { DocumentContentId = 2, Order = 2, Code = ContentCodes.COMPANY_NAME, RenderElementCode = ContentCodes.COMPANY_DETAILS, Content = MockLetter.COMPANY_NAME_CONTENT, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 3, Order = 3, Code = ContentCodes.COMPANY_ADDRESS_1, RenderElementCode = ContentCodes.COMPANY_DETAILS, Content = MockLetter.COMPANY_ADDRESS_1_CONTENT, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 4, Order = 4, Code = ContentCodes.COMPANY_ADDRESS_2, RenderElementCode = ContentCodes.COMPANY_DETAILS, Content = MockLetter.COMPANY_ADDRESS_2_CONTENT, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 5, Order = 5, Code = ContentCodes.COMPANY_ADDRESS_3, RenderElementCode = ContentCodes.COMPANY_DETAILS, Content = MockLetter.COMPANY_ADDRESS_3_CONTENT, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 6, Order = 6, Code = ContentCodes.COMPANY_PHONE_NUMBER, RenderElementCode = ContentCodes.COMPANY_DETAILS, Content = MockLetter.COMPANY_PHONE_NUMBER_CONTENT, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 7, Order = 7, Code = ContentCodes.COMPANY_EMAIL, RenderElementCode = ContentCodes.COMPANY_DETAILS, Content = MockLetter.COMPANY_EMAIL_CONTENT, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 8, Order = 8, Code = ContentCodes.CUSTOMER_NAME, RenderElementCode = ContentCodes.CUSTOMER_DETAILS, Content = MockLetter.CUSTOMER_NAME_CONTENT, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 9, Order = 9, Code = ContentCodes.CUSTOMER_ADDRESS_1, RenderElementCode = ContentCodes.CUSTOMER_DETAILS, Content = MockLetter.CUSTOMER_ADDRESS_1_CONTENT, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 10, Order = 10, Code = ContentCodes.CUSTOMER_ADDRESS_2, RenderElementCode = ContentCodes.CUSTOMER_DETAILS, Content = MockLetter.CUSTOMER_ADDRESS_2_CONTENT, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 11, Order = 11, Code = ContentCodes.CUSTOMER_ADDRESS_3, RenderElementCode = ContentCodes.CUSTOMER_DETAILS, Content = MockLetter.CUSTOMER_ADDRESS_3_CONTENT, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 12, Order = 12, Code = ContentCodes.CUSTOMER_ADDRESS_4, RenderElementCode = ContentCodes.CUSTOMER_DETAILS, Content = MockLetter.CUSTOMER_ADDRESS_4_CONTENT, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 13, Order = 13, Code = ContentCodes.CUSTOMER_ADDRESS_5, RenderElementCode = ContentCodes.CUSTOMER_DETAILS, Content = MockLetter.CUSTOMER_ADDRESS_5_CONTENT, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 14, Order = 14, Code = ContentCodes.CUSTOMER_ADDRESS_6, RenderElementCode = ContentCodes.CUSTOMER_DETAILS, Content = MockLetter.CUSTOMER_ADDRESS_6_CONTENT, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 15, Order = 15, Code = ContentCodes.CUSTOMER_ADDRESS_7, RenderElementCode = ContentCodes.CUSTOMER_DETAILS, Content = MockLetter.CUSTOMER_ADDRESS_7_CONTENT, IgnoreParapgraphSpacing = true }
                ]
            });

            documentConfig.Paragraphs.Add(new DocumentParagraph
            {
                DocumentParagraphId = 3,
                Order = 2,
                Code = ContentCodes.LETTER_TITLE_PARAGRAPH,
                Font = "Courier",
                FontSize = 16,
                Colour = "0,176,240",
                Contents = 
                [
                    new DocumentContent { DocumentContentId = 16, Order = 16, Code = ContentCodes.LETTER_TITLE, RenderElementCode = ContentCodes.LETTER_TITLE_PARAGRAPH, Content = MockLetter.LETTER_TITLE_CONTENT }
                ]
            });

            documentConfig.Paragraphs.Add(new DocumentParagraph
            {
                DocumentParagraphId = 4,
                Order = 3,
                Code = MockLetter.PARAGRAPH_1,
                Font = "Courier",
                FontSize = 11,
                Contents = 
                [
                    new DocumentContent { DocumentContentId = 20, Order = 20, Code = MockLetter.PARAGRAPH_1_TEXT, RenderElementCode = MockLetter.PARAGRAPH_1, Content = MockLetter.LOREM_IPSUM_STANDARD_1500 }
                ]
            });

            documentConfig.Paragraphs.Add(new DocumentParagraph
            {
                DocumentParagraphId = 5,
                Order = 4,
                Code = MockLetter.PARAGRAPH_2,
                Font = "Times New Roman",
                FontSize = 12,
                Colour = "68,114,196",
                Contents =
                [
                    new DocumentContent { DocumentContentId = 21, Order = 21, Code = MockLetter.PARAGRAPH_2_TEXT_1, RenderElementCode = MockLetter.PARAGRAPH_2, Content = MockLetter.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_1_CONTENT },
                    new DocumentContent { DocumentContentId = 22, Order = 22, Code = MockLetter.PARAGRAPH_2_TEXT_2, RenderElementCode = MockLetter.PARAGRAPH_2, Content = MockLetter.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_2_CONTENT, Font = "Arial", FontSize = 14, Bold = true },
                    new DocumentContent { DocumentContentId = 23, Order = 23, Code = MockLetter.PARAGRAPH_2_TEXT_3, RenderElementCode = MockLetter.PARAGRAPH_2, Content = MockLetter.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_3_CONTENT },
                    new DocumentContent { DocumentContentId = 24, Order = 24, Code = MockLetter.PARAGRAPH_2_TEXT_4, RenderElementCode = MockLetter.PARAGRAPH_2, Content = MockLetter.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_4_CONTENT, Font = "Comic Sans MS", FontSize = 14, Colour = "255,0,0" },
                    new DocumentContent { DocumentContentId = 25, Order = 25, Code = MockLetter.PARAGRAPH_2_TEXT_5, RenderElementCode = MockLetter.PARAGRAPH_2, Content = MockLetter.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_5_CONTENT },
                    new DocumentContent { DocumentContentId = 26, Order = 26, Code = MockLetter.PARAGRAPH_2_TEXT_6, RenderElementCode = MockLetter.PARAGRAPH_2, Content = MockLetter.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_6_CONTENT, FontSize = 14, Colour = "192,0,0", Italic = true, Underscore = true },
                    new DocumentContent { DocumentContentId = 27, Order = 27, Code = MockLetter.PARAGRAPH_2_TEXT_7, RenderElementCode = MockLetter.PARAGRAPH_2, Content = MockLetter.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_7_CONTENT }
                ]
            });

            documentConfig.Paragraphs.Add(new DocumentParagraph
            {
                DocumentParagraphId = 6,
                Order = 5,
                DocumentParagraphType = DocumentParagraphType.Table,
                Code = MockLetter.LETTER_SUMMARY_TABLE,
                Rows =
                [
                    new DocumentTableRow { DocumentTableRowId = 1, Position = 1, TableCode =  MockLetter.LETTER_SUMMARY_TABLE  },
                    new DocumentTableRow { DocumentTableRowId = 2, Position = 2, TableCode =  MockLetter.LETTER_SUMMARY_TABLE  },
                    new DocumentTableRow { DocumentTableRowId = 3, Position = 3, TableCode =  MockLetter.LETTER_SUMMARY_TABLE  },
                    new DocumentTableRow { DocumentTableRowId = 4, Position = 4, TableCode =  MockLetter.LETTER_SUMMARY_TABLE  }
                ],
                Columns =
                [
                    new DocumentTableColumn { DocumentTableColumnId = 1, Position = 1, Width = 160, TableCode = MockLetter.LETTER_SUMMARY_TABLE  },
                    new DocumentTableColumn { DocumentTableColumnId = 2, Position = 2, Width = 30, TableCode = MockLetter.LETTER_SUMMARY_TABLE  }
                ],
                Cells =
                [
                    new DocumentTableCell{ DocumentTableCellId = 1, Row = 1, Column = 1, Code = MockLetter.TABLE_SUMMARY_1, RenderElementCode = MockLetter.LETTER_SUMMARY_TABLE, CellColour = "150,200,250", BorderLeft = 1, BorderLeftColour = "150,200,250", BorderTop = 1, BorderTopColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250", BorderBottom = 1, BorderBottomColour = "150,200,250" },
                    new DocumentTableCell{ DocumentTableCellId = 2, Row = 1, Column = 2, Code = MockLetter.TABLE_SUMMARY_2, RenderElementCode = MockLetter.LETTER_SUMMARY_TABLE, CellColour = "150,200,250", BorderLeft = 1, BorderLeftColour = "150,200,250", BorderTop = 1, BorderTopColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250", BorderBottom = 1, BorderBottomColour = "150,200,250" },
                    new DocumentTableCell{ DocumentTableCellId = 3, Row = 2, Column = 1, Code = MockLetter.TABLE_LINE_1, RenderElementCode = MockLetter.LETTER_SUMMARY_TABLE, BorderLeft = 1, BorderLeftColour = "150,200,250", BorderTop = 1, BorderTopColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250" },
                    new DocumentTableCell{ DocumentTableCellId = 4, Row = 2, Column = 2, Code = MockLetter.TABLE_LINE_1_AMOUNT, RenderElementCode = MockLetter.LETTER_SUMMARY_TABLE, BorderLeft = 1, BorderLeftColour = "150,200,250", BorderTop = 1, BorderTopColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250" },
                    new DocumentTableCell{ DocumentTableCellId = 5, Row = 3, Column = 1, Code = MockLetter.TABLE_LINE_2, RenderElementCode = MockLetter.LETTER_SUMMARY_TABLE, BorderLeft = 1, BorderLeftColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250" },
                    new DocumentTableCell{ DocumentTableCellId = 6, Row = 3, Column = 2, Code = MockLetter.TABLE_LINE_2_AMOUNT, RenderElementCode = MockLetter.LETTER_SUMMARY_TABLE, BorderLeft = 1, BorderLeftColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250" },
                    new DocumentTableCell{ DocumentTableCellId = 7, Row = 4, Column = 1, Code = MockLetter.TABLE_TOTAL, RenderElementCode = MockLetter.LETTER_SUMMARY_TABLE, BorderLeft = 1, BorderLeftColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250", BorderBottom = 1, BorderBottomColour = "150,200,250" },
                    new DocumentTableCell{ DocumentTableCellId = 8, Row = 4, Column = 2, Code = MockLetter.TABLE_TOTAL_AMOUNT, RenderElementCode = MockLetter.LETTER_SUMMARY_TABLE, BorderLeft = 1, BorderLeftColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250", BorderBottom = 1, BorderBottomColour = "150,200,250" }
                ],
                Contents =
                [
                    new DocumentContent { DocumentContentId = 33, Order = 33, Code = MockLetter.TABLE_SUMMARY_TEXT, RenderElementCode = MockLetter.TABLE_SUMMARY_1, Content = MockLetter.TABLE_SUMMARY_CONTENT, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 34, Order = 34, Code = MockLetter.TABLE_LINE_1_TEXT, RenderElementCode = MockLetter.TABLE_LINE_1, Content = MockLetter.TABLE_LINE_1_CONTENT, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 35, Order = 35, Code = MockLetter.TABLE_LINE_1_AMOUNT_TEXT, RenderElementCode = MockLetter.TABLE_LINE_1_AMOUNT, Content = MockLetter.TABLE_LINE_1_AMOUNT_CONTENT, AlignContent = DocumentContentAlign.End, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 36, Order = 36, Code = MockLetter.TABLE_LINE_2_TEXT, RenderElementCode = MockLetter.TABLE_LINE_2, Content = MockLetter.TABLE_LINE_2_CONTENT, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 37, Order = 37, Code = MockLetter.TABLE_LINE_2_AMOUNT_TEXT, RenderElementCode = MockLetter.TABLE_LINE_2_AMOUNT, Content = MockLetter.TABLE_LINE_2_AMOUNT_CONTENT, AlignContent = DocumentContentAlign.End, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 38, Order = 38, Code = MockLetter.TABLE_TOTAL_TEXT, RenderElementCode = MockLetter.TABLE_TOTAL, Content = MockLetter.TABLE_TOTAL_CONTENT, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 39, Order = 39, Code = MockLetter.TABLE_TOTAL_AMOUNT_TEXT, RenderElementCode = MockLetter.TABLE_TOTAL_AMOUNT, Content = MockLetter.TABLE_TOTAL_AMOUNT_CONTENT, AlignContent = DocumentContentAlign.End, IgnoreParapgraphSpacing = true }
                ]
            });

            documentConfig.Paragraphs.Add(new DocumentParagraph 
            {
                DocumentParagraphId = 7, 
                Order = 6, 
                Code = MockLetter.PARAGRAPH_3,
                Contents =
                [
                    new DocumentContent { DocumentContentId = 28, Order = 28, Code = MockLetter.PARAGRAPH_3_TEXT, RenderElementCode = MockLetter.PARAGRAPH_3, Content = MockLetter.LOREM_IPSUM_SECTION_1_10_32 }
                ]
            });

            documentConfig.Paragraphs.Add(new DocumentParagraph 
            {
                DocumentParagraphId = 8, 
                Order = 7, 
                Code = MockLetter.PARAGRAPH_4,
                Contents =
                [
                    new DocumentContent { DocumentContentId = 29, Order = 29, Code = MockLetter.PARAGRAPH_4_TEXT, RenderElementCode = MockLetter.PARAGRAPH_4, Content = MockLetter.LOREM_IPSUM_SECTION_1_10_32_HACKMAN_1914 }
                ]
            });

            documentConfig.Paragraphs.Add(new DocumentParagraph 
            {
                DocumentParagraphId = 9,
                Order = 8,
                Code = MockLetter.PARAGRAPH_5,
                Contents =
                [
                    new DocumentContent { DocumentContentId = 30, Order = 30, Code = MockLetter.PARAGRAPH_5_TEXT, RenderElementCode = MockLetter.PARAGRAPH_5, Content = MockLetter.LOREM_IPSUM_SECTION_1_10_33 }
                ]
            });

            documentConfig.Paragraphs.Add(new DocumentParagraph 
            {
                DocumentParagraphId = 10, 
                Order = 9, 
                Code = MockLetter.PARAGRAPH_6,
                Contents =
                [
                    new DocumentContent { DocumentContentId = 31, Order = 31, Code = MockLetter.PARAGRAPH_6_TEXT, RenderElementCode = MockLetter.PARAGRAPH_6, Content = MockLetter.LOREM_IPSUM_SECTION_1_10_33_HACKMAN_1914 }
                ]
            });

            documentConfig.Paragraphs.Add(new DocumentParagraph
            {
                DocumentParagraphId = 11,
                Order = 10,
                DocumentParagraphType = DocumentParagraphType.Table,
                Code = ContentCodes.SIGNATURE_TABLE,
                Rows =
                [
                    new DocumentTableRow { DocumentTableRowId = 1, Position = 1, TableCode =  ContentCodes.SIGNATURE_TABLE  }
                ],
                Columns =
                [
                    new DocumentTableColumn { DocumentTableColumnId = 1, Position = 1, Width = 35, TableCode = ContentCodes.SIGNATURE_TABLE  }
                ],
                Cells =
                [
                    new DocumentTableCell { DocumentTableCellId = 1, Row = 1, Column = 1, Code = ContentCodes.SIGNATURE_DETAILS, RenderElementCode = ContentCodes.SIGNATURE_TABLE }
                ],
                Contents =
                [
                    new DocumentContent { DocumentContentId = 17, Order = 17, Code = ContentCodes.SIGNATURE, RenderElementCode = ContentCodes.SIGNATURE_DETAILS, ContentType = DocumentContentType.Image, Name = "Signature", Source = Path.Combine(outputLocation, "signature.png"), ImageHeight = 15, ImageWidth = 35 },
                    new DocumentContent { DocumentContentId = 18, Order = 18, Code = ContentCodes.SIGNEE, RenderElementCode = ContentCodes.SIGNATURE_DETAILS, Content = MockLetter.SIGNEE_CONTENT, IgnoreParapgraphSpacing = true },
                    new DocumentContent { DocumentContentId = 19, Order = 19, Code = ContentCodes.SIGNEE_TITLE, RenderElementCode = ContentCodes.SIGNATURE_DETAILS, Content = MockLetter.SIGNEE_TITLE_CONTENT, IgnoreParapgraphSpacing = true }
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
