using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehicles;

namespace CleanArchitecture.Application.UnitTests.Vehicles;

internal static class VehicleMock
{
    public static Vehicle Create() => new Vehicle(
        new VehicleId(Guid.NewGuid()),
        new Model("Civic"),
        new Vin("34545315"),
        new Currency(150.0m, CurrencyType.Usd),
        Currency.Zero(),
        DateTime.UtcNow,
        [],
        new Direction("USA", "Texas", "Laredo", "Limon", "Av. the highfly bird")
    );
}