using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Origin.Core.Converters;
using Origin.Core.Models;
using Origin.Resources;
using A = DocumentFormat.OpenXml.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;

namespace Origin.Generator.OpenXml.Extensions
{
    public static class ImageExtensions
    {
        public static void AddImage(this MainDocumentPart mainPart, DocumentContent? documentContent)
        {
            ArgumentNullException.ThrowIfNull(documentContent);

            if (documentContent.Image == null) throw new NullReferenceException(nameof(documentContent.Image));

            ImagePart imagePart = mainPart.AddImagePart(ImagePartType.Png);

            using Stream stream = ResourceManager.GetPngAsStream(documentContent.Image);

            imagePart.FeedData(stream);

            documentContent.Tag = mainPart.GetIdOfPart(imagePart);
        }

        public static void AddImageElement(this Run run, DocumentContent? image)
        {
            ArgumentNullException.ThrowIfNull(image);

            if (image.Name == null) throw new NullReferenceException(nameof(image.Name));
            if (image.Tag == null) throw new NullReferenceException(nameof(image.Tag));
            if (image.ImageWidth == null) throw new NullReferenceException(nameof(image.ImageWidth));
            if (image.ImageHeight == null) throw new NullReferenceException(nameof(image.ImageHeight));

            Drawing element =
                 new(
                     new DW.Inline(
                         new DW.Extent() { Cx = image.ImageWidth.Value.ToEMU(), Cy = image.ImageHeight.Value.ToEMU() },
                         new DW.EffectExtent()
                         {
                             LeftEdge = 0L,
                             TopEdge = 0L,
                             RightEdge = 5080,
                             BottomEdge = 0L
                         },
                         new DW.DocProperties()
                         {
                             Id = (UInt32Value)1U,
                             Name = image.Name
                         },
                         new DW.NonVisualGraphicFrameDrawingProperties(
                             new A.GraphicFrameLocks() { NoChangeAspect = true }),
                         new A.Graphic(
                             new A.GraphicData(
                                 new PIC.Picture(
                                     new PIC.NonVisualPictureProperties(
                                         new PIC.NonVisualDrawingProperties()
                                         {
                                             Id = (UInt32Value)0U,
                                             Name = image.Name
                                         },
                                         new PIC.NonVisualPictureDrawingProperties()),
                                     new PIC.BlipFill(
                                         new A.Blip(
                                             new A.BlipExtensionList(
                                                 new A.BlipExtension()
                                                 {
                                                     Uri =
                                                        "{28A0092B-C50C-407E-A947-70E740481C1C}"
                                                 })
                                         )
                                         {
                                             Embed = image.Tag,
                                             CompressionState =
                                             A.BlipCompressionValues.Print
                                         },
                                         new A.Stretch(
                                             new A.FillRectangle())),
                                     new PIC.ShapeProperties(
                                         new A.Transform2D(
                                             new A.Offset() { X = 0L, Y = 0L },
                                             new A.Extents() { Cx = image.ImageWidth.Value.ToEMU(), Cy = image.ImageHeight.Value.ToEMU() }),
                                         new A.PresetGeometry(
                                             new A.AdjustValueList()
                                         )
                                         { Preset = A.ShapeTypeValues.Rectangle }))
                             )
                             { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" })
                     )
                     {
                         DistanceFromTop = (UInt32Value)0U,
                         DistanceFromBottom = (UInt32Value)0U,
                         DistanceFromLeft = (UInt32Value)0U,
                         DistanceFromRight = (UInt32Value)0U,
                         EditId = "50D07946"
                     });

            run.AppendChild(element);
        }
    }
}
