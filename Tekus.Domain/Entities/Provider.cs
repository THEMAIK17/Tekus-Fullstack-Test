namespace Tekus.Domain.Entities;

public class Provider
{
    public int Id { get; set; }
    public string Nit { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    // --- Relations ---
    public ICollection<Service> Services { get; set; } = new List<Service>();
    public ICollection<ProviderCustomField> CustomFields { get; set; } = new List<ProviderCustomField>();
    
}