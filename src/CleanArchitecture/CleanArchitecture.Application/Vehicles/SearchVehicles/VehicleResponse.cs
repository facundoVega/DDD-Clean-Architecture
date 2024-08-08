using CleanArchitecture.Applications.Vehicles.SearchVehicles;

namespace CleanArchitecture.Application.SearchVehicles;


public sealed class VehicleResponse
{
    public Guid Id { get; init; }
    public string? Model { get; init; }
    public string? Vin { get; init; }
    public decimal Price { get; init; }
    public string? CurrencyType { get; init; }
    public ResponseDirection? Direction { get; set;} 
}