using CleanArchitecture.Applications.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehicles;

namespace CleanArchitecture.Application.Vehicles.GetVehiclesByPagination;

public sealed record GetVehiclesByPaginationQuery : SpecificationEntry, IQuery<PaginationResult<Vehicle, VehicleId>>
{
    public string? Model {get; init;}
} 
