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
                SubstituteStart = "[",
                Paragraphs = [new DocumentParagraph
                {
                    Colour = "Red",
                    Font = "Test",
                    FontSize = 12,
                    SubstituteEnd = "]",
                    Contents = [new DocumentContent()]
                },
                new DocumentParagraph
                {
                    SubstituteStart = "%",
                    SubstituteEnd= "&",
                    DocumentParagraphType = DocumentParagraphType.Table,
                    Contents = [new DocumentContent { SubstituteStart = "(", SubstituteEnd = ")" }]
                }]
            };

            // Act
            foreach (DocumentParagraph paragraph in args.Paragraphs)
            {
                paragraph.InheritParagraphProperties(args);

                foreach (DocumentContent documentContent in paragraph.Contents)
                {
                    documentContent.InheritContentProperties(paragraph);
                }
            }

            // Assert
            Assert.AreEqual(args.Paragraphs[0].Colour, args.Paragraphs[0].Contents[0].Colour);
            Assert.AreEqual(args.Paragraphs[0].Font, args.Paragraphs[0].Contents[0].Font);
            Assert.AreEqual(args.Paragraphs[0].FontSize, args.Paragraphs[0].Contents[0].FontSize);
            Assert.AreEqual(args.SubstituteStart, args.Paragraphs[0].Contents[0].SubstituteStart);
            Assert.AreEqual(args.Paragraphs[0].SubstituteEnd, args.Paragraphs[0].Contents[0].SubstituteEnd);
            Assert.AreEqual("[", args.Paragraphs[0].Contents[0].SubstituteStart);
            Assert.AreEqual("]", args.Paragraphs[0].Contents[0].SubstituteEnd);
            Assert.AreEqual("(", args.Paragraphs[1].Contents[0].SubstituteStart);
            Assert.AreEqual(")", args.Paragraphs[1].Contents[0].SubstituteEnd);

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            Assert.AreEqual(args.Colour, args.Paragraphs[1].Colour);
            Assert.AreEqual(args.Font, args.Paragraphs[1].Font);
            Assert.AreEqual(args.FontSize, args.Paragraphs[1].FontSize);

            Assert.AreEqual(args.Colour, args.Paragraphs[1].Contents[0].Colour);
            Assert.AreEqual(args.Font, args.Paragraphs[1].Contents[0].Font);
            Assert.AreEqual(args.FontSize, args.Paragraphs[1].Contents[0].FontSize);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
    }
}