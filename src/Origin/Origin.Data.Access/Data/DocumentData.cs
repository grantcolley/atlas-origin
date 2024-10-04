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
                    .Include(c => c.Substitutes)
                    .Include(c => c.Paragraphs)
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
                    ParagraphSpacingBetweenLinesBefore = addDocumentConfig.ParagraphSpacingBetweenLinesBefore,
                    ParagraphSpacingBetweenLinesAfter = addDocumentConfig.ParagraphSpacingBetweenLinesAfter,
                    AlignContent = addDocumentConfig.AlignContent,
                    IgnoreParapgraphSpacing = addDocumentConfig.IgnoreParapgraphSpacing,
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
                    .Include(d => d.Contents)
                    .Include(d => d.Columns)
                    .Include(d => d.Rows)
                    .Include (d => d.Cells)
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
                    Order = addDocumentParagraph.Order,
                    DocumentParagraphType = addDocumentParagraph.DocumentParagraphType,
                    ParagraphSpacingBetweenLinesBefore = addDocumentParagraph.ParagraphSpacingBetweenLinesBefore,
                    ParagraphSpacingBetweenLinesAfter = addDocumentParagraph.ParagraphSpacingBetweenLinesAfter,
                    AlignContent = addDocumentParagraph.AlignContent,
                    IgnoreParapgraphSpacing = addDocumentParagraph.IgnoreParapgraphSpacing,
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

                if(documentParagraph.DocumentParagraphType == DocumentParagraphType.Table)
                {
                    if(addDocumentParagraph.Columns.Count > 0)
                    {
                        documentParagraph.Columns.AddRange(addDocumentParagraph.Columns);

                        await _applicationDbContext
                            .SaveChangesAsync(cancellationToken)
                            .ConfigureAwait(false);
                    }

                    if (addDocumentParagraph.Rows.Count > 0)
                    {
                        documentParagraph.Rows.AddRange(addDocumentParagraph.Rows);

                        await _applicationDbContext
                            .SaveChangesAsync(cancellationToken)
                            .ConfigureAwait(false);
                    }

                    if (addDocumentParagraph.Cells.Count > 0)
                    {
                        documentParagraph.Cells.AddRange(addDocumentParagraph.Cells);

                        await _applicationDbContext
                            .SaveChangesAsync(cancellationToken)
                            .ConfigureAwait(false);
                    }
                }

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

        public async Task<DocumentParagraph> UpdateDocumentParagraphAsync(DocumentParagraph updateDocumentParagraph, CancellationToken cancellationToken)
        {
            try
            {
                DocumentParagraph existingParagraph = await _applicationDbContext.DocumentParagraphs
                    .Include(c => c.Contents)
                    .FirstOrDefaultAsync(p => p.DocumentParagraphId.Equals(updateDocumentParagraph.DocumentParagraphId), cancellationToken)
                    .ConfigureAwait(false)
                    ?? throw new NullReferenceException(
                        $"{nameof(updateDocumentParagraph)} DocumentParagraphId {updateDocumentParagraph.DocumentParagraphId} not found.");

                _applicationDbContext
                    .Entry(existingParagraph)
                    .CurrentValues.SetValues(updateDocumentParagraph);

                await UpdateParagraphContents(existingParagraph, updateDocumentParagraph).ConfigureAwait(false);

                if (updateDocumentParagraph.DocumentParagraphType == DocumentParagraphType.Table)
                {
                    await UpdateParagraphTableRows(existingParagraph, updateDocumentParagraph).ConfigureAwait(false);

                    await UpdateParagraphTableColumns(existingParagraph, updateDocumentParagraph).ConfigureAwait(false);

                    await UpdateParagraphTableCells(existingParagraph, updateDocumentParagraph).ConfigureAwait(false);
                }
                else
                {
                    foreach(DocumentTableRow row in existingParagraph.Rows)
                    {
                        existingParagraph.Rows.Remove(row);
                    }

                    foreach (DocumentTableColumn column in existingParagraph.Columns)
                    {
                        existingParagraph.Columns.Remove(column);
                    }

                    foreach (DocumentTableCell cell in existingParagraph.Cells)
                    {
                        existingParagraph.Cells.Remove(cell);
                    }
                }

                _applicationDbContext.DocumentParagraphs.Update(existingParagraph);

                await _applicationDbContext
                    .SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);

                if (Authorisation == null
                    || !Authorisation.HasPermission(Auth.DOCUMENT_WRITE))
                {
                    updateDocumentParagraph.IsReadOnly = true;
                }

                return updateDocumentParagraph;
            }
            catch (Exception ex)
            {
                throw new AtlasException(ex.Message, ex, $"DocumentParagraphId={updateDocumentParagraph.DocumentParagraphId}");
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

        private async Task UpdateParagraphTableRows(DocumentParagraph existingParagraph, DocumentParagraph updateDocumentParagraph)
        {
            List<DocumentTableRow> tableRows = updateDocumentParagraph.Rows
                .Where(r => r.DocumentTableRowId > 0)
                .ToList();

            List<DocumentTableRow> removeRows = existingParagraph.Rows
                .Where(rr => !tableRows.Any(r => r.DocumentTableRowId.Equals(rr.DocumentTableRowId)))
                .ToList();

            foreach (DocumentTableRow row in removeRows)
            {
                existingParagraph.Rows.Remove(row);
            }

            if (tableRows.Count > 0)
            {
                List<int> rowIds = tableRows.Select(r => r.DocumentTableRowId).ToList();

                IEnumerable<DocumentTableRow> existingRows = await _applicationDbContext.DocumentTableRows
                    .Where(r => rowIds.Contains(r.DocumentTableRowId))
                    .ToListAsync();

                foreach (DocumentTableRow existingRow in existingRows)
                {
                    DocumentTableRow row = tableRows
                        .First(r => r.DocumentTableRowId == existingRow.DocumentTableRowId);

                    _applicationDbContext
                        .Entry(existingRow)
                        .CurrentValues.SetValues(row);

                    _applicationDbContext.DocumentTableRows.Update(existingRow);
                }
            }

            List<DocumentTableRow> newRows = updateDocumentParagraph.Rows
                .Where(r => r.DocumentTableRowId == 0)
                .ToList();

            foreach (DocumentTableRow row in newRows)
            {
                await _applicationDbContext.DocumentTableRows.AddAsync(row).ConfigureAwait(false);

                existingParagraph.Rows.Add(row);
            }
        }

        private async Task UpdateParagraphTableColumns(DocumentParagraph existingParagraph, DocumentParagraph updateDocumentParagraph)
        {
            List<DocumentTableColumn> tableColumns = updateDocumentParagraph.Columns
                .Where(c => c.DocumentTableColumnId > 0)
                .ToList();

            List<DocumentTableColumn> removeColumns = existingParagraph.Columns
                .Where(rc => !tableColumns.Any(c => c.DocumentTableColumnId.Equals(rc.DocumentTableColumnId)))
                .ToList();

            foreach (DocumentTableColumn column in removeColumns)
            {
                existingParagraph.Columns.Remove(column);
            }

            if (tableColumns.Count > 0)
            {
                List<int> columnIds = tableColumns.Select(c => c.DocumentTableColumnId).ToList();

                IEnumerable<DocumentTableColumn> existingColumns = await _applicationDbContext.DocumentTableColumns
                    .Where(c => columnIds.Contains(c.DocumentTableColumnId))
                    .ToListAsync();

                foreach (DocumentTableColumn existingColumn in existingColumns)
                {
                    DocumentTableColumn column = tableColumns
                        .First(c => c.DocumentTableColumnId == existingColumn.DocumentTableColumnId);

                    _applicationDbContext
                        .Entry(existingColumn)
                        .CurrentValues.SetValues(column);

                    _applicationDbContext.DocumentTableColumns.Update(existingColumn);
                }
            }

            List<DocumentTableColumn> newColumns = updateDocumentParagraph.Columns
                .Where(c => c.DocumentTableColumnId == 0)
                .ToList();

            foreach (DocumentTableColumn column in newColumns)
            {
                await _applicationDbContext.DocumentTableColumns.AddAsync(column).ConfigureAwait(false);

                existingParagraph.Columns.Add(column);
            }
        }

        private async Task UpdateParagraphTableCells(DocumentParagraph existingParagraph, DocumentParagraph updateDocumentParagraph)
        {
            List<DocumentTableCell> tableCells = updateDocumentParagraph.Cells
                .Where(c => c.DocumentTableCellId > 0)
                .ToList();

            List<DocumentTableCell> removeCells = existingParagraph.Cells
                .Where(rc => !tableCells.Any(c => c.DocumentTableCellId.Equals(rc.DocumentTableCellId)))
                .ToList();

            foreach (DocumentTableCell cell in removeCells)
            {
                existingParagraph.Cells.Remove(cell);
            }

            if (tableCells.Count > 0)
            {
                List<int> cellIds = tableCells.Select(c => c.DocumentTableCellId).ToList();

                IEnumerable<DocumentTableCell> existingCells = await _applicationDbContext.DocumentTableCells
                    .Where(c => cellIds.Contains(c.DocumentTableCellId))
                    .ToListAsync();

                foreach (DocumentTableCell existingCell in existingCells)
                {
                    DocumentTableCell cell = tableCells
                        .First(c => c.DocumentTableCellId == existingCell.DocumentTableCellId);

                    _applicationDbContext
                        .Entry(existingCell)
                        .CurrentValues.SetValues(cell);

                    _applicationDbContext.DocumentTableCells.Update(existingCell);
                }
            }

            List<DocumentTableCell> newCells = updateDocumentParagraph.Cells
                .Where(c => c.DocumentTableCellId == 0)
                .ToList();

            foreach (DocumentTableCell cell in newCells)
            {
                await _applicationDbContext.DocumentTableCells.AddAsync(cell).ConfigureAwait(false);

                existingParagraph.Cells.Add(cell);
            }
        }

        private async Task UpdateParagraphContents(DocumentParagraph existingParagraph, DocumentParagraph updateDocumentParagraph)
        {
            List<DocumentContent> paragraphContents = updateDocumentParagraph.Contents
                .Where(c => c.DocumentContentId > 0)
                .ToList();

            List<DocumentContent> removeContents = existingParagraph.Contents
                .Where(rc => !paragraphContents.Any(c => c.DocumentContentId.Equals(rc.DocumentContentId)))
                .ToList();

            foreach (DocumentContent content in removeContents)
            {
                existingParagraph.Contents.Remove(content);
            }

            if (paragraphContents.Count > 0)
            {
                List<int> paragraphContentsIds = paragraphContents.Select(c => c.DocumentContentId).ToList();

                IEnumerable<DocumentContent> existingContents = await _applicationDbContext.DocumentContents
                    .Where(c => paragraphContentsIds.Contains(c.DocumentContentId))
                    .ToListAsync();

                foreach (DocumentContent existingContent in existingContents)
                {
                    DocumentContent documentContent = paragraphContents
                        .First(c => c.DocumentContentId == existingContent.DocumentContentId);

                    _applicationDbContext
                        .Entry(existingContent)
                        .CurrentValues.SetValues(documentContent);

                    _applicationDbContext.DocumentContents.Update(existingContent);
                }
            }

            List<DocumentContent> newContents = updateDocumentParagraph.Contents
                .Where(c => c.DocumentContentId == 0)
                .ToList();

            foreach (DocumentContent content in newContents)
            {
                await _applicationDbContext.DocumentContents.AddAsync(content).ConfigureAwait(false);

                existingParagraph.Contents.Add(content);
            }
        }
    }
}
