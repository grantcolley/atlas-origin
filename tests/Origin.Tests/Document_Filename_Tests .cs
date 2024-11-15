using Origin.Core.Extensions;
using Origin.Core.Models;
using Origin.Test.Data;
using Origin.Tests.Helpers;

namespace Origin.Tests
{
    [TestClass]
    public class Document_Filename_Tests
    {
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "FilenameTemplate")]
        public void FilenameTemplate_Null_Expected_NullReferenceException()
        {
            // Arrange
            Document document = new()
            {
                Config = new DocumentConfig(),
                Target = "Target"
            };

            // Act
            _ = document.Filename("docx");
        }

        [TestMethod]
        public void Filename_Id_Pass()
        {
            // Arrange
            DocumentSubstitute customerId = new() { Key = Substitutes.CUSTOMER_ID, Value = "123" };

            Document document = new()
            {
                FilenameTemplate = $"Filename_[{Substitutes.CUSTOMER_ID}]",
                Target = "Target",
                  Config = new DocumentConfig()
                  {
                      SubstituteStart = "[",
                      SubstituteEnd = "]",
                      Substitutes = new List<DocumentSubstitute>([customerId])
                  }
            };

            // Act
            string filename = document.Filename("docx");

            // Assert
            Assert.AreEqual(document.Filename, filename);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException), "Date")]
        public void Substitute_Not_Found_NullReferenceException()
        {
            // Arrange
            DocumentSubstitute customerId = new() { Key = Substitutes.CUSTOMER_ID, Value = "123" };

            Document document = new()
            {
                FilenameTemplate = $"Filename_[{Substitutes.CUSTOMER_ID}]_[DATE]",
                Target = "Target",
                Config = new CustomDocumentConfig()
                {
                    SubstituteStart = "[",
                    SubstituteEnd = "]",
                    Substitutes = new List<DocumentSubstitute>([customerId])
                }
            };

            // Act
            _ = document.Filename("docx");
        }
    }
}