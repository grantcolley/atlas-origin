using Atlas.Data.Context;
using Commercial.Core.Models;

namespace Commercial.Data.Access.Interfaces
{
    public interface ICommercialData : IAuthorisationData
    {
        Task<IEnumerable<Customer>> GetCustomersAsync(CancellationToken cancellationToken);
        Task<Customer?> GetCustomerAsync(int id, CancellationToken cancellationToken);
    }
}
