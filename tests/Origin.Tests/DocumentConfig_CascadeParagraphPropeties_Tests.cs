using Origin.Extensions;
using Origin.Model;

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
                Paragraphs = [new DocumentParagraph
                {
                    Colour = "Red",
                    Font = "Test",
                    FontSize = 10,
                    Bold = true,
                    Italic = true,
                    Underscore = true,
                    Contents = [new DocumentContent()]
                },
                new DocumentParagraph
                {
                    DocumentParagraphType = DocumentParagraphType.Table,
                    Colour = "Blue",
                    Font = "HelloWorld",
                    FontSize = 10,
                    Bold = true,
                    Italic = true,
                    Underscore = true,
                    Table = new DocumentTable
                    {
                        Cells = [new DocumentTableCell 
                        {
                            Contents = [new DocumentContent()]
                        }]
                    }
                }]
            };

            // Act
            args.CascadeParagraphPropeties();

            // Assert
            Assert.AreEqual(args.Paragraphs[0].Colour, args.Paragraphs[0].Contents[0].Colour);
            Assert.AreEqual(args.Paragraphs[0].Font, args.Paragraphs[0].Contents[0].Font);
            Assert.AreEqual(args.Paragraphs[0].FontSize, args.Paragraphs[0].Contents[0].FontSize);
            Assert.AreEqual(args.Paragraphs[0].Bold, args.Paragraphs[0].Contents[0].Bold);
            Assert.AreEqual(args.Paragraphs[0].Italic, args.Paragraphs[0].Contents[0].Italic);
            Assert.AreEqual(args.Paragraphs[0].Underscore, args.Paragraphs[0].Contents[0].Underscore);

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            Assert.AreEqual(args.Paragraphs[1].Colour, args.Paragraphs[1].Table.Cells[0].Colour);
            Assert.AreEqual(args.Paragraphs[1].Font, args.Paragraphs[1].Table.Cells[0].Font);
            Assert.AreEqual(args.Paragraphs[1].FontSize, args.Paragraphs[1].Table.Cells[0].FontSize);
            Assert.AreEqual(args.Paragraphs[1].Bold, args.Paragraphs[1].Table.Cells[0].Bold);
            Assert.AreEqual(args.Paragraphs[1].Italic, args.Paragraphs[1].Table.Cells[0].Italic);
            Assert.AreEqual(args.Paragraphs[1].Underscore, args.Paragraphs[1].Table.Cells[0].Underscore);

            Assert.AreEqual(args.Paragraphs[1].Table.Cells[0].Colour, args.Paragraphs[1].Table.Cells[0].Contents[0].Colour);
            Assert.AreEqual(args.Paragraphs[1].Table.Cells[0].Font, args.Paragraphs[1].Table.Cells[0].Contents[0].Font);
            Assert.AreEqual(args.Paragraphs[1].Table.Cells[0].FontSize, args.Paragraphs[1].Table.Cells[0].Contents[0].FontSize);
            Assert.AreEqual(args.Paragraphs[1].Table.Cells[0].Bold, args.Paragraphs[1].Table.Cells[0].Contents[0].Bold);
            Assert.AreEqual(args.Paragraphs[1].Table.Cells[0].Italic, args.Paragraphs[1].Table.Cells[0].Contents[0].Italic);
            Assert.AreEqual(args.Paragraphs[1].Table.Cells[0].Underscore, args.Paragraphs[1].Table.Cells[0].Contents[0].Underscore);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
    }
}