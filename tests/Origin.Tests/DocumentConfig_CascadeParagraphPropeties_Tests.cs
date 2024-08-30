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
                Paragraphs = [new DocumentParagraph
                {
                    Colour = "Red",
                    Font = "Test",
                    FontSize = 10,
                    Contents = [new DocumentContent()]
                },
                new DocumentParagraph
                {
                    DocumentParagraphType = DocumentParagraphType.Table,
                    Colour = "Blue",
                    Font = "HelloWorld",
                    FontSize = 10,
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

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            Assert.AreEqual(args.Paragraphs[1].Colour, args.Paragraphs[1].Table.Cells[0].Colour);
            Assert.AreEqual(args.Paragraphs[1].Font, args.Paragraphs[1].Table.Cells[0].Font);
            Assert.AreEqual(args.Paragraphs[1].FontSize, args.Paragraphs[1].Table.Cells[0].FontSize);

            Assert.AreEqual(args.Paragraphs[1].Table.Cells[0].Colour, args.Paragraphs[1].Table.Cells[0].Contents[0].Colour);
            Assert.AreEqual(args.Paragraphs[1].Table.Cells[0].Font, args.Paragraphs[1].Table.Cells[0].Contents[0].Font);
            Assert.AreEqual(args.Paragraphs[1].Table.Cells[0].FontSize, args.Paragraphs[1].Table.Cells[0].Contents[0].FontSize);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
    }
}