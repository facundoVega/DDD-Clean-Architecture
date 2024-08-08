using CleanArchitecture.Application.SearchVehicles;
using CleanArchitecture.Applications.Abstractions.Messaging;

namespace CleanArchitecture.Applications.Vehicles.SearchVehicles;

public sealed record SearchVehiclesQuery(
    DateOnly startDate,
    DateOnly dateEnd 
) :IQuery<IReadOnlyList<VehicleResponse>>;