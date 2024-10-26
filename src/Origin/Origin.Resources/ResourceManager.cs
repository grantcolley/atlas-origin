using System.Reflection;

namespace Origin.Resources
{
    public static class ResourceManager
    {
        public static IList<string> GetImagesResources()
        {
            IList<string> imageResources = new List<string>();

            string[] names = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            foreach (string name in names)
            {
                if (name.Contains(".Images."))
                {
                    int position = name.IndexOf(".Images.");
                    position += 8;
                    imageResources.Add(name.Substring(position));
                }
            }

            return imageResources;
        }

        public static Stream GetImage(string imageName)
        {
            var info = Assembly.GetExecutingAssembly().GetName();
            var name = info.Name;
            return Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream($"{name}.Images.{imageName}")!;
        }

        public static string GetImageAsBase64(string imageName)
        {
            var info = Assembly.GetExecutingAssembly().GetName();
            var name = info.Name;
            using Stream stream = Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream($"{name}.Images.{imageName}")!;

            MemoryStream memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);

            return $"base64:{Convert.ToBase64String(memoryStream.ToArray())}";
        }
    }
}
