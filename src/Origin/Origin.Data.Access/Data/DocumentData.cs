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
                    SubstituteStart = addDocumentConfig.SubstituteStart,
                    SubstituteEnd = addDocumentConfig.SubstituteEnd,
                    PageMarginLeft = addDocumentConfig.PageMarginLeft,
                    PageMarginTop = addDocumentConfig.PageMarginTop,
                    PageMarginRight = addDocumentConfig.PageMarginRight,
                    PageMarginBottom = addDocumentConfig.PageMarginBottom,
                    IgnoreParapgraphSpacing = addDocumentConfig.IgnoreParapgraphSpacing,
                    ParagraphSpacingBetweenLinesBefore = addDocumentConfig.ParagraphSpacingBetweenLinesBefore,
                    ParagraphSpacingBetweenLinesAfter = addDocumentConfig.ParagraphSpacingBetweenLinesAfter,
                    Font = addDocumentConfig.Font,
                    FontSize = addDocumentConfig.FontSize,
                    Colour = addDocumentConfig.Colour
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

                if (addDocumentConfig.Paragraphs.Count > 0)
                {
                    documentConfig.Paragraphs.AddRange(addDocumentConfig.Paragraphs);

                    await _applicationDbContext
                        .SaveChangesAsync(cancellationToken)
                        .ConfigureAwait(false);
                }

                // TODO: Add tables, rows, columns and cells.

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

        public Task<DocumentConfig> UpdateDocumentConfigAsync(DocumentConfig documentConfig, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteDocumentConfigAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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

        public Task<DocumentParagraph?> GetDocumentParagraphAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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
                    //ParagraphSpacingBetweenLinesBefore = addDocumentConfig.ParagraphSpacingBetweenLinesBefore,
                    //ParagraphSpacingBetweenLinesAfter = addDocumentConfig.ParagraphSpacingBetweenLinesAfter,
                    Font = addDocumentParagraph.Font,
                    FontSize = addDocumentParagraph.FontSize,
                    Colour = addDocumentParagraph.Colour
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

                // TODO: Add tables, rows, columns and cells.

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

        public Task<DocumentParagraph> UpdateDocumentParagraphAsync(DocumentParagraph documentParagraph, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteDocumentParagraphAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
