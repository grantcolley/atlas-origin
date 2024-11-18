using DocumentFormat.OpenXml.Wordprocessing;
using Origin.Core.Models;

namespace Origin.Generator.OpenXml.Extensions
{
    public static class DocumentContentAlignExtension
    {
        public static JustificationValues ToJustification(this DocumentContentAlign documentContentAlign)
        {
            return documentContentAlign switch
            {
                DocumentContentAlign.Start => JustificationValues.Start,
                DocumentContentAlign.Centre => JustificationValues.Center,
                DocumentContentAlign.End => JustificationValues.End,
                DocumentContentAlign.Distribute => JustificationValues.Distribute,
                _ => JustificationValues.Start,
            };
        }
    }
}
