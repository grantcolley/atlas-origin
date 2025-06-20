﻿using Origin.Core.Models;

namespace Origin.Tests.Data
{
    public static class TestData
    {
        public static Document GetDocumentOpenXml(string target, bool skipSubValues = false)
        {
            return new Document
            {
                DocumentGeneratorType = DocumentGeneratorType.OpenXmlDocument,
                Target = target,
                FilenameTemplate = $"TestDocx_[{Substitutes.CUSTOMER_ID}]",
                Config = GetDocumentArgs(skipSubValues)
            };
        }

        public static Document GetDocumentPdfSharp(string target, bool skipSubValues = false)
        {
            return new Document
            {
                DocumentGeneratorType = DocumentGeneratorType.PdfSharp,
                Target = target,
                FilenameTemplate = $"TestPdf_[{Substitutes.CUSTOMER_ID}]",
                Config = GetDocumentArgs(skipSubValues)
            };
        }

        public static Document GetDocumentHtml(string target, bool skipSubValues = false)
        {
            return new Document
            {
                DocumentGeneratorType = DocumentGeneratorType.Html,
                Target = target,
                FilenameTemplate = $"TestHtml_[{Substitutes.CUSTOMER_ID}]",
                Config = GetDocumentArgs(skipSubValues)
            };
        }

        public static DocumentConfig GetDocumentArgs(bool skipSubValues = false)
        {
            DocumentConfig documentConfig = new()
            {
                SubstituteStart = "[",
                SubstituteEnd = "]",
                Substitutes = GetDocumentSubstitutes(skipSubValues),
                ConfigParagraphs =
            [
                new DocumentConfigParagraph()
                {
                    Order = 0,
                    DocumentParagraph = new DocumentParagraph
                    {
                        DocumentParagraphId = 1,
                        DocumentParagraphType = DocumentParagraphType.Footer,
                        Name = ParagraphNames.FOOTER_PARAGRAPH,
                        FontSize = 8,
                        IgnoreParapgraphSpacing = true,
                        Contents =
                        [
                            new DocumentContent { DocumentContentId = 32, Order = 32, Name = MockLetter.FOOTER_TEXT, Content = MockLetter.FOOTER_CONTENT }
                        ]
                    }
                },
                new DocumentConfigParagraph()
                {
                    Order = 1,
                    DocumentParagraph = new DocumentParagraph
                    {
                        DocumentParagraphId = 2,
                        DocumentParagraphType = DocumentParagraphType.Table,
                        Name = ParagraphNames.LETTER_HEAD_TABLE,
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
                            new DocumentTableCell{ DocumentTableCellId = 1, RowNumber = 1, ColumnNumber = 1, CellCode = MockLetter.COMPANY_LOGO },
                            new DocumentTableCell{ DocumentTableCellId = 2, RowNumber = 2, ColumnNumber = 3, CellCode = MockLetter.COMPANY_DETAILS },
                            new DocumentTableCell{ DocumentTableCellId = 3, RowNumber = 3, ColumnNumber = 1, CellCode = MockLetter.CUSTOMER_DETAILS }
                        ],
                        Contents =
                        [
                            new DocumentContent { DocumentContentId = 1, Order = 1, Name = MockLetter.LOGO, RenderCellCode = MockLetter.COMPANY_LOGO, ContentType = DocumentContentType.Image, Image = "logo.png", ImageHeight = 15, ImageWidth = 35 },
                            new DocumentContent { DocumentContentId = 2, Order = 2, Name = MockLetter.COMPANY_NAME, RenderCellCode = MockLetter.COMPANY_DETAILS, Content = MockLetter.COMPANY_NAME_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { DocumentContentId = 3, Order = 3, Name = MockLetter.COMPANY_ADDRESS_1, RenderCellCode = MockLetter.COMPANY_DETAILS, Content = MockLetter.COMPANY_ADDRESS_1_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { DocumentContentId = 4, Order = 4, Name = MockLetter.COMPANY_ADDRESS_2, RenderCellCode = MockLetter.COMPANY_DETAILS, Content = MockLetter.COMPANY_ADDRESS_2_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { DocumentContentId = 5, Order = 5, Name = MockLetter.COMPANY_ADDRESS_3, RenderCellCode = MockLetter.COMPANY_DETAILS, Content = MockLetter.COMPANY_ADDRESS_3_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { DocumentContentId = 6, Order = 6, Name = MockLetter.COMPANY_PHONE_NUMBER, RenderCellCode = MockLetter.COMPANY_DETAILS, Content = MockLetter.COMPANY_PHONE_NUMBER_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { DocumentContentId = 7, Order = 7, Name = MockLetter.COMPANY_EMAIL, RenderCellCode = MockLetter.COMPANY_DETAILS, Content = MockLetter.COMPANY_EMAIL_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { DocumentContentId = 8, Order = 8, Name = MockLetter.CUSTOMER_NAME, RenderCellCode = MockLetter.CUSTOMER_DETAILS, Content = MockLetter.CUSTOMER_NAME_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { DocumentContentId = 9, Order = 9, Name = MockLetter.CUSTOMER_ADDRESS_1, RenderCellCode = MockLetter.CUSTOMER_DETAILS, Content = MockLetter.CUSTOMER_ADDRESS_1_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { DocumentContentId = 10, Order = 10, Name = MockLetter.CUSTOMER_ADDRESS_2, RenderCellCode = MockLetter.CUSTOMER_DETAILS, Content = MockLetter.CUSTOMER_ADDRESS_2_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { DocumentContentId = 11, Order = 11, Name = MockLetter.CUSTOMER_ADDRESS_3, RenderCellCode = MockLetter.CUSTOMER_DETAILS, Content = MockLetter.CUSTOMER_ADDRESS_3_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { DocumentContentId = 12, Order = 12, Name = MockLetter.CUSTOMER_ADDRESS_4, RenderCellCode = MockLetter.CUSTOMER_DETAILS, Content = MockLetter.CUSTOMER_ADDRESS_4_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { DocumentContentId = 13, Order = 13, Name = MockLetter.CUSTOMER_ADDRESS_5, RenderCellCode = MockLetter.CUSTOMER_DETAILS, Content = MockLetter.CUSTOMER_ADDRESS_5_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { DocumentContentId = 14, Order = 14, Name = MockLetter.CUSTOMER_ADDRESS_6, RenderCellCode = MockLetter.CUSTOMER_DETAILS, Content = MockLetter.CUSTOMER_ADDRESS_6_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { DocumentContentId = 15, Order = 15, Name = MockLetter.CUSTOMER_ADDRESS_7, RenderCellCode = MockLetter.CUSTOMER_DETAILS, Content = MockLetter.CUSTOMER_ADDRESS_7_CONTENT, IgnoreParapgraphSpacing = true }
                        ]
                    }
                },
                new DocumentConfigParagraph()
                {
                    Order = 2,
                    DocumentParagraph = new DocumentParagraph
                    {
                        DocumentParagraphId = 3,
                        Name = ParagraphNames.LETTER_TITLE_PARAGRAPH,
                        Font = "Courier",
                        FontSize = 16,
                        Colour = "0,176,240",
                        Contents =
                        [
                            new DocumentContent { DocumentContentId = 16, Order = 16, Name = MockLetter.LETTER_TITLE, Content = MockLetter.LETTER_TITLE_CONTENT }
                        ]
                    }
                },
                new DocumentConfigParagraph()
                {
                    Order = 3,
                    DocumentParagraph = new DocumentParagraph
                    {
                        DocumentParagraphId = 4,
                        Name = MockLetter.PARAGRAPH_1,
                        Font = "Courier",
                        FontSize = 11,
                        Contents =
                        [
                            new DocumentContent { DocumentContentId = 20, Order = 20, Name = MockLetter.PARAGRAPH_1_TEXT, RenderCellCode = MockLetter.PARAGRAPH_1, Content = MockLetter.LOREM_IPSUM_STANDARD_1500 }
                        ]
                    }
                },
                new DocumentConfigParagraph()
                {
                    Order = 4,
                    DocumentParagraph = new DocumentParagraph
                    {
                        DocumentParagraphId = 5,
                        Name = MockLetter.PARAGRAPH_2,
                        Font = "Times New Roman",
                        FontSize = 12,
                        Colour = "65,105,225",
                        Contents =
                        [
                            new DocumentContent { DocumentContentId = 21, Order = 21, Name = MockLetter.PARAGRAPH_2_TEXT_1, RenderCellCode = MockLetter.PARAGRAPH_2, Content = MockLetter.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_1_CONTENT },
                            new DocumentContent { DocumentContentId = 22, Order = 22, Name = MockLetter.PARAGRAPH_2_TEXT_2, RenderCellCode = MockLetter.PARAGRAPH_2, Content = MockLetter.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_2_CONTENT, Font = "Arial", FontSize = 14, Bold = true },
                            new DocumentContent { DocumentContentId = 23, Order = 23, Name = MockLetter.PARAGRAPH_2_TEXT_3, RenderCellCode = MockLetter.PARAGRAPH_2, Content = MockLetter.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_3_CONTENT },
                            new DocumentContent { DocumentContentId = 24, Order = 24, Name = MockLetter.PARAGRAPH_2_TEXT_4, RenderCellCode = MockLetter.PARAGRAPH_2, Content = MockLetter.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_4_CONTENT, Font = "Comic Sans MS", FontSize = 14, Colour = "255,0,0" },
                            new DocumentContent { DocumentContentId = 25, Order = 25, Name = MockLetter.PARAGRAPH_2_TEXT_5, RenderCellCode = MockLetter.PARAGRAPH_2, Content = MockLetter.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_5_CONTENT },
                            new DocumentContent { DocumentContentId = 26, Order = 26, Name = MockLetter.PARAGRAPH_2_TEXT_6, RenderCellCode = MockLetter.PARAGRAPH_2, Content = MockLetter.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_6_CONTENT, FontSize = 14, Colour = "192,0,0", Italic = true, Underscore = true },
                            new DocumentContent { DocumentContentId = 27, Order = 27, Name = MockLetter.PARAGRAPH_2_TEXT_7, RenderCellCode = MockLetter.PARAGRAPH_2, Content = MockLetter.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_7_CONTENT }
                        ]
                    }
                },
                new DocumentConfigParagraph()
                {
                    Order = 5,
                    DocumentParagraph = new DocumentParagraph
                    {
                        DocumentParagraphId = 6,
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
                            new DocumentTableCell{ DocumentTableCellId = 1, RowNumber = 1, ColumnNumber = 1, CellCode = MockLetter.TABLE_SUMMARY_1, CellColour = "150,200,250", BorderLeft = 1, BorderLeftColour = "150,200,250", BorderTop = 1, BorderTopColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250", BorderBottom = 1, BorderBottomColour = "150,200,250" },
                            new DocumentTableCell{ DocumentTableCellId = 2, RowNumber = 1, ColumnNumber = 2, CellCode = MockLetter.TABLE_SUMMARY_2, CellColour = "150,200,250", BorderLeft = 1, BorderLeftColour = "150,200,250", BorderTop = 1, BorderTopColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250", BorderBottom = 1, BorderBottomColour = "150,200,250" },
                            new DocumentTableCell{ DocumentTableCellId = 3, RowNumber = 2, ColumnNumber = 1, CellCode = MockLetter.TABLE_LINE_1, BorderLeft = 1, BorderLeftColour = "150,200,250", BorderTop = 1, BorderTopColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250" },
                            new DocumentTableCell{ DocumentTableCellId = 4, RowNumber = 2, ColumnNumber = 2, CellCode = MockLetter.TABLE_LINE_1_AMOUNT, BorderLeft = 1, BorderLeftColour = "150,200,250", BorderTop = 1, BorderTopColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250" },
                            new DocumentTableCell{ DocumentTableCellId = 5, RowNumber = 3, ColumnNumber = 1, CellCode = MockLetter.TABLE_LINE_2, BorderLeft = 1, BorderLeftColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250" },
                            new DocumentTableCell{ DocumentTableCellId = 6, RowNumber = 3, ColumnNumber = 2, CellCode = MockLetter.TABLE_LINE_2_AMOUNT, BorderLeft = 1, BorderLeftColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250" },
                            new DocumentTableCell{ DocumentTableCellId = 7, RowNumber = 4, ColumnNumber = 1, CellCode = MockLetter.TABLE_TOTAL, BorderLeft = 1, BorderLeftColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250", BorderBottom = 1, BorderBottomColour = "150,200,250" },
                            new DocumentTableCell{ DocumentTableCellId = 8, RowNumber = 4, ColumnNumber = 2, CellCode = MockLetter.TABLE_TOTAL_AMOUNT, BorderLeft = 1, BorderLeftColour = "150,200,250", BorderRight = 1, BorderRightColour = "150,200,250", BorderBottom = 1, BorderBottomColour = "150,200,250" }
                        ],
                        Contents =
                        [
                            new DocumentContent { DocumentContentId = 33, Order = 33, Name = MockLetter.TABLE_SUMMARY_TEXT, RenderCellCode = MockLetter.TABLE_SUMMARY_1, Content = MockLetter.TABLE_SUMMARY_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { DocumentContentId = 34, Order = 34, Name = MockLetter.TABLE_LINE_1_TEXT, RenderCellCode = MockLetter.TABLE_LINE_1, Content = MockLetter.TABLE_LINE_1_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { DocumentContentId = 35, Order = 35, Name = MockLetter.TABLE_LINE_1_AMOUNT_TEXT, RenderCellCode = MockLetter.TABLE_LINE_1_AMOUNT, Content = MockLetter.TABLE_LINE_1_AMOUNT_CONTENT, AlignContent = DocumentContentAlign.End, IgnoreParapgraphSpacing = true },
                            new DocumentContent { DocumentContentId = 36, Order = 36, Name = MockLetter.TABLE_LINE_2_TEXT, RenderCellCode = MockLetter.TABLE_LINE_2, Content = MockLetter.TABLE_LINE_2_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { DocumentContentId = 37, Order = 37, Name = MockLetter.TABLE_LINE_2_AMOUNT_TEXT, RenderCellCode = MockLetter.TABLE_LINE_2_AMOUNT, Content = MockLetter.TABLE_LINE_2_AMOUNT_CONTENT, AlignContent = DocumentContentAlign.End, IgnoreParapgraphSpacing = true },
                            new DocumentContent { DocumentContentId = 38, Order = 38, Name = MockLetter.TABLE_TOTAL_TEXT, RenderCellCode = MockLetter.TABLE_TOTAL, Content = MockLetter.TABLE_TOTAL_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { DocumentContentId = 39, Order = 39, Name = MockLetter.TABLE_TOTAL_AMOUNT_TEXT, RenderCellCode = MockLetter.TABLE_TOTAL_AMOUNT, Content = MockLetter.TABLE_TOTAL_AMOUNT_CONTENT, AlignContent = DocumentContentAlign.End, IgnoreParapgraphSpacing = true }
                        ]
                    }
                },
                new DocumentConfigParagraph()
                {
                    Order = 6,
                    DocumentParagraph = new DocumentParagraph
                    {
                        DocumentParagraphId = 7,
                        Name = MockLetter.PARAGRAPH_3,
                        Contents =
                        [
                            new DocumentContent { DocumentContentId = 28, Order = 28, Name = MockLetter.PARAGRAPH_3_TEXT, RenderCellCode = MockLetter.PARAGRAPH_3, Content = MockLetter.LOREM_IPSUM_SECTION_1_10_32 }
                        ]
                    }
                },
                new DocumentConfigParagraph()
                {
                    Order = 7,
                    DocumentParagraph = new DocumentParagraph
                    {
                        DocumentParagraphId = 8,
                        Name = MockLetter.PARAGRAPH_4,
                        Contents =
                        [
                            new DocumentContent { DocumentContentId = 29, Order = 29, Name = MockLetter.PARAGRAPH_4_TEXT, RenderCellCode = MockLetter.PARAGRAPH_4, Content = MockLetter.LOREM_IPSUM_SECTION_1_10_32_HACKMAN_1914 }
                        ]
                    }
                },
                //new DocumentConfigParagraph()
                //{
                //    Order = 8,
                //    DocumentParagraph = new DocumentParagraph
                //    {
                //        DocumentParagraphId = 9,
                //        Name = MockLetter.PARAGRAPH_5,
                //        Contents =
                //        [
                //            new DocumentContent { DocumentContentId = 30, Order = 30, Name = MockLetter.PARAGRAPH_5_TEXT, RenderCellCode = MockLetter.PARAGRAPH_5, Content = MockLetter.LOREM_IPSUM_SECTION_1_10_33 }
                //        ]
                //    }
                //},
                //new DocumentConfigParagraph()
                //{
                //    Order = 9,
                //    DocumentParagraph = new DocumentParagraph
                //    {
                //        DocumentParagraphId = 10,
                //        Name = MockLetter.PARAGRAPH_6,
                //        Contents =
                //        [
                //            new DocumentContent { DocumentContentId = 31, Order = 31, Name = MockLetter.PARAGRAPH_6_TEXT, RenderCellCode = MockLetter.PARAGRAPH_6, Content = MockLetter.LOREM_IPSUM_SECTION_1_10_33_HACKMAN_1914 }
                //        ]
                //    }
                //},
                new DocumentConfigParagraph()
                {
                    Order = 10,
                    DocumentParagraph = new DocumentParagraph
                    {
                        DocumentParagraphId = 11,
                        DocumentParagraphType = DocumentParagraphType.Table,
                        Name = ParagraphNames.SIGNATURE_TABLE,
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
                            new DocumentTableCell { DocumentTableCellId = 1, RowNumber = 1, ColumnNumber = 1, CellCode = MockLetter.SIGNATURE_DETAILS }
                        ],
                        Contents =
                        [
                            new DocumentContent { DocumentContentId = 17, Order = 17, Name = MockLetter.SIGNATURE, RenderCellCode = MockLetter.SIGNATURE_DETAILS, ContentType = DocumentContentType.Image, Image = "signature.png", ImageHeight = 15, ImageWidth = 35 },
                            new DocumentContent { DocumentContentId = 18, Order = 18, Name = MockLetter.SIGNEE, RenderCellCode = MockLetter.SIGNATURE_DETAILS, Content = MockLetter.SIGNEE_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { DocumentContentId = 19, Order = 19, Name = MockLetter.SIGNEE_TITLE, RenderCellCode = MockLetter.SIGNATURE_DETAILS, Content = MockLetter.SIGNEE_TITLE_CONTENT, IgnoreParapgraphSpacing = true }
                        ]
                    }
                }
            ]
            };

            return documentConfig;
        }

        public static List<DocumentSubstitute> GetDocumentSubstitutes(bool skipSubValues, bool templateOnly = false)
        {
            List<DocumentSubstitute> documentSubstitutes = [];

            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_ID, Value = skipSubValues ? null : "123" });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.COMPANY_NAME, Value = skipSubValues ? null : "Global Banking Corp." });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.COMPANY_ADDRESS_1, Value = skipSubValues ? null : "9 Cherry Tree Lane" });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.COMPANY_ADDRESS_2, Value = skipSubValues ? null : "Canary Wharf" });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.COMPANY_ADDRESS_3, Value = skipSubValues ? null : "E14 5HQ" });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.COMPANY_PHONE_NUMBER, Value = skipSubValues ? null : "+44 071 946-0241" });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.COMPANY_EMAIL, Value = skipSubValues ? null : "gbc@email.com" });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_NAME, Value = skipSubValues ? null : "Mrs Jane Masters" });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_ADDRESS_1, Value = skipSubValues ? null : "142 Middle Street", Group = Substitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_ADDRESS_2, Value = skipSubValues ? null : "", Group = Substitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_ADDRESS_3, Value = skipSubValues ? null : "", Group = Substitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_ADDRESS_4, Value = skipSubValues ? null : "Brockham", Group = Substitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_ADDRESS_5, Value = skipSubValues ? null : "", Group = Substitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_ADDRESS_6, Value = skipSubValues ? null : "Surrey", Group = Substitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_ADDRESS_7, Value = skipSubValues ? null : "KT20 3AD", Group = Substitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_REFERENCE, Value = skipSubValues ? null : "MASTERS/ABC-123" });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.SIGNEE, Value = skipSubValues ? null : "Mrs Peggy Olson" });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.SIGNEE_TITLE, Value = skipSubValues ? null : "Managing Director" });

            if (!templateOnly)
            {
                documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_2, Value = skipSubValues ? null : "\"Lorem ipsum dolor sit amet consectetuer\"" });
                documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_4, Value = skipSubValues ? null : "Actually, it is nonsense" });
                documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_6, Value = skipSubValues ? null : "The phrase has been used for several centuries by typographers" });
                documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.TABLE_SUMMARY, Value = skipSubValues ? null : "Present Test Data in Table" });
                documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.TABLE_LINE_1, Value = skipSubValues ? null : "Line item no. 1" });
                documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.TABLE_LINE_1_AMOUNT, Value = skipSubValues ? null : "£70" });
                documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.TABLE_LINE_2, Value = skipSubValues ? null : "Line item no. 2" });
                documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.TABLE_LINE_2_AMOUNT, Value = skipSubValues ? null : "£30" });
                documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.TABLE_TOTAL, Value = skipSubValues ? null : "Total" });
                documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.TABLE_TOTAL_AMOUNT, Value = skipSubValues ? null : "£100" });
            }

            return documentSubstitutes;
        }
    }
}
