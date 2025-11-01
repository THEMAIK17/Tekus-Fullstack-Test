using Tekus.Domain.Entities;

namespace Tekus.Domain.Interfaces;

public interface IProviderRepository
{
    // Reads
    Task<Provider?> GetByIdAsync(int id);
    Task<IEnumerable<Provider>> GetAllAsync();
        
    // Writes (tracks changes, does not save)
    void Add(Provider provider); 
    void Update(Provider provider);
    void Delete(Provider provider);
}