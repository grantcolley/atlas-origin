using Origin.Core.Model;
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
        /// First collapses substitute groups, then applies substitutes to <see cref="DocumentContent.Content"/>, 
        /// followed by linking <see cref="DocumentContent"/>'s and <see cref="DocumentTable"/>'s to their 
        /// <see cref="DocumentParagraph"/> by <see cref="DocumentContent.RenderElementCode"/>. Finally, cascades
        /// <see cref="DocumentParagraph"/>'s <see cref="DocumentContentProperties"/> to it's content.
        /// </summary>
        /// <param name="documentConfig">The <see cref="DocumentConfig"/>.</param>
        /// <returns>A list of <see cref="DocumentParagraph"/>'s</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static List<DocumentParagraph> BuildDocument(this DocumentConfig documentConfig)
        {
            documentConfig.CollapseSubstituteGroups();

            documentConfig.ApplySubstitutesToDocumentContent();

            List<DocumentParagraph> documentParagraphs = [.. documentConfig.Paragraphs.OrderBy(p => p.Order)];

            foreach (DocumentParagraph documentParagraph in documentParagraphs)
            {
                if (string.IsNullOrWhiteSpace(documentParagraph.Code)) throw new NullReferenceException(nameof(documentParagraph.Code));

                documentParagraph.Contents
                    = [.. documentConfig.Contents
                    .Where(c => c.RenderElementCode == documentParagraph.Code)
                    .OrderBy(c => c.Order)];

                if (documentParagraph.DocumentParagraphType == DocumentParagraphType.Table)
                {
                    documentParagraph.Table
                        = documentConfig.Tables.First(dt => dt.RenderElementCode == documentParagraph.Code);
                }
            }

            foreach (DocumentTableCell documentTableCell in documentConfig.Tables.SelectMany(t => t.Cells))
            {
                if (string.IsNullOrWhiteSpace(documentTableCell.Code)) throw new NullReferenceException(nameof(documentTableCell.Code));

                IEnumerable<DocumentContent> cellContents
                    = documentConfig.Contents
                    .Where(c => c.RenderElementCode == documentTableCell.Code)
                    .OrderBy(c => c.Order);

                documentTableCell.Contents.AddRange(cellContents);
            }

            documentConfig.CascadeParagraphPropeties();

            return documentParagraphs;
        }

        public static void ApplySubstitutesToDocumentContent(this DocumentConfig documentConfig)
        {
            Dictionary<string, DocumentSubstitute> substitutes = [];

            foreach (DocumentSubstitute documentSubstitute in documentConfig.Substitutes)
            {
                substitutes.Add(documentSubstitute.Key ?? throw new NullReferenceException(nameof(documentSubstitute.Key)), documentSubstitute);
            }

            foreach (DocumentContent documentContent in documentConfig.Contents)
            {
                documentContent.Content = documentContent.Content.ApplySubstitutesToContent(substitutes, documentConfig.SubstituteStart, documentConfig.SubstituteEnd);
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
        public static string? ApplySubstitutesToContent(this string? content, Dictionary<string, DocumentSubstitute>? substitutes, char? substituteStart, char? substituteEnd)
        {
            if (string.IsNullOrWhiteSpace(content)) return content;

            if (substituteStart == null && substituteEnd != null) throw new InvalidDataException($"{nameof(substituteEnd)} {substituteEnd} missing corresponding {nameof(substituteStart)}");
            if (substituteStart != null && substituteEnd == null) throw new InvalidDataException($"{nameof(substituteStart)} {substituteStart} missing corresponding {nameof(substituteEnd)}");

            if (substituteStart == null && substituteEnd == null) return content;

            if (substitutes == null || substitutes.Count == 0) return content;

            StringBuilder newContent = new();

            int subStartPosition = 0;
            int subEndPosition = 0;

            bool openSubStart = false;

            for (int i = 0; i < content.Length; i++)
            {
                if (openSubStart == false)
                {
                    if (content[i] == substituteStart)
                    {
                        // Mark the substituteStart position.

                        subStartPosition = i;
                        openSubStart = true;

                        if (subStartPosition > subEndPosition)
                        {
                            // Append content preceding the substituteStart.

                            int contextStartPosition = subEndPosition == 0 ? 0 : subEndPosition + 1;

                            newContent.Append(content.AsSpan(contextStartPosition, subStartPosition - contextStartPosition));
                        }
                    }
                    else if (content[i] == substituteEnd)
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
                    if (content[i] == substituteStart)
                    {
                        if (openSubStart == true) throw new InvalidDataException($"{nameof(substituteStart)} missing corresponding {nameof(substituteEnd)}");
                    }
                    else if (content[i] == substituteEnd)
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

        /// <summary>
        /// Cascades <see cref="DocumentContentProperties"/> from the <see cref="DocumentParagraph"/> down its content.
        /// If a <see cref="DocumentContent"/> has its own properties set, then it will not inherit the properties 
        /// from its parent <see cref="DocumentParagraph"/>. 
        /// </summary>
        /// <param name="documentConfig"></param>
        public static void CascadeParagraphPropeties(this DocumentConfig documentConfig)
        {
            foreach(DocumentParagraph documentParagraph in documentConfig.Paragraphs) 
            {
                documentParagraph.InheritProperties(documentConfig);

                foreach(DocumentContent documentContent in documentParagraph.Contents)
                {
                    documentContent.InheritProperties(documentParagraph);
                }

                if(documentParagraph.DocumentParagraphType == DocumentParagraphType.Table)
                {
                    if(documentParagraph.Table == null) throw new NullReferenceException(nameof(documentParagraph.Table));

                    foreach(DocumentTableCell documentTableCell in documentParagraph.Table.Cells)
                    {
                        documentTableCell.InheritProperties(documentParagraph);

                        foreach(DocumentContent documentContent in documentTableCell.Contents)
                        {
                            documentContent.InheritProperties(documentParagraph);
                        }
                    }
                }
            }
        }

        public static List<DocumentContent> GetImages(this DocumentConfig documentConfig)
        {
            return documentConfig.Contents.Where(c => c.ContentType == DocumentContentType.Image).ToList();
        }

        public static DocumentParagraph? GetFooterParagraph(this DocumentConfig documentConfig)
        {
            return documentConfig.Paragraphs.Single(p => p.DocumentParagraphType == DocumentParagraphType.Footer);
        }
    }
}
