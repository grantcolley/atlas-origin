using MigraDoc.DocumentObjectModel;
using Origin.Core.Models;

namespace Origin.Pdf.Extensions
{
    public static class DocumentContentAlignExtension
    {
        public static ParagraphAlignment ToJustification(this DocumentContentAlign documentContentAlign)
        {
            return documentContentAlign switch
            {
                DocumentContentAlign.Start => ParagraphAlignment.Left,
                DocumentContentAlign.Centre => ParagraphAlignment.Center,
                DocumentContentAlign.End => ParagraphAlignment.Right,
                DocumentContentAlign.Distribute => ParagraphAlignment.Justify,
                _ => ParagraphAlignment.Left,
            };
        }
    }
}
