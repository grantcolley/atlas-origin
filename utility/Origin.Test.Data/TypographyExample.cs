using Origin.Core.Extensions;
using Origin.Core.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;

namespace Origin.Test.Data
{
    public static class TypographyExample
    {
        public static DocumentConfig Build(DocumentConfig cloneConfig)
        {
            DocumentConfig documentConfig = cloneConfig.Clone();
            documentConfig.Name = "Customer Product Letter";
            documentConfig.Substitutes.Clear();

            documentConfig.ConfigParagraphs.Add(new DocumentConfigParagraph()
            {
                Order = 3,
                DocumentParagraph = new DocumentParagraph
                {
                    Name = "Customer Product Letter Paragraph",
                    Font = "Times New Roman",
                    FontSize = 12,
                    Colour = "65,105,225",
                    Contents =
                        [
                            new DocumentContent { DocumentContentId = 21, Order = 21, Name = LoremIpsumContent.PARAGRAPH_1_TEXT_1, Content = LoremIpsumContent.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_CONTENT_1 },
                            new DocumentContent { DocumentContentId = 22, Order = 22, Name = LoremIpsumContent.PARAGRAPH_1_TEXT_2, Content = LoremIpsumContent.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_CONTENT_2, Font = "Arial", FontSize = 14, Bold = true },
                            new DocumentContent { DocumentContentId = 23, Order = 23, Name = LoremIpsumContent.PARAGRAPH_1_TEXT_3, Content = LoremIpsumContent.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_CONTENT_3 },
                            new DocumentContent { DocumentContentId = 24, Order = 24, Name = LoremIpsumContent.PARAGRAPH_1_TEXT_4, Content = LoremIpsumContent.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_CONTENT_4, Font = "Comic Sans MS", FontSize = 14, Colour = "255,0,0" },
                            new DocumentContent { DocumentContentId = 25, Order = 25, Name = LoremIpsumContent.PARAGRAPH_1_TEXT_5, Content = LoremIpsumContent.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_CONTENT_5 },
                            new DocumentContent { DocumentContentId = 26, Order = 26, Name = LoremIpsumContent.PARAGRAPH_1_TEXT_6, Content = LoremIpsumContent.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_CONTENT_6, FontSize = 14, Colour = "192,0,0", Italic = true, Underscore = true },
                            new DocumentContent { DocumentContentId = 27, Order = 27, Name = LoremIpsumContent.PARAGRAPH_1_TEXT_7, Content = LoremIpsumContent.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_CONTENT_7 }
                        ]
                }
            });

            return documentConfig;
        }
    }
}
