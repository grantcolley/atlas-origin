using Atlas.Core.Constants;
using Atlas.Core.Exceptions;
using Atlas.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Origin.Core.Models;
using Origin.Data.Access.Interfaces;

namespace Origin.Data.Access.Data
{
    public class DocumentPropertiesData(ApplicationDbContext applicationDbContext, ILogger<DocumentPropertiesData> logger)
        : AuthorisationData<DocumentPropertiesData>(applicationDbContext, logger), IDocumentPropertiesData
    {
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
