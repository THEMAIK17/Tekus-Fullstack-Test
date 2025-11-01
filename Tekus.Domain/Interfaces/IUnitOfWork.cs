namespace Tekus.Domain.Interfaces;

// Defines the contract for the Unit of Work pattern.
public interface IUnitOfWork
{
    
    // Asynchronously saves all pending changes to the database.
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    

}