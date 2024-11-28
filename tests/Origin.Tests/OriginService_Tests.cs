using Origin.Core.Models;
using Origin.Service.Interface;
using Origin.Service.Providers;
using Origin.Service.Services;

namespace Origin.Tests
{
    [TestClass]
    public class OriginService_Tests
    {
        private static readonly CancellationToken cancellationToken = new();

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "documentGeneratorProvider")]
        public void Constructor_DocumentGeneratorProvider_Null_Expected_ArgumentNullException()
        {
            // Arrange
            IDocumentGeneratorProvider? documentGeneratorProvider = null;

            // Act
#pragma warning disable CS8604 // Possible null reference argument.
            _ = new DocumentWriterService(documentGeneratorProvider);
#pragma warning restore CS8604 // Possible null reference argument.
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "document")]
        public async Task ExecuteAsync_Document_Null_Expected_ArgumentNullException()
        {
            // Arrange
            DocumentGeneratorProvider documentServiceProvider = new();
            DocumentWriterService service = new(documentServiceProvider);
            Document? document = null;

            // Act
#pragma warning disable CS8604 // Possible null reference argument.
            await service.ExecuteAsync(document, cancellationToken);
#pragma warning restore CS8604 // Possible null reference argument.
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "documentConfig")]
        public async Task ExecuteAsync_DocumentConfig_Null_Expected_ArgumentNullException()
        {
            // Arrange
            DocumentGeneratorProvider documentGeneratorProvider = new();
            DocumentWriterService service = new(documentGeneratorProvider);
            Document? document = new();

            // Act
            await service.ExecuteAsync(document, cancellationToken);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Config")]
        public async Task ExecuteAsync_DocumentConfig_None_Expected_NotSupportedException()
        {
            // Arrange
            DocumentGeneratorProvider documentGeneratorProvider = new();
            DocumentWriterService service = new(documentGeneratorProvider);
            Document? document = new();

            // Act
            await service.ExecuteAsync(document, cancellationToken);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Target")]
        public async Task ExecuteAsync_DocumentGeneratorType_None_Expected_NotSupportedException()
        {
            // Arrange
            DocumentGeneratorProvider documentGeneratorProvider = new();
            DocumentWriterService service = new(documentGeneratorProvider);
            Document? document = new() { Config = new DocumentConfig() };

            // Act
            await service.ExecuteAsync(document, cancellationToken);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException), "DocumentServiceType.None")]
        public async Task ExecuteAsync_DocumentConfig_Null_Expected_NotSupportedException()
        {
            // Arrange
            DocumentGeneratorProvider documentGeneratorProvider = new();
            DocumentWriterService service = new(documentGeneratorProvider);
            Document? document = new()
            {
                Config = new DocumentConfig(),
                Target = "test"
            };

            // Act
            await service.ExecuteAsync(document, cancellationToken);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "OriginDocument.FilenameTemplate")]
        public async Task ExecuteAsync_OriginDocument_FilenameTemplate_Null_Expected_NullReferenceException()
        {
            // Arrange
            DocumentGeneratorProvider documentGeneratorProvider = new();
            DocumentWriterService service = new(documentGeneratorProvider);
            Document? document = new()
            {
                Config = new DocumentConfig(),
                Target = "test",
                DocumentGeneratorType = DocumentGeneratorType.OpenXmlDocument
            };

            // Act
            await service.ExecuteAsync(document, cancellationToken);
        }
    }
}
