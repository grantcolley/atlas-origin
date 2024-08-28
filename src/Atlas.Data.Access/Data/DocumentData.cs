using Atlas.Core.Constants;
using Atlas.Core.Exceptions;
using Atlas.Data.Access.Base;
using Atlas.Data.Access.Context;
using Atlas.Data.Access.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Origin.Core.Models;

namespace Atlas.Data.Access.Data
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

        public Task<DocumentConfig> CreateDocumentConfigAsync(DocumentConfig documentConfig, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<DocumentConfig> UpdateDocumentConfigAsync(DocumentConfig documentConfig, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteDocumentConfigAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
