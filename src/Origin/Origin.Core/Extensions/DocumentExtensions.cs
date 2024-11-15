using Origin.Core.Models;

namespace Origin.Core.Extensions
{
    public static class DocumentExtensions
    {
        /// <summary>
        /// Apply substitutes to the FilenameTemplate.
        /// </summary>
        /// <param name="document"></param>
        /// <param name="fileExtension"></param>
        /// <returns>The full file name consisting of the <see cref="Target"/> and substituted file name.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        public static string Filename(this Document document, string fileExtension)
        {
            ArgumentNullException.ThrowIfNull(document, nameof(document));
            ArgumentNullException.ThrowIfNull(document.Config, nameof(document.Config));

            if (string.IsNullOrWhiteSpace(document.FilenameTemplate)) throw new NullReferenceException(nameof(document.FilenameTemplate));

            Dictionary<string, DocumentSubstitute> substitutes = [];

            foreach (DocumentSubstitute documentSubstitute in document.Config.Substitutes)
            {
                substitutes.Add(documentSubstitute.Key ?? throw new NullReferenceException(nameof(documentSubstitute.Key)), documentSubstitute);
            }

            string fileName = document.FilenameTemplate.ApplySubstitutesToContent(substitutes, document.Config.SubstituteStart, document.Config.SubstituteEnd)
                ?? throw new NullReferenceException(nameof(document.FilenameTemplate));

            return $"{fileName}.{fileExtension}";
        }
    }
}
