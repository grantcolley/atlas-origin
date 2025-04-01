using Atlas.Core.Models;

namespace Atlas.Data.Access.Interfaces
{
    public interface IDeveloperData
    {
        void MigrateDatabase();
        Task<DatabaseStatus?> GetDatabaseStatusAsync(string? user, CancellationToken cancellationToken);
        Task<DatabaseStatus?> CreateDatabaseAsync(string? user, CancellationToken cancellationToken);
        Task<DatabaseStatus?> SeedDatabaseAsync(string? user, CancellationToken cancellationToken);        
    }
}
