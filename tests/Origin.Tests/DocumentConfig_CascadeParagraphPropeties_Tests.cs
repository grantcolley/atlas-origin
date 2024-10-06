using Origin.Core.Extensions;
using Origin.Core.Models;

namespace Origin.Tests
{
    [TestClass]    
    public class DocumentConfig_CascadeParagraphPropeties_Tests
    {
        [TestMethod]
        public void CascadeProperties_Pass()
        {
            // Arrange
            DocumentConfig args = new()
            {
                Colour = "Blue",
                Font = "HelloWorld",
                FontSize = 10,
                SubstituteStart = "["
            };

            args.ConfigParagraphs =
            [
                new DocumentConfigParagraph()
                {
                    DocumentConfig = args,
                    DocumentParagraph = new DocumentParagraph
                    {
                        Colour = "Red",
                        Font = "Test",
                        FontSize = 12,
                        SubstituteEnd = "]",
                        Contents = [new DocumentContent()]
                    }
                },
                new DocumentConfigParagraph()
                {
                    DocumentConfig = args,
                    DocumentParagraph = new DocumentParagraph
                    {
                        SubstituteStart = "%",
                        SubstituteEnd= "&",
                        DocumentParagraphType = DocumentParagraphType.Table,
                        Contents = [new DocumentContent { SubstituteStart = "(", SubstituteEnd = ")" }]
                    }
                }
            ];

            // Act
            foreach (DocumentParagraph paragraph in args.ConfigParagraphs.Select(cp => cp.DocumentParagraph))
            {
                paragraph.InheritParagraphProperties(args);

                foreach (DocumentContent documentContent in paragraph.Contents)
                {
                    documentContent.InheritContentProperties(paragraph);
                }
            }

            // Assert
            Assert.AreEqual(args.ConfigParagraphs.Select(cp => cp.DocumentParagraph).ToList()[0].Colour, args.ConfigParagraphs.Select(cp => cp.DocumentParagraph).ToList()[0].Contents[0].Colour);
            Assert.AreEqual(args.ConfigParagraphs.Select(cp => cp.DocumentParagraph).ToList()[0].Font, args.ConfigParagraphs.Select(cp => cp.DocumentParagraph).ToList()[0].Contents[0].Font);
            Assert.AreEqual(args.ConfigParagraphs.Select(cp => cp.DocumentParagraph).ToList()[0].FontSize, args.ConfigParagraphs.Select(cp => cp.DocumentParagraph).ToList()[0].Contents[0].FontSize);
            Assert.AreEqual(args.SubstituteStart, args.ConfigParagraphs.Select(cp => cp.DocumentParagraph).ToList()[0].Contents[0].SubstituteStart);
            Assert.AreEqual(args.ConfigParagraphs.Select(cp => cp.DocumentParagraph).ToList()[0].SubstituteEnd, args.ConfigParagraphs.Select(cp => cp.DocumentParagraph).ToList()[0].Contents[0].SubstituteEnd);
            Assert.AreEqual("[", args.ConfigParagraphs.Select(cp => cp.DocumentParagraph).ToList()[0].Contents[0].SubstituteStart);
            Assert.AreEqual("]", args.ConfigParagraphs.Select(cp => cp.DocumentParagraph).ToList()[0].Contents[0].SubstituteEnd);
            Assert.AreEqual("(", args.ConfigParagraphs.Select(cp => cp.DocumentParagraph).ToList()[1].Contents[0].SubstituteStart);
            Assert.AreEqual(")", args.ConfigParagraphs.Select(cp => cp.DocumentParagraph).ToList()[1].Contents[0].SubstituteEnd);

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            Assert.AreEqual(args.Colour, args.ConfigParagraphs.Select(cp => cp.DocumentParagraph).ToList()[1].Colour);
            Assert.AreEqual(args.Font, args.ConfigParagraphs.Select(cp => cp.DocumentParagraph).ToList()[1].Font);
            Assert.AreEqual(args.FontSize, args.ConfigParagraphs.Select(cp => cp.DocumentParagraph).ToList()[1].FontSize);

            Assert.AreEqual(args.Colour, args.ConfigParagraphs.Select(cp => cp.DocumentParagraph).ToList()[1].Contents[0].Colour);
            Assert.AreEqual(args.Font, args.ConfigParagraphs.Select(cp => cp.DocumentParagraph).ToList()[1].Contents[0].Font);
            Assert.AreEqual(args.FontSize, args.ConfigParagraphs.Select(cp => cp.DocumentParagraph).ToList()[1].Contents[0].FontSize);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
    }
}