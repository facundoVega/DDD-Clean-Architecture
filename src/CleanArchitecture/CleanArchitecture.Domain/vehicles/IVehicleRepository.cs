using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Vehicles;

public interface IVehicleRepository 
{
    Task<Vehicle?> GetByIdAsync(VehicleId id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Vehicle>> GetAllWithSpec(ISpecification<Vehicle, VehicleId> spec);

    Task<int> CountAsync(ISpecification<Vehicle, VehicleId> spec);
}