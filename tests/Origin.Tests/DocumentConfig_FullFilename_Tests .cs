using Origin.Core.Extensions;
using Origin.Core.Models;
using Origin.Test.Data;
using Origin.Tests.Helpers;

namespace Origin.Tests
{
    [TestClass]
    public class DocumentArgs_FullFilename_Tests
    {
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "FilenameTemplate")]
        public void FilenameTemplate_Null_Expected_NullReferenceException()
        {
            // Arrange
            DocumentConfig documentArgs = new()
            {
                OutputLocation = "OutputLocationTest"
            };

            // Act
            _ = documentArgs.FullFilename();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "OutputLocation")]
        public void OutputLocation_Null_Expected_NullReferenceException()
        {
            // Arrange
            DocumentConfig documentArgs = new()
            {
                FilenameTemplate = "FilenameTemplateTest"
            };

            // Act
            _ = documentArgs.FullFilename();
        }

        [TestMethod]
        public void Filename_Id_Pass()
        {
            // Arrange
            DocumentSubstitute customerId = new() { Key = Substitutes.CUSTOMER_ID, Value = "123" };

            DocumentConfig documentArgs = new()
            {
                FilenameTemplate = $"Filename_[{Substitutes.CUSTOMER_ID}]",
                OutputLocation = "OutputLocationTest",
                SubstituteStart = '[',
                SubstituteEnd = ']',
                Substitutes = new List<DocumentSubstitute>([customerId])
            };

            // Act
            string fullFilename = documentArgs.FullFilename();

            // Assert
            Assert.AreEqual(Path.Combine(documentArgs.OutputLocation, $"Filename_{customerId.Value}"), fullFilename);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException), "Date")]
        public void Substitute_Not_Found_NullReferenceException()
        {
            // Arrange
            DocumentSubstitute customerId = new() { Key = Substitutes.CUSTOMER_ID, Value = "123" };

            CustomDocumentConfig customDocumentArgs = new()
            {
                FilenameTemplate = "Filename_[CUSTOMER_ID]_[DATE]",
                OutputLocation = "OutputLocationTest",
                SubstituteStart = '[',
                SubstituteEnd = ']',
                Substitutes = new List<DocumentSubstitute>([customerId])
            };

            // Act
            _ = customDocumentArgs.FullFilename();
        }
    }
}