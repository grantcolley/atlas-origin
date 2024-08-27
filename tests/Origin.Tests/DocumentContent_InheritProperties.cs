using Origin.Core.Extensions;
using Origin.Core.Model;

namespace Origin.Tests
{
    [TestClass]    
    public class DocumentContent_InheritProperties
    {
        [TestMethod]
        public void Inherit_All_Properties_Pass()
        {
            // Arrange
            DocumentContent content1 = new();
            DocumentContent content2 = new()
            {
                Colour = "Red",
                Font = "Test",
                FontSize = 10,
                Bold = true,
                Italic = true,
                Underscore = true
            };

            // Act
            content1.InheritProperties(content2);

            // Assert
            Assert.AreEqual(content2.Colour, content1.Colour);
            Assert.AreEqual(content2.Font, content1.Font);
            Assert.AreEqual(content2.FontSize, content1.FontSize);
            Assert.AreEqual(content2.Bold, content1.Bold);
            Assert.AreEqual(content2.Italic, content1.Italic);
            Assert.AreEqual(content2.Underscore, content1.Underscore);
        }

        [TestMethod]
        public void Inherit_Partial_Properties_Pass()
        {
            // Arrange
            DocumentContent content1 = new()
            {
                Colour = "Blue",
                Font = "HelloWorld",
                FontSize = 15
            };

            DocumentContent content2 = new()
            {
                Colour = "Red",
                Font = "Test",
                FontSize = 10,
                Bold = true,
                Italic = true,
                Underscore = true
            };

            // Act
            content1.InheritProperties(content2);

            // Assert
            Assert.AreEqual("Blue", content1.Colour);
            Assert.AreEqual("HelloWorld", content1.Font);
            Assert.AreEqual(15, content1.FontSize);
            Assert.AreEqual(content2.Bold, content1.Bold);
            Assert.AreEqual(content2.Italic, content1.Italic);
            Assert.AreEqual(content2.Underscore, content1.Underscore);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "inherit")]
        public void Inherit_Null_Expected_ArgumentNullException_Pass()
        {
            // Arrange
            DocumentContent content1 = new()
            {
                Colour = "Blue",
                Font = "HelloWorld",
                FontSize = 15
            };

            DocumentContent? content2 = null;

            // Act
#pragma warning disable CS8604 // Possible null reference argument.
            content1.InheritProperties(content2);
#pragma warning restore CS8604 // Possible null reference argument.
        }

        [TestMethod]
        public void Inherit_No_Propeties_Pass()
        {
            // Arrange
            DocumentContent content1 = new()
            {
                Colour = "Red",
                Font = "Test",
                FontSize = 10,
                Bold = true,
                Italic = true,
                Underscore = true
            };

            DocumentContent content2 = new();

            // Act
            content1.InheritProperties(content2);

            // Assert
            Assert.AreEqual("Red", content1.Colour);
            Assert.AreEqual("Test", content1.Font);
            Assert.AreEqual(10, content1.FontSize);
            Assert.AreEqual(true, content1.Bold);
            Assert.AreEqual(true, content1.Italic);
            Assert.AreEqual(true, content1.Underscore);
        }
    }
}