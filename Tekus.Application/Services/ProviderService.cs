using Tekus.Application.Interfaces;
using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;

namespace Tekus.Application.Services
{
    // Implements the business logic for Providers.
    // Orchestrates the repository and the unit of work.
    public class ProviderService : IProviderService
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProviderService(IProviderRepository providerRepository, IUnitOfWork unitOfWork)
        {
            _providerRepository = providerRepository;
            _unitOfWork = unitOfWork;
        }
        // Read operations pass through directly to the repository.
        public async Task<Provider?> GetProviderByIdAsync(int id)
        {
            return await _providerRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Provider>> GetAllProvidersAsync()
        {
            return await _providerRepository.GetAllAsync();
        }
        
        // --- Business Logic Validations ---
        // Validations as required by the PDF.
        public async Task CreateProviderAsync(Provider provider)
        {
            
            if (string.IsNullOrWhiteSpace(provider.Name))
            {
                throw new System.Exception("Provider name cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(provider.Nit))
            {
                throw new System.Exception("Provider NIT cannot be empty.");
            }
            
            

           
            _providerRepository.Add(provider);

         
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateProviderAsync(Provider provider)
        {
           

            
            _providerRepository.Update(provider);

         
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteProviderAsync(int id)
        {
           
            var provider = await _providerRepository.GetByIdAsync(id);

            if (provider == null)
            {
                
                throw new System.Exception("Provider not found.");
            }

           
            _providerRepository.Delete(provider);

            
            await _unitOfWork.SaveChangesAsync();
        }
    }
}