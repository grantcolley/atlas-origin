using Origin.Core.Models;

namespace Origin.Test.Data
{
    public static class BaseLetterTemplate
    {
        public static DocumentConfig GetBaseLetterTemplateDocumentConfig()
        {
            DocumentConfig documentConfig = new()
            {
                Name = "Base Letter Template",
                SubstituteStart = "[",
                SubstituteEnd = "]",
                Substitutes = GetDocumentSubstitutes()
            };

            documentConfig.ConfigParagraphs =
            [
                new DocumentConfigParagraph()
                {
                    Order = 200,
                    DocumentParagraph = new DocumentParagraph
                    {
                        DocumentParagraphType = DocumentParagraphType.Footer,
                        Name = "Base Letter Footer",
                        FontSize = 8,
                        IgnoreParapgraphSpacing = true,
                        Contents =
                        [
                            new DocumentContent { Order = 1, Name = BaseLetterTemplateContentNames.FOOTER_TEXT, Content = BaseLetterTemplateContent.FOOTER_CONTENT }
                        ]
                    }
                },
                new DocumentConfigParagraph()
                {
                    Order = 1,
                    DocumentParagraph = new DocumentParagraph
                    {
                        DocumentParagraphType = DocumentParagraphType.Table,
                        Name = "Base Letter Heading",
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
                            new DocumentTableCell{ RowNumber = 1, ColumnNumber = 1, CellCode = BaseLetterTemplateTableCellCodes.COMPANY_LOGO },
                            new DocumentTableCell{ RowNumber = 2, ColumnNumber = 3, CellCode = BaseLetterTemplateTableCellCodes.COMPANY_DETAILS },
                            new DocumentTableCell{ RowNumber = 3, ColumnNumber = 1, CellCode = BaseLetterTemplateTableCellCodes.CUSTOMER_DETAILS }
                        ],
                        Contents =
                        [
                            new DocumentContent { Order = 1, Name = BaseLetterTemplateContentNames.LOGO, RenderCellCode = BaseLetterTemplateTableCellCodes.COMPANY_LOGO, ContentType = DocumentContentType.Image, Image = BaseLetterTemplateContent.LOGO_CONTENT, ImageHeight = 15, ImageWidth = 35 },
                            new DocumentContent { Order = 2, Name = BaseLetterTemplateContentNames.COMPANY_NAME, RenderCellCode = BaseLetterTemplateTableCellCodes.COMPANY_DETAILS, Content = BaseLetterTemplateContent.COMPANY_NAME_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 3, Name = BaseLetterTemplateContentNames.COMPANY_ADDRESS_1, RenderCellCode = BaseLetterTemplateTableCellCodes.COMPANY_DETAILS, Content = BaseLetterTemplateContent.COMPANY_ADDRESS_1_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 4, Name = BaseLetterTemplateContentNames.COMPANY_ADDRESS_2, RenderCellCode = BaseLetterTemplateTableCellCodes.COMPANY_DETAILS, Content = BaseLetterTemplateContent.COMPANY_ADDRESS_2_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 5, Name = BaseLetterTemplateContentNames.COMPANY_ADDRESS_3, RenderCellCode = BaseLetterTemplateTableCellCodes.COMPANY_DETAILS, Content = BaseLetterTemplateContent.COMPANY_ADDRESS_3_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 6, Name = BaseLetterTemplateContentNames.COMPANY_PHONE_NUMBER, RenderCellCode = BaseLetterTemplateTableCellCodes.COMPANY_DETAILS, Content = BaseLetterTemplateContent.COMPANY_PHONE_NUMBER_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 7, Name = BaseLetterTemplateContentNames.COMPANY_EMAIL, RenderCellCode = BaseLetterTemplateTableCellCodes.COMPANY_DETAILS, Content = BaseLetterTemplateContent.COMPANY_EMAIL_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 8, Name = BaseLetterTemplateContentNames.CUSTOMER_NAME, RenderCellCode = BaseLetterTemplateTableCellCodes.CUSTOMER_DETAILS, Content = BaseLetterTemplateContent.CUSTOMER_NAME_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 9, Name = BaseLetterTemplateContentNames.CUSTOMER_ADDRESS_1, RenderCellCode = BaseLetterTemplateTableCellCodes.CUSTOMER_DETAILS, Content = BaseLetterTemplateContent.CUSTOMER_ADDRESS_1_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 10, Name = BaseLetterTemplateContentNames.CUSTOMER_ADDRESS_2, RenderCellCode = BaseLetterTemplateTableCellCodes.CUSTOMER_DETAILS, Content = BaseLetterTemplateContent.CUSTOMER_ADDRESS_2_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 11, Name = BaseLetterTemplateContentNames.CUSTOMER_ADDRESS_3, RenderCellCode = BaseLetterTemplateTableCellCodes.CUSTOMER_DETAILS, Content = BaseLetterTemplateContent.CUSTOMER_ADDRESS_3_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 12, Name = BaseLetterTemplateContentNames.CUSTOMER_ADDRESS_4, RenderCellCode = BaseLetterTemplateTableCellCodes.CUSTOMER_DETAILS, Content = BaseLetterTemplateContent.CUSTOMER_ADDRESS_4_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 13, Name = BaseLetterTemplateContentNames.CUSTOMER_ADDRESS_5, RenderCellCode = BaseLetterTemplateTableCellCodes.CUSTOMER_DETAILS, Content = BaseLetterTemplateContent.CUSTOMER_ADDRESS_5_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 14, Name = BaseLetterTemplateContentNames.CUSTOMER_ADDRESS_6, RenderCellCode = BaseLetterTemplateTableCellCodes.CUSTOMER_DETAILS, Content = BaseLetterTemplateContent.CUSTOMER_ADDRESS_6_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 15, Name = BaseLetterTemplateContentNames.CUSTOMER_ADDRESS_7, RenderCellCode = BaseLetterTemplateTableCellCodes.CUSTOMER_DETAILS, Content = BaseLetterTemplateContent.CUSTOMER_ADDRESS_7_CONTENT, IgnoreParapgraphSpacing = true }
                        ]
                    }
                },
                new DocumentConfigParagraph()
                {
                    Order = 2,
                    DocumentParagraph = new DocumentParagraph
                    {
                        Name = "Base Letter Template Title",
                        Font = "Courier",
                        FontSize = 16,
                        Colour = "0,176,240",
                        Contents =
                        [
                            new DocumentContent { Order = 1, Name = BaseLetterTemplateContentNames.LETTER_TITLE, Content = BaseLetterTemplateContent.LETTER_TITLE_CONTENT }
                        ]
                    }
                },
                new DocumentConfigParagraph()
                {
                    Order = 100,
                    DocumentParagraph = new DocumentParagraph
                    {
                        DocumentParagraphType = DocumentParagraphType.Table,
                        Name = "Base Letter Template Close",
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
                            new DocumentTableCell { RowNumber = 1, ColumnNumber = 1, CellCode = BaseLetterTemplateTableCellCodes.SIGNATURE_DETAILS }
                        ],
                        Contents =
                        [
                            new DocumentContent { Order = 1, Name = BaseLetterTemplateContentNames.SIGNATURE, RenderCellCode = BaseLetterTemplateTableCellCodes.SIGNATURE_DETAILS, ContentType = DocumentContentType.Image, Image = BaseLetterTemplateContent.SIGNATURE_CONTENT, ImageHeight = 15, ImageWidth = 35 },
                            new DocumentContent { Order = 2, Name = BaseLetterTemplateContentNames.SIGNEE, RenderCellCode = BaseLetterTemplateTableCellCodes.SIGNATURE_DETAILS, Content = BaseLetterTemplateContent.SIGNEE_CONTENT, IgnoreParapgraphSpacing = true },
                            new DocumentContent { Order = 3, Name = BaseLetterTemplateContentNames.SIGNEE_TITLE, RenderCellCode = BaseLetterTemplateTableCellCodes.SIGNATURE_DETAILS, Content = BaseLetterTemplateContent.SIGNEE_TITLE_CONTENT, IgnoreParapgraphSpacing = true }
                        ]
                    }
                }
            ];

            return documentConfig;
        }

        private static List<DocumentSubstitute> GetDocumentSubstitutes()
        {
            List<DocumentSubstitute> documentSubstitutes = [];

            documentSubstitutes.Add(new DocumentSubstitute { Key = BaseLetterTemplateSubstitutes.COMPANY_NAME });
            documentSubstitutes.Add(new DocumentSubstitute { Key = BaseLetterTemplateSubstitutes.COMPANY_ADDRESS_1 });
            documentSubstitutes.Add(new DocumentSubstitute { Key = BaseLetterTemplateSubstitutes.COMPANY_ADDRESS_2 });
            documentSubstitutes.Add(new DocumentSubstitute { Key = BaseLetterTemplateSubstitutes.COMPANY_ADDRESS_3 });
            documentSubstitutes.Add(new DocumentSubstitute { Key = BaseLetterTemplateSubstitutes.COMPANY_PHONE_NUMBER });
            documentSubstitutes.Add(new DocumentSubstitute { Key = BaseLetterTemplateSubstitutes.COMPANY_EMAIL });

            documentSubstitutes.Add(new DocumentSubstitute { Key = BaseLetterTemplateSubstitutes.CUSTOMER_TITLE });
            documentSubstitutes.Add(new DocumentSubstitute { Key = BaseLetterTemplateSubstitutes.CUSTOMER_FIRST_NAME });
            documentSubstitutes.Add(new DocumentSubstitute { Key = BaseLetterTemplateSubstitutes.CUSTOMER_SURNAME });
            documentSubstitutes.Add(new DocumentSubstitute { Key = BaseLetterTemplateSubstitutes.CUSTOMER_ADDRESS_1, Group = BaseLetterTemplateSubstitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = BaseLetterTemplateSubstitutes.CUSTOMER_ADDRESS_2, Group = BaseLetterTemplateSubstitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = BaseLetterTemplateSubstitutes.CUSTOMER_ADDRESS_3, Group = BaseLetterTemplateSubstitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = BaseLetterTemplateSubstitutes.CUSTOMER_ADDRESS_4, Group = BaseLetterTemplateSubstitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = BaseLetterTemplateSubstitutes.CUSTOMER_ADDRESS_5, Group = BaseLetterTemplateSubstitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = BaseLetterTemplateSubstitutes.CUSTOMER_ADDRESS_6, Group = BaseLetterTemplateSubstitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = BaseLetterTemplateSubstitutes.CUSTOMER_ADDRESS_7, Group = BaseLetterTemplateSubstitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = BaseLetterTemplateSubstitutes.CUSTOMER_SORT_CODE });
            documentSubstitutes.Add(new DocumentSubstitute { Key = BaseLetterTemplateSubstitutes.CUSTOMER_ACCOUNT_NUMBER });

            documentSubstitutes.Add(new DocumentSubstitute { Key = BaseLetterTemplateSubstitutes.SIGNEE });
            documentSubstitutes.Add(new DocumentSubstitute { Key = BaseLetterTemplateSubstitutes.SIGNEE_TITLE });

            return documentSubstitutes;
        }
    }
}
