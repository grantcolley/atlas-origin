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
                    .AsNoTracking()
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

        public async Task<Customer?> GetCustomerByProductAsync(int productId, CancellationToken cancellationToken)
        {
            try
            {
                Product product = await _applicationDbContext.Products
                    .Include(p => p.Customer)
                    .AsNoTracking()
                    .FirstAsync(p => p.ProductId == productId, cancellationToken)
                    .ConfigureAwait(false);

                Customer customer = await _applicationDbContext.Customers
                    .AsNoTracking()
                    .FirstAsync(c => c.CustomerId == product.CustomerId, cancellationToken)
                    .ConfigureAwait(false) ?? throw new AtlasException($"Cannot find customer by ProductId {productId}");

                customer.Products.Add(product);

                return customer;
            }
            catch (Exception ex)
            {
                throw new AtlasException(ex.Message, ex, $"ProductId={productId}");
            }
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await _applicationDbContext.Companies
                    .AsNoTracking()
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new AtlasException(ex.Message, ex);
            }
        }

        public async Task<Company?> GetCompanyAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                Company company = await _applicationDbContext.Companies
                    .AsNoTracking()
                    .FirstAsync(c => c.CompanyId.Equals(id), cancellationToken)
                    .ConfigureAwait(false);

                if (Authorisation == null
                    || !Authorisation.HasPermission(Auth.COMMERCIAL_WRITE))
                {
                    company.IsReadOnly = true;
                }

                return company;
            }
            catch (Exception ex)
            {
                throw new AtlasException(ex.Message, ex, $"CompanyId={id}");
            }
        }
    }
}
