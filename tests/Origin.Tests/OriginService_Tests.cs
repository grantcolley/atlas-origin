using Origin.Core.Models;
using Origin.OpenXml.Sevices;
using Origin.PdfSharp.Services;
using Origin.Service.Interface;
using Origin.Service.Providers;
using Origin.Service.Services;

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
        [ExpectedException(typeof(ArgumentNullException), "documentConfig")]
        public void TryCreate_DocumentConfig_Null_Expected_ArgumentNullException()
        {
            // Arrange
            IDocumentService docxDocumentService = new DocXDocumentService();
            IDocumentService pdfDocumentService = new PdfDocumentService();
            DocumentServiceProvider documentServiceProvider = new([docxDocumentService, pdfDocumentService]);
            OriginService originationService = new(documentServiceProvider);
            DocumentConfig? documentConfig = null;

            // Act
#pragma warning disable CS8604 // Possible null reference argument.
            originationService.CreateFile(documentConfig, out _);
#pragma warning restore CS8604 // Possible null reference argument.
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "OutputLocation")]
        public void TryCreate_DocumentServiceType_None_Expected_NotSupportedException()
        {
            // Arrange
            IDocumentService docxDocumentService = new DocXDocumentService();
            IDocumentService pdfDocumentService = new PdfDocumentService();
            DocumentServiceProvider documentServiceProvider = new([docxDocumentService, pdfDocumentService]);
            OriginService originationService = new(documentServiceProvider);
            DocumentConfig? documentConfig = new();

            // Act
            originationService.CreateFile(documentConfig, out _);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException), "DocumentServiceType.None")]
        public void TryCreate_DocumentConfig_Null_Expected_NotSupportedException()
        {
            // Arrange
            IDocumentService docxDocumentService = new DocXDocumentService();
            IDocumentService pdfDocumentService = new PdfDocumentService();
            DocumentServiceProvider documentServiceProvider = new([docxDocumentService, pdfDocumentService]);
            OriginService originationService = new(documentServiceProvider);
            DocumentConfig? documentArgs = new() { OutputLocation = "test" };

            // Act
            originationService.CreateFile(documentArgs, out _);
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
            originationService.CreateFile(documentArgs, out _);
        }
    }
}
