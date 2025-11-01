using Tekus.Domain.Dtos;
using Tekus.Domain.Entities;

namespace Tekus.Application.Interfaces;

public interface IProviderService
{
    // This contract defines the application's business logic  for Providers.
        Task<Provider?> GetProviderByIdAsync(int id);
        
        // Now accepts query parameters.
        Task<IEnumerable<Provider>> GetAllProvidersAsync(ProviderQueryParameters queryParameters);
        Task CreateProviderAsync(Provider provider);
        Task UpdateProviderAsync(Provider provider);
        Task DeleteProviderAsync(int id);
        
    

}