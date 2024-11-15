using Origin.Core.Models;
using Origin.Service.Providers;

namespace Origin.Tests
{
    [TestClass]
    public class DocumentGeneratorProvider_Tests
    {
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException), "DocumentServiceType.None")]
        public void ValidateOutputLocation_OutputLocation_Null_Expected_ArgumentNullException()
        {
            // Arrange
            DocumentGeneratorProvider documentGeneratorProvider = new();

            // Act
            _ = documentGeneratorProvider.GetDocumentGenerator(DocumentGeneratorType.None);
        }
    }
}
