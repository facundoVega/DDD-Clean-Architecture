namespace CleanArchitecture.Domain.Vehicles;

public record VehicleId(Guid Value)
{
    public static VehicleId New => new (new Guid());
}