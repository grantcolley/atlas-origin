using System.Reflection;

namespace Origin.Resources
{
    public static class ResourceManager
    {
        public static IList<string?> GetImageResources()
        {
            IList<string?> resources = [];

            string[] names = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            foreach (string name in names)
            {
                if (name.Contains(".Images."))
                {
                    int position = name.IndexOf(".Images.");
                    position += 8;
                    resources.Add(name[position..]);
                }
            }

            return [.. resources.Order()];
        }

        public static IList<string?> GetFontResources()
        {
            IList<string?> resources = [];

            string[] names = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            foreach (string name in names)
            {
                if (name.Contains(".Fonts."))
                {
                    int position = name.IndexOf(".Fonts.");
                    position += 7;
                    resources.Add(name.Substring(position, name.Length - position - 4));
                }
            }

            return [.. resources.Order()];
        }

        public static Stream GetPngAsStream(string imageName)
        {
            var info = Assembly.GetExecutingAssembly().GetName();
            var name = info.Name;
            return Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream($"{name}.Images.{imageName}") ?? throw new NullReferenceException($"Unable to locate resource {imageName}");
        }

        public static string GetPngAsBase64(string imageName)
        {
            var info = Assembly.GetExecutingAssembly().GetName();
            var name = info.Name;
            using Stream stream = GetPngAsStream(imageName);

            MemoryStream memoryStream = new();
            stream.CopyTo(memoryStream);
            byte[] bytes = memoryStream.ToArray();
            memoryStream.Dispose();
            
            return Convert.ToBase64String(bytes);
        }

        public static byte[] GetFontAsByteArray(string fontName)
        {
            var info = Assembly.GetExecutingAssembly().GetName();
            var name = info.Name;
            Stream stream = Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream($"{name}.Fonts.{fontName}.ttf") ?? throw new NullReferenceException($"Unable to locate resource {fontName}");

            MemoryStream memoryStream = new();
            stream.CopyTo(memoryStream);
            byte[] bytes = memoryStream.ToArray();
            memoryStream.Dispose();

            return bytes;
        }

        public static string GetFontAsBase64(string fontName)
        {
            byte[] bytes = GetFontAsByteArray(fontName);

            return Convert.ToBase64String(bytes);
        }
    }
}
