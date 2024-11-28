using Atlas.Core.Constants;
using Atlas.Core.Exceptions;
using Atlas.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                DocumentConfig documentConfig = await _applicationDbContext.DocumentConfigs
                    .Include(c => c.Substitutes)
                    .Include(c => c.ConfigParagraphs)
                    .ThenInclude(cp => cp.DocumentParagraph)
                    .ThenInclude(p => p.Contents)
                    .Include(c => c.ConfigParagraphs)
                    .ThenInclude(cp => cp.DocumentParagraph)
                    .ThenInclude(p => p.Cells)
                    .Include(c => c.ConfigParagraphs)
                    .ThenInclude(cp => cp.DocumentParagraph)
                    .ThenInclude(p => p.Rows)
                    .Include(c => c.ConfigParagraphs)
                    .ThenInclude(cp => cp.DocumentParagraph)
                    .ThenInclude(p => p.Columns)
                    .AsNoTracking()
                    .FirstAsync(d => d.DocumentConfigId.Equals(id), cancellationToken)
                    .ConfigureAwait(false);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

                if (Authorisation == null
                    || !Authorisation.HasPermission(Auth.DOCUMENT_WRITE))
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
            using IDbContextTransaction transaction = _applicationDbContext.Database.BeginTransaction();

            try
            {
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

                if (addDocumentConfig.ConfigParagraphs.Count > 0)
                {
                    documentConfig.ConfigParagraphs.AddRange(addDocumentConfig.ConfigParagraphs);

                    await _applicationDbContext
                        .SaveChangesAsync(cancellationToken)
                        .ConfigureAwait(false);
                }

                if (Authorisation == null
                    || !Authorisation.HasPermission(Auth.DOCUMENT_WRITE))
                {
                    documentConfig.IsReadOnly = true;
                }

                await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);

                return documentConfig;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);

                throw new AtlasException(ex.Message, ex);
            }
        }

        public async Task<DocumentConfig> UpdateDocumentConfigAsync(DocumentConfig updateDocumentConfig, CancellationToken cancellationToken)
        {
            using IDbContextTransaction transaction = _applicationDbContext.Database.BeginTransaction();

            try
            {
                DocumentConfig existingDocumentConfig = await _applicationDbContext.DocumentConfigs
                    .Include(d => d.Substitutes)
                    .Include(d => d.ConfigParagraphs)
                    .FirstOrDefaultAsync(d => d.DocumentConfigId.Equals(updateDocumentConfig.DocumentConfigId), cancellationToken)
                    .ConfigureAwait(false)
                    ?? throw new NullReferenceException(
                        $"{nameof(updateDocumentConfig)} DocumentConfigId {updateDocumentConfig.DocumentConfigId} not found.");

                _applicationDbContext
                    .Entry(existingDocumentConfig)
                    .CurrentValues.SetValues(updateDocumentConfig);

                UpdateDocumentSubstitutes(existingDocumentConfig, updateDocumentConfig);

                UpdateDocumentConfigParagraphs(existingDocumentConfig, updateDocumentConfig);

                _applicationDbContext.DocumentConfigs.Update(existingDocumentConfig);

                await _applicationDbContext
                    .SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);

                if (Authorisation == null
                    || !Authorisation.HasPermission(Auth.DOCUMENT_WRITE))
                {
                    updateDocumentConfig.IsReadOnly = true;
                }

                await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);

                return updateDocumentConfig;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);

                throw new AtlasException(ex.Message, ex, $"DocumentConfigId={updateDocumentConfig.DocumentConfigId}");
            }
        }

        public async Task<int> DeleteDocumentConfigAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                DocumentConfig documentConfig = await _applicationDbContext.DocumentConfigs
                    .Include(d => d.Substitutes)
                    .FirstAsync(d =>d.DocumentConfigId.Equals(id), cancellationToken)
                    .ConfigureAwait(false);

                documentConfig.Substitutes.Clear();
                documentConfig.ConfigParagraphs.Clear();

                _applicationDbContext.DocumentConfigs.Remove(documentConfig);

                int result = await _applicationDbContext
                    .SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);

                return result;
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
                    .Include(p => p.Contents)
                    .Include(p => p.Columns)
                    .Include(p => p.Rows)
                    .Include(p => p.Cells)
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
                    .Include(p => p.Contents)
                    .Include(p => p.Columns)
                    .Include(p => p.Rows)
                    .Include(p => p.Cells)
                    .AsNoTracking()
                    .FirstAsync(p => p.DocumentParagraphId.Equals(id), cancellationToken)
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
                throw new AtlasException(ex.Message, ex, $"DocumentParagraphId={id}");
            }
        }

        public async Task<DocumentParagraph> CreateDocumentParagraphAsync(DocumentParagraph addDocumentParagraph, CancellationToken cancellationToken)
        {
            using IDbContextTransaction transaction = _applicationDbContext.Database.BeginTransaction();

            try
            {
                if (addDocumentParagraph.DocumentParagraphId > 0) throw new AtlasException($"Can not create DocumentParagraph because DocumentParagraphId is {addDocumentParagraph.DocumentParagraphId} when zero was expected.");

                DocumentParagraph documentParagraph = new()
                {
                    Name = addDocumentParagraph.Name,
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

                await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);

                return documentParagraph;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);

                throw new AtlasException(ex.Message, ex);
            }
        }

        public async Task<DocumentParagraph> UpdateDocumentParagraphAsync(DocumentParagraph updateDocumentParagraph, CancellationToken cancellationToken)
        {
            using IDbContextTransaction transaction = _applicationDbContext.Database.BeginTransaction();

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

                await UpdateParagraphContentsAsync(existingParagraph, updateDocumentParagraph).ConfigureAwait(false);

                if (updateDocumentParagraph.DocumentParagraphType == DocumentParagraphType.Table)
                {
                    await UpdateParagraphTableRowsAsync(existingParagraph, updateDocumentParagraph).ConfigureAwait(false);

                    await UpdateParagraphTableColumnsAsync(existingParagraph, updateDocumentParagraph).ConfigureAwait(false);

                    await UpdateParagraphTableCellsAsync(existingParagraph, updateDocumentParagraph).ConfigureAwait(false);
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

                await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);

                return updateDocumentParagraph;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);

                throw new AtlasException(ex.Message, ex, $"DocumentParagraphId={updateDocumentParagraph.DocumentParagraphId}");
            }
        }

        public async Task<int> DeleteDocumentParagraphAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                DocumentParagraph documentParagraph = await _applicationDbContext.DocumentParagraphs
                    .Include(p => p.Columns)
                    .Include(p => p.Rows)
                    .Include(p => p.Cells)
                    .Include(p => p.Contents)
                    .FirstAsync(p => p.DocumentParagraphId.Equals(id), cancellationToken)
                    .ConfigureAwait(false);

                documentParagraph.Columns.Clear();
                documentParagraph.Rows.Clear();
                documentParagraph.Cells.Clear();
                documentParagraph.Contents.Clear();

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

        private async Task UpdateParagraphTableRowsAsync(DocumentParagraph existingParagraph, DocumentParagraph updateDocumentParagraph)
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

        private async Task UpdateParagraphTableColumnsAsync(DocumentParagraph existingParagraph, DocumentParagraph updateDocumentParagraph)
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

        private async Task UpdateParagraphTableCellsAsync(DocumentParagraph existingParagraph, DocumentParagraph updateDocumentParagraph)
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

        private async Task UpdateParagraphContentsAsync(DocumentParagraph existingParagraph, DocumentParagraph updateDocumentParagraph)
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

        private void UpdateDocumentSubstitutes(DocumentConfig existingDocument, DocumentConfig updateDocument)
        {
            List<DocumentSubstitute> documentSubstitutes = updateDocument.Substitutes
                .Where(s => s.DocumentSubstituteId > 0)
                .ToList();

            // remove existing substitutes that are no longer linked to the existing document.
            List<DocumentSubstitute> removeSubstitutes = existingDocument.Substitutes
                .Where(rs => !documentSubstitutes.Any(s => s.DocumentSubstituteId.Equals(rs.DocumentSubstituteId)))
                .ToList();

            foreach (DocumentSubstitute substitute in removeSubstitutes)
            {
                existingDocument.Substitutes.Remove(substitute);
            }

            if (documentSubstitutes.Count > 0)
            {
                (DocumentSubstitute existing, DocumentSubstitute update) GetMatch(DocumentSubstitute existing, DocumentSubstitute update)
                {
                    return (existing, update);
                };

                // update existing substitutes that are linked to the existing document.
                var matches = from existing in existingDocument.Substitutes
                              join update in documentSubstitutes on existing.DocumentSubstituteId equals update.DocumentSubstituteId
                              select GetMatch(existing, update);

                foreach (var (existing, update) in matches)
                {
                    _applicationDbContext
                        .Entry(existing)
                        .CurrentValues.SetValues(update);

                    _applicationDbContext.DocumentSubstitutes.Update(existing);
                }
            }

            // new substitutes to be added to the existing document.
            List<DocumentSubstitute> newSubstitutes = updateDocument.Substitutes
                .Where(s => s.DocumentSubstituteId == 0)
                .ToList();

            existingDocument.Substitutes.AddRange(newSubstitutes);
        }

        private void UpdateDocumentConfigParagraphs(DocumentConfig existingDocument, DocumentConfig updateDocument)
        {
            List<DocumentConfigParagraph> documentConfigParagraphs = updateDocument.ConfigParagraphs
                .Where(p => p.DocumentConfigParagraphId > 0)
                .ToList();

            // remove existing configParagraphs that are no longer linked to the existing document.
            List<DocumentConfigParagraph> removeDocumentConfigParagraphs = existingDocument.ConfigParagraphs
                .Where(rp => !documentConfigParagraphs.Any(p => p.DocumentConfigParagraphId.Equals(rp.DocumentConfigParagraphId)))
                .ToList();

            foreach (DocumentConfigParagraph documentConfigParagraph in removeDocumentConfigParagraphs)
            {
                existingDocument.ConfigParagraphs.Remove(documentConfigParagraph);
            }

            if (documentConfigParagraphs.Count > 0)
            {
                (DocumentConfigParagraph existing, DocumentConfigParagraph update) GetMatch(DocumentConfigParagraph existing, DocumentConfigParagraph update)
                {
                    return (existing, update);
                };

                // update existing configParagraphs that are linked to the existing document.
                var matches = from existing in existingDocument.ConfigParagraphs
                              join update in documentConfigParagraphs on existing.DocumentConfigParagraphId equals update.DocumentConfigParagraphId
                              select GetMatch(existing, update);

                foreach (var (existing, update) in matches)
                {
                    _applicationDbContext
                        .Entry(existing)
                        .CurrentValues.SetValues(update);

                    _applicationDbContext.DocumentConfigParagraphs.Update(existing);
                }
            }

            // new configParagraphs to be added to the existing document.
            List<DocumentConfigParagraph> newDocumentConfigParagraphs = updateDocument.ConfigParagraphs
                .Where(p => p.DocumentConfigParagraphId == 0)
                .ToList();

            existingDocument.ConfigParagraphs.AddRange(newDocumentConfigParagraphs);
        }
    }
}
