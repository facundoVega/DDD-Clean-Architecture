using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Vehicles;

public static class VehicleErrors 
{
    public static Error NotFound = new(
        "Vehicle.Found",
        "Vehicle with this ID does not exist."
    );
}