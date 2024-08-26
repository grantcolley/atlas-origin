using Origin.Model;
using Origin.OpenXml.DocX.Sevices;
using Origin.Pdf.Services;
using Origin.Service.Services;
using Origin.Service.Interface;
using Origin.Service.Providers;
using Origin.Tests.Helpers;
using Origin.Test.Data;

namespace Origin.Tests
{
    [TestClass]
    public class OriginService_Tests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "documentServiceProvider")]
        public void Constructor_DocumentServiceProvider_Null_Expected_ArgumentNullException()
        {
            // Arrange
            IDocumentServiceProvider? documentServiceProvider = null;

            // Act
#pragma warning disable CS8604 // Possible null reference argument.
            _ = new OriginService(documentServiceProvider);
#pragma warning restore CS8604 // Possible null reference argument.
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "documentArgs")]
        public void TryCreate_DocumentArgs_Null_Expected_ArgumentNullException()
        {
            // Arrange
            IDocumentService docxDocumentService = new DocXDocumentService();
            IDocumentService pdfDocumentService = new PdfDocumentService();
            DocumentServiceProvider documentServiceProvider = new([docxDocumentService, pdfDocumentService]);
            OriginService originationService = new(documentServiceProvider);
            DocumentConfig? documentArgs = null;

            // Act
#pragma warning disable CS8604 // Possible null reference argument.
            _ = originationService.TryCreate(documentArgs, out _);
#pragma warning restore CS8604 // Possible null reference argument.
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException), "DocumentServiceType.None")]
        public void TryCreate_DocumentArgs_Null_Expected_NotSupportedException()
        {
            // Arrange
            IDocumentService docxDocumentService = new DocXDocumentService();
            IDocumentService pdfDocumentService = new PdfDocumentService();
            DocumentServiceProvider documentServiceProvider = new([docxDocumentService, pdfDocumentService]);
            OriginService originationService = new(documentServiceProvider);
            DocumentConfig? documentArgs = new();

            // Act
            _ = originationService.TryCreate(documentArgs, out _);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "OriginDocument.Filename")]
        public void TryCreate_OriginDocument_Filename_Null_Expected_NullReferenceException()
        {
            // Arrange
            IDocumentService docxDocumentService = new DocXDocumentService();
            IDocumentService pdfDocumentService = new PdfDocumentService();
            DocumentServiceProvider documentServiceProvider = new([docxDocumentService, pdfDocumentService]);
            OriginService originationService = new(documentServiceProvider);
            DocumentConfig? documentArgs = new()
            {
                DocumentServiceType = DocumentServiceType.OpenXmlDocument
            };

            // Act
            _ = originationService.TryCreate(documentArgs, out _);
        }

        [TestMethod]
        public void TryCreate_Pass()
        {
            // Arrange
            IDocumentService docxDocumentService = new TestDocxDocumentService();
            IDocumentService pdfDocumentService = new PdfDocumentService();
            DocumentServiceProvider documentServiceProvider = new([docxDocumentService, pdfDocumentService]);
            OriginService originationService = new(documentServiceProvider);
            DocumentSubstitute customerId = new() { Key = Substitutes.CUSTOMER_ID, Value = "123" };
            DocumentConfig? documentArgs = new()
            {
                DocumentServiceType = DocumentServiceType.OpenXmlDocument,
                FilenameTemplate = $"Filename_[{Substitutes.CUSTOMER_ID}]",
                OutputLocation = Directory.GetCurrentDirectory(),
                Substitutes = new List<DocumentSubstitute>([customerId])
            };

            // Act
            bool result = originationService.TryCreate(documentArgs, out string fullFilename);

            // Assert
            Assert.IsTrue(result);
            Assert.IsNotNull(Path.Combine(Directory.GetCurrentDirectory(), $"Filename_{customerId.Value}"), fullFilename);
        }
    }
}
