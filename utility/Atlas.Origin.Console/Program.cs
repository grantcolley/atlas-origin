// See https://aka.ms/new-console-template for more information
using Origin.Core.Extensions;
using Origin.Core.Models;
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

IDocumentGeneratorProvider documentServiceProvider = new DocumentGeneratorProvider();
DocumentWriterService originationWriterService = new(documentServiceProvider);

Document documentOpenXml = TestData.GetDocumentOpenXml(outputLocation);
Document documentPdfSharp = TestData.GetDocumentPdfSharp(outputLocation);

#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable CS8602 // Dereference of a possibly null reference.
documentOpenXml.Config.ApplySubstitutes = true;
documentPdfSharp.Config.ApplySubstitutes = true;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore IDE0079 // Remove unnecessary suppression

try
{
    string fullFilename = await originationWriterService.ExecuteAsync(documentOpenXml, cancellationToken).ConfigureAwait(false);

    Console.WriteLine(JsonSerializer.Serialize(documentOpenXml, jsonSerializerOptions));

    Process.Start(new ProcessStartInfo(fullFilename) { UseShellExecute = true });

    fullFilename = await originationWriterService.ExecuteAsync(documentPdfSharp, cancellationToken).ConfigureAwait(false);

    Console.WriteLine(JsonSerializer.Serialize(documentPdfSharp, jsonSerializerOptions));

    Process.Start(new ProcessStartInfo(fullFilename) { UseShellExecute = true });
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}
