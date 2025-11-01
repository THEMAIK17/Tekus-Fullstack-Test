using Tekus.Domain.Entities;

namespace Tekus.Application.Interfaces;

public interface IProviderService
{
    // This contract defines the application's business logic  for Providers.
        Task<Provider?> GetProviderByIdAsync(int id);
        Task<IEnumerable<Provider>> GetAllProvidersAsync();
        Task CreateProviderAsync(Provider provider);
        Task UpdateProviderAsync(Provider provider);
        Task DeleteProviderAsync(int id);
        
    

}