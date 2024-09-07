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
                Paragraphs = [new DocumentParagraph
                {
                    Colour = "Red",
                    Font = "Test",
                    FontSize = 12,
                    Contents = [new DocumentContent()]
                },
                new DocumentParagraph
                {
                    DocumentParagraphType = DocumentParagraphType.Table,
                    Contents = [new DocumentContent()]
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