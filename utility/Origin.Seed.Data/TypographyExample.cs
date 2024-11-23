using Origin.Core.Extensions;
using Origin.Core.Models;

namespace Origin.Test.Data
{
    public static class TypographyExample
    {
        public static DocumentConfig Build(DocumentConfig cloneConfig)
        {
            DocumentConfig documentConfig = cloneConfig.Clone();
            documentConfig.Name = "A Typography Example";
            documentConfig.Substitutes.Clear();

            documentConfig.ConfigParagraphs.Add(new DocumentConfigParagraph()
            {
                Order = 3,
                DocumentParagraph = new DocumentParagraph
                {
                    Name = "Typography Lorem Ipsum Paragraph",
                    Font = "Times New Roman",
                    FontSize = 12,
                    Colour = "65,105,225",
                    Contents =
                        [
                            new DocumentContent { Order = 1, Name = LoremIpsumContent.PARAGRAPH_1_TEXT_1, Content = LoremIpsumContent.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_CONTENT_1 },
                            new DocumentContent { Order = 2, Name = LoremIpsumContent.PARAGRAPH_1_TEXT_2, Content = LoremIpsumContent.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_CONTENT_2, Font = "Arial", FontSize = 14, Bold = true },
                            new DocumentContent { Order = 3, Name = LoremIpsumContent.PARAGRAPH_1_TEXT_3, Content = LoremIpsumContent.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_CONTENT_3 },
                            new DocumentContent { Order = 4, Name = LoremIpsumContent.PARAGRAPH_1_TEXT_4, Content = LoremIpsumContent.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_CONTENT_4, Font = "Comic Sans MS", FontSize = 14, Colour = "255,0,0" },
                            new DocumentContent { Order = 5, Name = LoremIpsumContent.PARAGRAPH_1_TEXT_5, Content = LoremIpsumContent.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_CONTENT_5 },
                            new DocumentContent { Order = 6, Name = LoremIpsumContent.PARAGRAPH_1_TEXT_6, Content = LoremIpsumContent.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_CONTENT_6, FontSize = 14, Colour = "192,0,0", Italic = true, Underscore = true },
                            new DocumentContent { Order = 7, Name = LoremIpsumContent.PARAGRAPH_1_TEXT_7, Content = LoremIpsumContent.LOREM_IPSUM_IS_NONSENSE_MICROSOFT_CONTENT_7 }
                        ]
                }
            });

            return documentConfig;
        }
    }
}
