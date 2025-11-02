using Microsoft.AspNetCore.Mvc;
using Tekus.Application.Interfaces;
using Tekus.Domain.Dtos;
using Tekus.Domain.Entities;

namespace Tekus.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Sets the base URL to "api/Providers"
    public class ProvidersController : ControllerBase
    {
        private readonly IProviderService _providerService;

        // Inject the business logic service
        public ProvidersController(IProviderService providerService)
        {
            _providerService = providerService;
        }

        // GET: api/Providers?pageNumber=1&pageSize=10&sortBy=Name&name=Tekus
        [HttpGet]
        public async Task<IActionResult> GetAllProviders([FromQuery] ProviderQueryParameters queryParameters)
        {
            // Pass the query parameters to the service
            var providers = await _providerService.GetAllProvidersAsync(queryParameters);
            return Ok(providers);
        }

        // GET: api/Providers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProviderById(int id)
        {
            var provider = await _providerService.GetProviderByIdAsync(id);
            
            if (provider == null)
            {
                return NotFound(); // Returns 404
            }
            
            return Ok(provider); // Returns 200
        }

        // POST: api/Providers
        [HttpPost]
        public async Task<IActionResult> CreateProvider([FromBody] Provider? provider)
        {
            if (provider == null)
            {
                return BadRequest(); // Returns 400
            }
            
            await _providerService.CreateProviderAsync(provider);
            
            // Returns 201 Created with a link to the new resource
            return CreatedAtAction(nameof(GetProviderById), new { id = provider.Id }, provider);
        }

        // PUT: api/Providers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProvider(int id, [FromBody] Provider provider)
        {
            if (id != provider.Id)
            {
                return BadRequest("ID mismatch");
            }

            await _providerService.UpdateProviderAsync(provider);
            return NoContent(); // Returns 204
        }

        // DELETE: api/Providers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvider(int id)
        {
            await _providerService.DeleteProviderAsync(id);
            return NoContent(); // Returns 204
        }
    }
}