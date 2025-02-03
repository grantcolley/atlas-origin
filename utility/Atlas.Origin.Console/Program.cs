// See https://aka.ms/new-console-template for more information
using Origin.Core.Models;
using Origin.Generator.Html.Services;
using Origin.Generator.OpenXml.Services;
using Origin.Generator.PdfSharp.Services;
using Origin.Service.Interface;
using Origin.Service.Providers;
using Origin.Service.Services;
using Origin.Tests.Data;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

Console.WriteLine("Origin.Console!");

string outputLocation = @"..\..\..\..\output";

CancellationToken cancellationToken = new ();
JsonSerializerOptions jsonSerializerOptions = new() { WriteIndented = true, ReferenceHandler = ReferenceHandler.Preserve };

// Eager load into the application domain
_ = typeof(DocXDocumentGenerator).Assembly;
_ = typeof(PdfDocumentGenerator).Assembly;
_ = typeof(HtmlDocumentGenerator).Assembly;

IDocumentGeneratorProvider documentServiceProvider = new DocumentGeneratorProvider();
DocumentWriterService originationWriterService = new(documentServiceProvider);

Document documentOpenXml = TestData.GetDocumentOpenXml(outputLocation);
Document documentPdfSharp = TestData.GetDocumentPdfSharp(outputLocation);
Document documentHtml = TestData.GetDocumentHtml(outputLocation);

#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable CS8602 // Dereference of a possibly null reference.
documentOpenXml.Config.ApplySubstitutes = true;
documentPdfSharp.Config.ApplySubstitutes = true;
documentHtml.Config.ApplySubstitutes = true;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore IDE0079 // Remove unnecessary suppression

try
{
    async Task GenerateFileAsync(Document document)
    {
        string fullFilename = await originationWriterService.ExecuteAsync(document, cancellationToken).ConfigureAwait(false);

        Console.WriteLine(JsonSerializer.Serialize(document, jsonSerializerOptions));

        Process.Start(new ProcessStartInfo(fullFilename) { UseShellExecute = true });
    }

    await GenerateFileAsync(documentOpenXml);

    await GenerateFileAsync(documentPdfSharp);

    await GenerateFileAsync(documentHtml);
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}
