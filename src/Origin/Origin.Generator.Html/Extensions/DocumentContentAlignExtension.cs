using Origin.Core.Models;

namespace Origin.Generator.Html.Extensions
{
    public static class DocumentContentAlignExtension
    {
        public static string ToJustification(this DocumentContentAlign documentContentAlign)
        {
            return documentContentAlign switch
            {
                DocumentContentAlign.Start => "start",
                DocumentContentAlign.Centre => "center",
                DocumentContentAlign.End => "end",
                DocumentContentAlign.Distribute => "justify",
                _ => "start",
            };
        }
    }
}
