using Tekus.Domain.Interfaces;

namespace Tekus.Infraestructure.Data;

public class UnitWork
{
    // This class implements the IUnitOfWork interface.
    // It is responsible for committing all database changes.
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TekusDbContext _context;

        public UnitOfWork(TekusDbContext context)
        {
            _context = context;
        }

        // Commits all tracked changes (Inserts, Updates, Deletes)
        // to the database in a single transaction.
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
