using Origin.OpenXml.Sevices;

namespace Origin.Tests
{
    [TestClass]
    public class DocumentServiceBase_Tests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "outputLocation")]
        public void ValidateOutputLocation_OutputLocation_Null_Expected_ArgumentNullException()
        {
            // Arrange
            DocXDocumentService testDocumentService = new();

            string? outputLocation = null;

            // Act
            testDocumentService.ValidateOutputLocation(outputLocation);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "outputLocation")]
        public void ValidateOutputLocation_OutputLocation_Blank_Expected_ArgumentNullException()
        {
            // Arrange
            DocXDocumentService testDocumentService = new();

            string? outputLocation = string.Empty;

            // Act
            testDocumentService.ValidateOutputLocation(outputLocation);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "file")]
        public void FileDeleteIfExists_File_Null_Expected_ArgumentNullException()
        {
            // Arrange
            DocXDocumentService testDocumentService = new();

            string? file = null;

            // Act
            testDocumentService.FileDeleteIfExists(file);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "file")]
        public void FileDeleteIfExists_File_Null_Blank_Expected_ArgumentNullException()
        {
            // Arrange
            DocXDocumentService testDocumentService = new();

            string? file = string.Empty;

            // Act
            testDocumentService.FileDeleteIfExists(file);
        }
    }
}
