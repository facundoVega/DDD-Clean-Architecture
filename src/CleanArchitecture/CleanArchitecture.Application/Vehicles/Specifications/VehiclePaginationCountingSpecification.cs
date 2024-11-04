using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Vehicles.Specifications;

public class VehiclePaginationCountingSpecification 
: BaseSpecification <Vehicle, VehicleId>
{
    public VehiclePaginationCountingSpecification(string search)
    : base(
        x => string.IsNullOrEmpty(search) || x.Model == new Model(search)
    )
    {
    }
}