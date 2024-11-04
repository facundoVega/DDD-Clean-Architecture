using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehicles;

namespace CleanArchitecture.Domain.UnitTests.Vehicles;

internal static class VehicleMock
{
    public static Vehicle Create(Currency price, Currency? maintenance = null )
    => new(
        VehicleId.New,
        new Model("Civic"),
        new Vin("TestVin"),
        price,
        maintenance ?? Currency.Zero(),
        DateTime.UtcNow.AddYears(-1),
        [],
        new Direction("USA", "Texas", "Lorenz", "El Paso", "Av. El Alamo")
    );
} 