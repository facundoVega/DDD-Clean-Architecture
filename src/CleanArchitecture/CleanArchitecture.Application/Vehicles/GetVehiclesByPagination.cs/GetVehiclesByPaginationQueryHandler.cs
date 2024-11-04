using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Vehicles;
using CleanArchitecture.Domain.Vehicles.Specifications;

namespace CleanArchitecture.Application.Vehicles.GetVehiclesByPagination;

internal sealed class GetVehiclesByPaginationQueryHandler
: IQueryHandler<GetVehiclesByPaginationQuery, PaginationResult<Vehicle, VehicleId>>
{
    private readonly IVehicleRepository _vehicleRepository;

    public GetVehiclesByPaginationQueryHandler(IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }
    public async Task<Result<PaginationResult<Vehicle, VehicleId>>> Handle(
        GetVehiclesByPaginationQuery request, 
        CancellationToken cancellationToken
    )
    {

        var spec = new VehiclePaginationSpecification(
            request.Sort!,
            request.PageIndex,
            request.PageSize,
            request.Model!
        );

        var records = await _vehicleRepository.GetAllWithSpec(spec);

        var specCount = new VehiclePaginationCountingSpecification(request.Model!);
    
        var totalRecords = await _vehicleRepository.CountAsync(specCount);

        var rounded =  Math.Ceiling( Convert.ToDecimal(totalRecords) / Convert.ToDecimal(request.PageSize));

        var totalPages = Convert.ToInt32(rounded);    

        var recordsByPage = records.Count();

        return new PaginationResult<Vehicle, VehicleId>
        {
            Count = totalRecords,
            Data = records.ToList(),
            PageCount = totalPages,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            ResultByPage = recordsByPage
        };  

    }
}