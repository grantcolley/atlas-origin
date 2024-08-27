using Origin.Core.Model;
using Origin.OpenXml.Sevices;
using Origin.Service.Interface;
using Origin.Service.Providers;

namespace Origin.Tests
{
    [TestClass]
    public class DocumentServiceProvider_Tests
    {
        [TestMethod]
        public void GetDocumentService_Constructor_Injection_Pass()
        {
            // Arrange
            IDocumentService docxDocumentService = new DocXDocumentService();

            // Act
            DocumentServiceProvider documentServiceProvider = new([docxDocumentService]);
            IDocumentService? returnedDocxDocumentService = documentServiceProvider.GetDocumentService(DocumentServiceType.OpenXmlDocument);

            // Arrange
            Assert.AreEqual(docxDocumentService, returnedDocxDocumentService);
        }

        [TestMethod]
        public void AddDocumentService_Pass()
        {
            // Arrange
            DocumentServiceProvider documentServiceProvider = new();
            IDocumentService docxDocumentService = new DocXDocumentService();

            // Act
            documentServiceProvider.AddDocumentService(docxDocumentService);
            IDocumentService? returnedDocxDocumentService = documentServiceProvider.GetDocumentService(DocumentServiceType.OpenXmlDocument);

            // Arrange
            Assert.AreEqual(docxDocumentService, returnedDocxDocumentService);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException), "DocumentServiceType.None")]
        public void ValidateOutputLocation_OutputLocation_Null_Expected_ArgumentNullException()
        {
            // Arrange
            DocumentServiceProvider documentServiceProvider = new();

            // Act
            _ = documentServiceProvider.GetDocumentService(DocumentServiceType.None);
        }
    }
}
