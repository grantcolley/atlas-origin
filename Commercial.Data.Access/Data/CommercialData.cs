using Atlas.Core.Constants;
using Atlas.Core.Exceptions;
using Atlas.Data.Context;
using Commercial.Core.Models;
using Commercial.Data.Access.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Commercial.Data.Access.Data
{
    public class CommercialData(ApplicationDbContext applicationDbContext, ILogger<CommercialData> logger)
        : AuthorisationData<CommercialData>(applicationDbContext, logger), ICommercialData
    {
        public async Task<IEnumerable<Customer>> GetCustomersAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await _applicationDbContext.Customers
                    .AsNoTracking()
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new AtlasException(ex.Message, ex);
            }
        }

        public async Task<Customer?> GetCustomerAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                Customer customer = await _applicationDbContext.Customers
                    .Include(c => c.Products)
                    .FirstAsync(c => c.CustomerId.Equals(id), cancellationToken)
                    .ConfigureAwait(false);

                if (Authorisation == null
                    || !Authorisation.HasPermission(Auth.COMMERCIAL_WRITE))
                {
                    customer.IsReadOnly = true;
                }

                return customer;
            }
            catch (Exception ex)
            {
                throw new AtlasException(ex.Message, ex, $"CustomerId={id}");
            }
        }
    }
}
