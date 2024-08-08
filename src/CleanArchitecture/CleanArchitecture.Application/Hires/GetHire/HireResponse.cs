namespace CleanArchitecture.Application.Hires.GetHire;

public sealed class HireResponse
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public Guid VehicleId { get; init; }
    public int Status { get; init; }
    public decimal HirePrice {get; init; }
    public string? CurrencyTypeHire { get; init; }
    public decimal MaintenancePrice {get; init; }
    public string? MaintenanceCurrencyType { get; init; }
    public decimal AppliancePrice {get; init; }
    public string? ApplianceCurrencyType { get; init;}
    public decimal TotalPrice {get; init; }
    public string? TotalPriceCurrencyType { get; init; }
    public DateOnly DurationStart { get; init; }
    public DateOnly DurationEnd { get; init; }
    public DateTime CreationDate { get; init; }
}
