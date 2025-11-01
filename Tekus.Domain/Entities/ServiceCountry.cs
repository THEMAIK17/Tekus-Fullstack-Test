namespace Tekus.Domain.Entities;

public class ServiceCountry
{
    public int Id { get; set; }
    public string CountryName { get; set; } = string.Empty;

    // --- Relations ---
    public int ServiceId { get; set; }
    public Service Service { get; set; } = null!;
}
