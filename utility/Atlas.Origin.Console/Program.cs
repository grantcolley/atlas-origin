// See https://aka.ms/new-console-template for more information
using Origin.Core.Models;
using Origin.OpenXml.Sevices;
using Origin.PdfSharp.Services;
using Origin.Service.Interface;
using Origin.Service.Providers;
using Origin.Service.Services;
using Origin.Test.Data;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

Console.WriteLine("Origin.Console!");

string outputLocation = @"..\..\..\..\output";

JsonSerializerOptions jsonSerializerOptions = new() { WriteIndented = true, ReferenceHandler = ReferenceHandler.Preserve };

IDocumentService[] documentServices = [new DocXDocumentService(), new PdfDocumentService()];
IDocumentServiceProvider documentServiceProvider = new DocumentServiceProvider(documentServices);
OriginService originationService = new(documentServiceProvider);

DocumentConfig documentOpenXml = TestData.GetDocumentConfigOpenXml(outputLocation);
DocumentConfig documentPdfSharp = TestData.GetDocumentArgsConfigSharp(outputLocation);

try
{
    originationService.CreateFile(documentOpenXml, out string fullFilenameDocx);

    string json = JsonSerializer.Serialize(documentOpenXml, jsonSerializerOptions);

    Console.WriteLine(json);

    Process.Start(new ProcessStartInfo(fullFilenameDocx) { UseShellExecute = true });

    originationService.CreateFile(documentPdfSharp, out string fullFilenamePdfSharp);

    json = JsonSerializer.Serialize(documentPdfSharp, jsonSerializerOptions);

    Console.WriteLine(json);

    Process.Start(new ProcessStartInfo(fullFilenamePdfSharp) { UseShellExecute = true });
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}
