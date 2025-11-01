using Microsoft.EntityFrameworkCore;
using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;
using Tekus.Infraestructure.Data;

namespace Tekus.Infraestructure.Repositories;

// This class implements the IProviderRepository contract.
// It is responsible for all data access operations for the Provider entity.
public class ProviderRepository : IProviderRepository
{
    private readonly TekusDbContext _context;

    public ProviderRepository(TekusDbContext context)
    {
        _context = context;
    }

    public async Task<Provider?> GetByIdAsync(int id)
    {
        // Now I use .Include() to load related data
        return await _context.Providers
            .Include(p => p.Services)        // Eager load Services
                .ThenInclude(s => s.Countries)
            .Include(p => p.CustomFields)    // Eager load CustomFields
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Provider>> GetAllAsync()
    {
        // Add .Include() to load the related data for the entire list
        return await _context.Providers
            .Include(p => p.Services)
                .ThenInclude(s => s.Countries)
            .Include(p => p.CustomFields)
            .ToListAsync();
    }

    public void Add(Provider provider)
    {
       
        _context.Providers.Add(provider);
    }

    public void Update(Provider provider)
    {
        
        _context.Providers.Update(provider);
    }

    public void Delete(Provider provider)
    {
       
        _context.Providers.Remove(provider);
    }
}
