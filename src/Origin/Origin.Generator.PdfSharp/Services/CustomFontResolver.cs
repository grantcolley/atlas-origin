using PdfSharp.Fonts;

namespace Origin.Generator.PdfSharp.Services
{
    public class CustomFontResolver : IFontResolver, IFontResolverMarker
    {
        public FontResolverInfo? ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(familyName);

            FontResolverInfo? fontResolverInfo = PlatformFontResolver.ResolveTypeface(familyName, isBold, isItalic);

            if (fontResolverInfo != null)
            {
                return fontResolverInfo;
            }

            string faceName = familyName.ToLower() switch
            {
                "arial" => "arial",
                "comic sans ms" => "comic",
                "courier" => "cour",
                "times new roman" => "times",
                "tahoma" => "tahoma",
                _ => throw new NotImplementedException(familyName),
            };

            return new FontResolverInfo(faceName, isBold, isItalic);
        }

        public byte[]? GetFont(string faceName)
        {
            return LoadFontData(faceName);
        }

        private static byte[] LoadFontData(string name)
        {
            return Resources.ResourceManager.GetFontAsByteArray(name);
        }
    }
}
