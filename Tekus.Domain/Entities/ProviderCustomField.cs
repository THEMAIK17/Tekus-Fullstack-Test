namespace Tekus.Domain.Entities;

public class ProviderCustomField
{
    public int Id { get; set; }
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;

    // --- Relations ---
    public int ProviderId { get; set; }
    public Provider Provider { get; set; } = null!;
}
