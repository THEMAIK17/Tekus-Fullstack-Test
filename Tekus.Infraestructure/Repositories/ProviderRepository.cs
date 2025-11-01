using Microsoft.EntityFrameworkCore;
using Tekus.Domain.Dtos;
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

    public async Task<IEnumerable<Provider>> GetAllAsync(ProviderQueryParameters queryParameters)
        {
            //  Start with the base query (IQueryable)
            // IQueryable allows us to build the SQL query step-by-step
            IQueryable<Provider> query = _context.Providers
                .Include(p => p.Services)
                    .ThenInclude(s => s.Countries)
                .Include(p => p.CustomFields);

            //  Apply Filtering 
            if (!string.IsNullOrWhiteSpace(queryParameters.Name))
            {
                
                query = query.Where(p => p.Name.Contains(queryParameters.Name));
            }

            if (!string.IsNullOrWhiteSpace(queryParameters.Nit))
            {
                query = query.Where(p => p.Nit == queryParameters.Nit);
            }

            //  Apply Sorting
            if (!string.IsNullOrWhiteSpace(queryParameters.SortBy))
            {
                // I use a switch statement to safely apply sorting
                switch (queryParameters.SortBy.ToLowerInvariant())
                {
                    case "name":
                        query = queryParameters.IsDescending
                            ? query.OrderByDescending(p => p.Name)
                            : query.OrderBy(p => p.Name);
                        break;
                    case "nit":
                        query = queryParameters.IsDescending
                            ? query.OrderByDescending(p => p.Nit)
                            : query.OrderBy(p => p.Nit);
                        break;
                    default:
                        query = query.OrderBy(p => p.Id);
                        break;
                }
            }
            else
            {
                
                query = query.OrderBy(p => p.Id);
            }

            //  Apply Paging
            query = query
                .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
                .Take(queryParameters.PageSize);

            // 5. Execute the final, complete SQL query against the database
            return await query.ToListAsync();
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
