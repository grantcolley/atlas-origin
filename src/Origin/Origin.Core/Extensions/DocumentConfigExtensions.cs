using Origin.Core.Models;
using System.Linq;
using System.Text;

namespace Origin.Core.Extensions
{
    public static class DocumentConfigExtensions
    {
        /// <summary>
        /// Apply substitutes to the FilenameTemplate.
        /// </summary>
        /// <param name="documentConfig"></param>
        /// <returns>The full file name consisting of the <see cref="OutputLocation"/> and substituted file name.</returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public static string FullFilename(this DocumentConfig documentConfig)
        {
            if (string.IsNullOrWhiteSpace(documentConfig.FilenameTemplate)) throw new NullReferenceException(nameof(documentConfig.FilenameTemplate));
            if (string.IsNullOrWhiteSpace(documentConfig.OutputLocation)) throw new NullReferenceException(nameof(documentConfig.OutputLocation));

            Dictionary<string, DocumentSubstitute> substitutes = [];

            foreach (DocumentSubstitute documentSubstitute in documentConfig.Substitutes)
            {
                substitutes.Add(documentSubstitute.Key ?? throw new NullReferenceException(nameof(documentSubstitute.Key)), documentSubstitute);
            }

            string fileName = documentConfig.FilenameTemplate.ApplySubstitutesToContent(substitutes, documentConfig.SubstituteStart, documentConfig.SubstituteEnd)
                ?? throw new NullReferenceException(nameof(documentConfig.FilenameTemplate));

            return Path.Combine(documentConfig.OutputLocation, fileName);
        }

        /// <summary>
        /// Collapses substitute groups, cascades properties from <see cref="DocumentConfig"/> down to <see cref="DocumentContent"/>,
        /// and links <see cref="DocumentContent"/> to their <see cref="DocumentParagraph"/>'s and <see cref="DocumentTableCell"/>'s
        /// by <see cref="DocumentContent.RenderCellCode"/>.
        /// </summary>
        /// <param name="documentConfig">The <see cref="DocumentConfig"/>.</param>
        /// <exception cref="NullReferenceException"></exception>
        public static void ConstructDocumentConfig(this DocumentConfig documentConfig)
        {
            documentConfig.CollapseSubstituteGroups();

            List<DocumentParagraph> documentParagraphs = [.. documentConfig.ConfigParagraphs.OrderBy(cp => cp.Order).Select(cp => cp.DocumentParagraph)];

            foreach (DocumentParagraph documentParagraph in documentParagraphs)
            {
                documentParagraph.InheritParagraphProperties(documentConfig);

                foreach(DocumentContent documentContent in documentParagraph.Contents)
                {
                    documentContent.InheritContentProperties(documentParagraph);
                }

                if (documentParagraph.DocumentParagraphType == DocumentParagraphType.Table)
                {
                    foreach (DocumentTableCell documentTableCell in documentParagraph.Cells)
                    {
                        if (string.IsNullOrWhiteSpace(documentTableCell.Code)) throw new NullReferenceException(nameof(documentTableCell.Code));

                        documentTableCell.Contents 
                            = [.. documentParagraph.Contents.Where(c => c.RenderCellCode == documentTableCell.Code).OrderBy(c => c.Order)];
                    }
                }
                else
                {
                    documentParagraph.Contents = [.. documentParagraph.Contents.OrderBy(c => c.Order)];
                }
            }
        }

        public static void ApplySubstitutesToDocumentContent(this DocumentConfig documentConfig)
        {
            Dictionary<string, DocumentSubstitute> substitutes = [];

            foreach (DocumentSubstitute documentSubstitute in documentConfig.Substitutes)
            {
                substitutes.Add(documentSubstitute.Key ?? throw new NullReferenceException(nameof(documentSubstitute.Key)), documentSubstitute);
            }

            foreach (DocumentContent documentContent in documentConfig.ConfigParagraphs.Select(cp => cp.DocumentParagraph).SelectMany(p => p.Contents))
            {
                documentContent.Content = documentContent.Content.ApplySubstitutesToContent(substitutes, documentContent.SubstituteStart, documentContent.SubstituteEnd);
            }
        }

        /// <summary>
        /// Identifies target text within content to be substituted with another value.
        /// 
        /// Target text, delimited by substituteStart and substituteEnd, is mapped 
        /// to a substitute key, which provides the substitue value.
        /// 
        /// If content is null, empty or whitespace, return it.
        /// If substitutes is null or empty, return the content.
        /// If substituteStart or substituteEnd is null, return the content.
        /// 
        /// An exception is thrown if:
        /// - either substituteStart or substituteEnd is populated, while the other is null, throws InvalidDataException
        /// - a substituteStart has no corresponding substituteEnd, throws InvalidDataException
        /// - a substituteEnd has no corresponding substituteStart, throws InvalidDataException 
        /// - if target text doesn't map to a key in the substitutes dictionary, throws KeyNotFoundException 
        /// </summary>
        /// <param name="substitutes">The dictionary of substitutes.</param>
        /// <param name="content">The content to substitute.</param>
        /// <param name="substituteStart">The substitute start character.</param>
        /// <param name="substituteEnd">The substitute end character.</param>
        /// <returns>Content with substitutes applied.</returns>
        /// <exception cref="InvalidDataException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public static string? ApplySubstitutesToContent(this string? content, Dictionary<string, DocumentSubstitute>? substitutes, string? substituteStart, string? substituteEnd)
        {
            if (string.IsNullOrWhiteSpace(content)) return content;

            if (string.IsNullOrWhiteSpace(substituteStart) && substituteEnd != null) throw new InvalidDataException($"{nameof(substituteEnd)} {substituteEnd} missing corresponding {nameof(substituteStart)}");
            if (substituteStart != null && string.IsNullOrWhiteSpace(substituteEnd)) throw new InvalidDataException($"{nameof(substituteStart)} {substituteStart} missing corresponding {nameof(substituteEnd)}");

            if (string.IsNullOrWhiteSpace(substituteStart) && string.IsNullOrWhiteSpace(substituteEnd)) return content;

            if (substitutes == null || substitutes.Count == 0) return content;

            StringBuilder newContent = new();

            char? subStart = substituteStart?.ToCharArray()[0];
            char? subEnd = substituteEnd?.ToCharArray()[0];
            int subStartPosition = 0;
            int subEndPosition = 0;

            bool openSubStart = false;

            for (int i = 0; i < content.Length; i++)
            {
                if (openSubStart == false)
                {
                    if (content[i] == subStart)
                    {
                        // Mark the subStart position.

                        subStartPosition = i;
                        openSubStart = true;

                        if (subStartPosition > subEndPosition)
                        {
                            // Append content preceding the substituteStart.

                            int contextStartPosition = subEndPosition == 0 ? 0 : subEndPosition + 1;

                            newContent.Append(content.AsSpan(contextStartPosition, subStartPosition - contextStartPosition));
                        }
                    }
                    else if (content[i] == subEnd)
                    {
                        if (openSubStart == false) throw new InvalidDataException($"{nameof(substituteEnd)} missing corresponding {nameof(substituteStart)}");
                    }
                    else if (i == content.Length - 1)
                    {
                        if (subEndPosition > 0
                            && subEndPosition + 1 <= content.Length)
                        {
                            // End of content, if there have been substitutions, append any remaining content after the last one.

                            newContent.Append(content.AsSpan(subEndPosition + 1));
                        }
                    }
                }
                else
                {
                    if (content[i] == subStart)
                    {
                        if (openSubStart == true) throw new InvalidDataException($"{nameof(substituteStart)} missing corresponding {nameof(substituteEnd)}");
                    }
                    else if (content[i] == subEnd)
                    {
                        if (openSubStart == false) throw new InvalidDataException($"{nameof(substituteEnd)} missing corresponding {nameof(substituteStart)}");

                        subEndPosition = i;

                        string subKey = content[(subStartPosition + 1)..subEndPosition];

                        if (!substitutes.TryGetValue(subKey, out DocumentSubstitute? value)) throw new KeyNotFoundException(subKey);

                        newContent.Append(value.Value ?? string.Empty);

                        openSubStart = false;
                    }
                }
            }

            if (openSubStart == true) throw new InvalidDataException($"{nameof(substituteStart)} missing corresponding {nameof(substituteEnd)}");

            if (subStartPosition == 0 && subEndPosition == 0) return content;

            return newContent.ToString();
        }

        /// <summary>
        /// Grouped substitutes are sorted alphabetically and null, empty and whitespace content is collapsed.
        /// </summary>
        /// <param name="documentConfig">The <see cref="DocumentConfig"/>.</param>
        public static void CollapseSubstituteGroups(this DocumentConfig documentConfig)
        {
            var collapseGroups = from s in documentConfig.Substitutes
                          group s by s.Group into sg
                          select new { Group = sg.Key, DocumentSubstitute = sg.ToList() };

            foreach (var collapseGroup in collapseGroups) 
            {
                if(collapseGroup.Group == null)
                {
                    continue;
                }

                DocumentSubstitute[] orderedSubstitutes = [.. collapseGroup.DocumentSubstitute
                    .OrderBy(s => s.Key)];

                DocumentSubstitute[] nonNullOrEmptySubstitutes = [.. collapseGroup.DocumentSubstitute
                    .Where(s => !string.IsNullOrWhiteSpace(s.Value))
                    .OrderBy(s => s.Key)];

                for(int i = 0; i < orderedSubstitutes.Length; i++) 
                {
                    if(i < nonNullOrEmptySubstitutes.Length)
                    {
                        orderedSubstitutes[i].Value = nonNullOrEmptySubstitutes[i].Value;
                    }
                    else
                    {
                        orderedSubstitutes[i].Value = null;
                    }
                }
            }
        }

        public static DocumentParagraph? GetFooterParagraph(this DocumentConfig documentConfig)
        {
            return documentConfig.ConfigParagraphs.Select(cp => cp.DocumentParagraph).Single(p => p.DocumentParagraphType == DocumentParagraphType.Footer);
        }
    }
}
