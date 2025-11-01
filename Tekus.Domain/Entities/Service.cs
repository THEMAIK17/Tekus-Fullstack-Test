namespace Tekus.Domain.Entities;

public class Service
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal HourlyRateUsd { get; set; }

    // --- Relations ---
    public int ProviderId { get; set; }
    public Provider Provider { get; set; } = null!; 
        
    public ICollection<ServiceCountry> Countries { get; set; } = new List<ServiceCountry>();
    
}