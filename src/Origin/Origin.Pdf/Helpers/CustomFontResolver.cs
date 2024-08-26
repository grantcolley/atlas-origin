using PdfSharp.Fonts;

namespace Origin.Pdf.Helpers
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
            using Stream? stream = typeof(CustomFontResolver).Assembly.GetManifestResourceStream($"Origin.Pdf.Fonts.{name}.ttf") ?? throw new ArgumentException("No resource with name " + name);
            int num = (int)stream.Length;
            byte[] array = new byte[num];
            stream.Read(array, 0, num);
            return array;
        }
    }
}
