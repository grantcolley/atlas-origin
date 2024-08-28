using Origin.Core.Models;
using Origin.OpenXml.Sevices;
using Origin.Pdf.Services;
using Origin.Service.Extensions;
using Origin.Service.Interface;
using Origin.Service.Providers;

namespace Origin.Tests
{
    [TestClass]
    public class DocumentServiceProviderExtensions_Tests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "documentServices")]
        public void Constructor_DocumentServices_Null_Expected_ArgumentNullException()
        {
            // Arrange
            IDocumentService[]? documentServices = null;

            // Act
#pragma warning disable CS8604 // Possible null reference argument.
            _ = new DocumentServiceProvider(documentServices);
#pragma warning restore CS8604 // Possible null reference argument.
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "documentServices")]
        public void AddProviders_DocumentServices_Null_Expected_ArgumentNullException()
        {
            // Arrange
            DocumentServiceProvider documentServiceProvider = new();
            IEnumerable<IDocumentService>? documentServices = null;

            // Act
#pragma warning disable CS8604 // Possible null reference argument.
            _ = documentServiceProvider.AddProviders(documentServices);
#pragma warning restore CS8604 // Possible null reference argument.
        }

        [TestMethod]
        public void AddProviders_Pass()
        {
            // Arrange
            DocumentServiceProvider documentServiceProvider = new();
            IDocumentService docxDocumentService = new DocXDocumentService();
            IDocumentService pdfDocumentService = new PdfDocumentService();
            IEnumerable<IDocumentService>? documentServices = [docxDocumentService, pdfDocumentService];

            // Act
            IDocumentServiceProvider returnedDocumentServiceProvider = documentServiceProvider.AddProviders(documentServices);
            IDocumentService returnedDocxDocumentService = returnedDocumentServiceProvider.GetDocumentService(DocumentServiceType.OpenXmlDocument);
            IDocumentService returnedPdfDocumentService = returnedDocumentServiceProvider.GetDocumentService(DocumentServiceType.PdfSharp);

            // Assert
            Assert.AreEqual(documentServiceProvider, returnedDocumentServiceProvider);
            Assert.AreEqual(docxDocumentService, returnedDocxDocumentService);
            Assert.AreEqual(pdfDocumentService, returnedPdfDocumentService);
        }
    }
}
