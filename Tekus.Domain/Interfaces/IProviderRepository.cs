using Tekus.Domain.Dtos;
using Tekus.Domain.Entities;

namespace Tekus.Domain.Interfaces;

public interface IProviderRepository
{
    // Reads
    Task<Provider?> GetByIdAsync(int id);
    
    // Now accepts query parameters to handle paging, sorting, and filtering.
    Task<IEnumerable<Provider>> GetAllAsync(ProviderQueryParameters queryParameters);
        
    // Writes (tracks changes, does not save)
    void Add(Provider provider); 
    void Update(Provider provider);
    void Delete(Provider provider);
}