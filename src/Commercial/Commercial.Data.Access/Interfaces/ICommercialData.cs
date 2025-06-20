﻿using Atlas.Data.Access.Interfaces;
using Commercial.Core.Models;

namespace Commercial.Data.Access.Interfaces
{
    public interface ICommercialData : IAuthorisationData
    {
        Task<IEnumerable<Customer>> GetCustomersAsync(CancellationToken cancellationToken);
        Task<Customer?> GetCustomerAsync(int id, CancellationToken cancellationToken);
        Task<Customer?> GetCustomerByProductAsync(int productId, CancellationToken cancellationToken);
        Task<IEnumerable<Company>> GetCompaniesAsync(CancellationToken cancellationToken);
        Task<Company?> GetCompanyAsync(int id, CancellationToken cancellationToken);
    }
}
