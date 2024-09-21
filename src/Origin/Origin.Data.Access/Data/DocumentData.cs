using Atlas.Core.Constants;
using Atlas.Core.Exceptions;
using Atlas.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Origin.Core.Models;
using Origin.Data.Access.Interfaces;

namespace Origin.Data.Access.Data
{
    public class DocumentData(ApplicationDbContext applicationDbContext, ILogger<DocumentData> logger)
        : AuthorisationData<DocumentData>(applicationDbContext, logger), IDocumentData
    {
        public async Task<IEnumerable<DocumentConfig>> GetDocumentConfigsAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await _applicationDbContext.DocumentConfigs
                    .AsNoTracking()
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new AtlasException(ex.Message, ex);
            }
        }

        public async Task<DocumentConfig?> GetDocumentConfigAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                DocumentConfig documentConfig = await _applicationDbContext.DocumentConfigs
                    .AsNoTracking()
                    .FirstAsync(d => d.DocumentConfigId.Equals(id), cancellationToken)
                    .ConfigureAwait(false);

                if (Authorisation == null
                    || !Authorisation.HasPermission(Auth.DEVELOPER))
                {
                    documentConfig.IsReadOnly = true;
                }

                return documentConfig;
            }
            catch (Exception ex)
            {
                throw new AtlasException(ex.Message, ex, $"DocumentConfigId={id}");
            }
        }

        public async Task<DocumentConfig> CreateDocumentConfigAsync(DocumentConfig addDocumentConfig, CancellationToken cancellationToken)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(nameof(addDocumentConfig));

                if (addDocumentConfig.DocumentConfigId > 0) throw new AtlasException($"Can not create DocumentConfig because DocumentConfigId is {addDocumentConfig.DocumentConfigId} when zero was expected.");

                DocumentConfig documentConfig = new()
                {
                    Name = addDocumentConfig.Name,
                    PageMarginLeft = addDocumentConfig.PageMarginLeft,
                    PageMarginTop = addDocumentConfig.PageMarginTop,
                    PageMarginRight = addDocumentConfig.PageMarginRight,
                    PageMarginBottom = addDocumentConfig.PageMarginBottom,
                    IgnoreParapgraphSpacing = addDocumentConfig.IgnoreParapgraphSpacing,
                    ParagraphSpacingBetweenLinesBefore = addDocumentConfig.ParagraphSpacingBetweenLinesBefore,
                    ParagraphSpacingBetweenLinesAfter = addDocumentConfig.ParagraphSpacingBetweenLinesAfter,
                    Font = addDocumentConfig.Font,
                    FontSize = addDocumentConfig.FontSize,
                    Colour = addDocumentConfig.Colour,
                    SubstituteStart = addDocumentConfig.SubstituteStart,
                    SubstituteEnd = addDocumentConfig.SubstituteEnd,
                };

                await _applicationDbContext.DocumentConfigs
                    .AddAsync(documentConfig, cancellationToken)
                    .ConfigureAwait(false);

                await _applicationDbContext
                    .SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);

                if (addDocumentConfig.Substitutes.Count > 0)
                {
                    documentConfig.Substitutes.AddRange(addDocumentConfig.Substitutes);

                    await _applicationDbContext
                        .SaveChangesAsync(cancellationToken)
                        .ConfigureAwait(false);
                }

                // TODO: Add paragraphs, rows, columns, cells and content and substitutes.

                if (addDocumentConfig.Paragraphs.Count > 0)
                {
                    documentConfig.Paragraphs.AddRange(addDocumentConfig.Paragraphs);

                    await _applicationDbContext
                        .SaveChangesAsync(cancellationToken)
                        .ConfigureAwait(false);
                }

                if (Authorisation == null
                    || !Authorisation.HasPermission(Auth.DOCUMENT_WRITE))
                {
                    documentConfig.IsReadOnly = true;
                }

                return documentConfig;
            }
            catch (Exception ex)
            {
                throw new AtlasException(ex.Message, ex);
            }
        }

        public async Task<DocumentConfig> UpdateDocumentConfigAsync(DocumentConfig documentConfig, CancellationToken cancellationToken)
        {
            try
            {
                DocumentConfig existing = await _applicationDbContext.DocumentConfigs
                    .FirstOrDefaultAsync(d => d.DocumentConfigId.Equals(documentConfig.DocumentConfigId), cancellationToken)
                    .ConfigureAwait(false)
                    ?? throw new NullReferenceException(
                        $"{nameof(documentConfig)} DocumentConfigId {documentConfig.DocumentConfigId} not found.");

                _applicationDbContext
                    .Entry(existing)
                    .CurrentValues.SetValues(documentConfig);

                // TODO: add/remove paragraphs, rows, columns, cells and content...

                _applicationDbContext.DocumentConfigs.Update(existing);

                await _applicationDbContext
                    .SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);

                if (Authorisation == null
                    || !Authorisation.HasPermission(Auth.DOCUMENT_WRITE))
                {
                    documentConfig.IsReadOnly = true;
                }

                return documentConfig;
            }
            catch (Exception ex)
            {
                throw new AtlasException(ex.Message, ex, $"DocumentConfigId={documentConfig.DocumentConfigId}");
            }
        }

        public async Task<int> DeleteDocumentConfigAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                DocumentConfig documentConfig = await _applicationDbContext.DocumentConfigs
                    .FirstAsync(d =>d.DocumentConfigId.Equals(id), cancellationToken)
                    .ConfigureAwait(false);

                _applicationDbContext.DocumentConfigs.Remove(documentConfig);

                return await _applicationDbContext
                    .SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new AtlasException(ex.Message, ex, $"DocumentConfigId={id}");
            }
        }

        public async Task<IEnumerable<DocumentParagraph>> GetDocumentParagraphsAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await _applicationDbContext.DocumentParagraphs
                    .AsNoTracking()
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new AtlasException(ex.Message, ex);
            }
        }

        public async Task<DocumentParagraph?> GetDocumentParagraphAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                DocumentParagraph documentParagraph = await _applicationDbContext.DocumentParagraphs
                    .AsNoTracking()
                    .FirstAsync(d => d.DocumentParagraphId.Equals(id), cancellationToken)
                    .ConfigureAwait(false);

                if (Authorisation == null
                    || !Authorisation.HasPermission(Auth.DEVELOPER))
                {
                    documentParagraph.IsReadOnly = true;
                }

                return documentParagraph;
            }
            catch (Exception ex)
            {
                throw new AtlasException(ex.Message, ex, $"DocumentParagraphId={id}");
            }
        }

        public async Task<DocumentParagraph> CreateDocumentParagraphAsync(DocumentParagraph addDocumentParagraph, CancellationToken cancellationToken)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(nameof(addDocumentParagraph));

                if (addDocumentParagraph.DocumentParagraphId > 0) throw new AtlasException($"Can not create DocumentParagraph because DocumentParagraphId is {addDocumentParagraph.DocumentParagraphId} when zero was expected.");

                DocumentParagraph documentParagraph = new()
                {
                    Name = addDocumentParagraph.Name,
                    Code = addDocumentParagraph.Code,
                    Order = addDocumentParagraph.Order,
                    DocumentParagraphType = addDocumentParagraph.DocumentParagraphType,
                    AlignContent = addDocumentParagraph.AlignContent,
                    IgnoreParapgraphSpacing = addDocumentParagraph.IgnoreParapgraphSpacing,
                    ParagraphSpacingBetweenLinesBefore = addDocumentParagraph.ParagraphSpacingBetweenLinesBefore,
                    ParagraphSpacingBetweenLinesAfter = addDocumentParagraph.ParagraphSpacingBetweenLinesAfter,
                    Font = addDocumentParagraph.Font,
                    FontSize = addDocumentParagraph.FontSize,
                    Colour = addDocumentParagraph.Colour,
                    SubstituteStart = addDocumentParagraph.SubstituteStart,
                    SubstituteEnd = addDocumentParagraph.SubstituteEnd,
                };

                await _applicationDbContext.DocumentParagraphs
                    .AddAsync(documentParagraph, cancellationToken)
                    .ConfigureAwait(false);

                await _applicationDbContext
                    .SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);

                if (addDocumentParagraph.Contents.Count > 0)
                {
                    documentParagraph.Contents.AddRange(addDocumentParagraph.Contents);

                    await _applicationDbContext
                        .SaveChangesAsync(cancellationToken)
                        .ConfigureAwait(false);
                }

                // TODO: Add rows, columns, cells and content.

                if (Authorisation == null
                    || !Authorisation.HasPermission(Auth.DOCUMENT_WRITE))
                {
                    documentParagraph.IsReadOnly = true;
                }

                return documentParagraph;
            }
            catch (Exception ex)
            {
                throw new AtlasException(ex.Message, ex);
            }
        }

        public async Task<DocumentParagraph> UpdateDocumentParagraphAsync(DocumentParagraph documentParagraph, CancellationToken cancellationToken)
        {
            try
            {
                DocumentParagraph existing = await _applicationDbContext.DocumentParagraphs
                    .FirstOrDefaultAsync(p => p.DocumentParagraphId.Equals(documentParagraph.DocumentParagraphId), cancellationToken)
                    .ConfigureAwait(false)
                    ?? throw new NullReferenceException(
                        $"{nameof(documentParagraph)} DocumentParagraphId {documentParagraph.DocumentParagraphId} not found.");

                _applicationDbContext
                    .Entry(existing)
                    .CurrentValues.SetValues(documentParagraph);

                // TODO: add/remove rows, columns, cells and content...

                _applicationDbContext.DocumentParagraphs.Update(existing);

                await _applicationDbContext
                    .SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);

                if (Authorisation == null
                    || !Authorisation.HasPermission(Auth.DOCUMENT_WRITE))
                {
                    documentParagraph.IsReadOnly = true;
                }

                return documentParagraph;
            }
            catch (Exception ex)
            {
                throw new AtlasException(ex.Message, ex, $"DocumentParagraphId={documentParagraph.DocumentParagraphId}");
            }
        }

        public async Task<int> DeleteDocumentParagraphAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                DocumentParagraph documentParagraph = await _applicationDbContext.DocumentParagraphs
                    .FirstAsync(p => p.DocumentParagraphId.Equals(id), cancellationToken)
                    .ConfigureAwait(false);

                _applicationDbContext.DocumentParagraphs.Remove(documentParagraph);

                return await _applicationDbContext
                    .SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new AtlasException(ex.Message, ex, $"DocumentParagraphId={id}");
            }
        }

        public async Task<IEnumerable<DocumentFont>> GetDocumentFontsAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await _applicationDbContext.DocumentFonts
                    .AsNoTracking()
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new AtlasException(ex.Message, ex);
            }
        }

        public async Task<DocumentFont?> GetDocumentFontAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                DocumentFont documentFont = await _applicationDbContext.DocumentFonts
                    .AsNoTracking()
                    .FirstAsync(f => f.DocumentFontId.Equals(id), cancellationToken)
                    .ConfigureAwait(false);

                if (Authorisation == null
                    || !Authorisation.HasPermission(Auth.DEVELOPER))
                {
                    documentFont.IsReadOnly = true;
                }

                return documentFont;
            }
            catch (Exception ex)
            {
                throw new AtlasException(ex.Message, ex, $"DocumentFontId={id}");
            }
        }

        public async Task<DocumentFont> CreateDocumentFontAsync(DocumentFont addDocumentFont, CancellationToken cancellationToken)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(nameof(addDocumentFont));

                if (addDocumentFont.DocumentFontId > 0) throw new AtlasException($"Can not create DocumentFont because DocumentFontId is {addDocumentFont.DocumentFontId} when zero was expected.");

                DocumentFont documentFont = new()
                {
                    Font = addDocumentFont.Font
                };

                await _applicationDbContext.DocumentFonts
                    .AddAsync(documentFont, cancellationToken)
                    .ConfigureAwait(false);

                await _applicationDbContext
                    .SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);

                if (Authorisation == null
                    || !Authorisation.HasPermission(Auth.DOCUMENT_WRITE))
                {
                    documentFont.IsReadOnly = true;
                }

                return documentFont;
            }
            catch (Exception ex)
            {
                throw new AtlasException(ex.Message, ex);
            }
        }

        public async Task<DocumentFont> UpdateDocumentFontAsync(DocumentFont documentFont, CancellationToken cancellationToken)
        {
            try
            {
                DocumentFont existing = await _applicationDbContext.DocumentFonts
                    .FirstOrDefaultAsync(f => f.DocumentFontId.Equals(documentFont.DocumentFontId), cancellationToken)
                    .ConfigureAwait(false)
                    ?? throw new NullReferenceException(
                        $"{nameof(documentFont)} DocumentFontId {documentFont.DocumentFontId} not found.");

                _applicationDbContext
                    .Entry(existing)
                    .CurrentValues.SetValues(documentFont);

                await _applicationDbContext
                    .SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);

                if (Authorisation == null
                    || !Authorisation.HasPermission(Auth.DOCUMENT_WRITE))
                {
                    documentFont.IsReadOnly = true;
                }

                return documentFont;
            }
            catch (Exception ex)
            {
                throw new AtlasException(ex.Message, ex, $"DocumentFontId={documentFont.DocumentFontId}");
            }
        }

        public async Task<int> DeleteDocumentFontAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                DocumentFont documentFont = await _applicationDbContext.DocumentFonts
                    .FirstAsync(f => f.DocumentFontId.Equals(id), cancellationToken)
                    .ConfigureAwait(false);

                _applicationDbContext.DocumentFonts.Remove(documentFont);

                return await _applicationDbContext
                    .SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new AtlasException(ex.Message, ex, $"DocumentFontId={id}");
            }
        }

        public async Task<IEnumerable<DocumentColour>> GetDocumentColoursAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await _applicationDbContext.DocumentColours
                    .AsNoTracking()
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new AtlasException(ex.Message, ex);
            }
        }

        public async Task<DocumentColour?> GetDocumentColourAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                DocumentColour documentColour = await _applicationDbContext.DocumentColours
                    .AsNoTracking()
                    .FirstAsync(c => c.DocumentColourId.Equals(id), cancellationToken)
                    .ConfigureAwait(false);

                if (Authorisation == null
                    || !Authorisation.HasPermission(Auth.DEVELOPER))
                {
                    documentColour.IsReadOnly = true;
                }

                return documentColour;
            }
            catch (Exception ex)
            {
                throw new AtlasException(ex.Message, ex, $"DocumentColourId={id}");
            }
        }

        public async Task<DocumentColour> CreateDocumentColourAsync(DocumentColour addDocumentColour, CancellationToken cancellationToken)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(nameof(addDocumentColour));

                if (addDocumentColour.DocumentColourId > 0) throw new AtlasException($"Can not create DocumentColour because DocumentColourId is {addDocumentColour.DocumentColourId} when zero was expected.");

                DocumentColour documentColour = new()
                {
                    Colour = addDocumentColour.Colour,
                    Rgb = addDocumentColour.Rgb
                };

                await _applicationDbContext.DocumentColours
                    .AddAsync(documentColour, cancellationToken)
                    .ConfigureAwait(false);

                await _applicationDbContext
                    .SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);

                if (Authorisation == null
                    || !Authorisation.HasPermission(Auth.DOCUMENT_WRITE))
                {
                    documentColour.IsReadOnly = true;
                }

                return documentColour;
            }
            catch (Exception ex)
            {
                throw new AtlasException(ex.Message, ex);
            }
        }

        public async Task<DocumentColour> UpdateDocumentColourAsync(DocumentColour documentColour, CancellationToken cancellationToken)
        {
            try
            {
                DocumentColour existing = await _applicationDbContext.DocumentColours
                    .FirstOrDefaultAsync(c => c.DocumentColourId.Equals(documentColour.DocumentColourId), cancellationToken)
                    .ConfigureAwait(false)
                    ?? throw new NullReferenceException(
                        $"{nameof(documentColour)} DocumentColourId {documentColour.DocumentColourId} not found.");

                _applicationDbContext
                    .Entry(existing)
                    .CurrentValues.SetValues(documentColour);

                await _applicationDbContext
                    .SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);

                if (Authorisation == null
                    || !Authorisation.HasPermission(Auth.DOCUMENT_WRITE))
                {
                    documentColour.IsReadOnly = true;
                }

                return documentColour;
            }
            catch (Exception ex)
            {
                throw new AtlasException(ex.Message, ex, $"DocumentColourId={documentColour.DocumentColourId}");
            }
        }

        public async Task<int> DeleteDocumentColourAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                DocumentColour documentColour = await _applicationDbContext.DocumentColours
                    .FirstAsync(c => c.DocumentColourId.Equals(id), cancellationToken)
                    .ConfigureAwait(false);

                _applicationDbContext.DocumentColours.Remove(documentColour);

                return await _applicationDbContext
                    .SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new AtlasException(ex.Message, ex, $"DocumentColourId={id}");
            }
        }
    }
}
