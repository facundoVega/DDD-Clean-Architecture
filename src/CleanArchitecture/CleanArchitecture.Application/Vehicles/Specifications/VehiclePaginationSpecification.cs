using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Vehicles.Specifications;

public class VehiclePaginationSpecification : BaseSpecification<Vehicle, VehicleId>
{
    public VehiclePaginationSpecification(
        string sort, 
        int pageIndex,
        int pageSize,
        string search
    ) : base(
        x => string.IsNullOrEmpty(search) || x.Model == new Model(search)
    )
    {
        ApplyPaging(pageSize * (pageIndex - 1), pageSize);
        
        if(!string.IsNullOrEmpty(sort))
        {
            switch(sort)
            {
                case "modelAsc": AddOrderBy(p => p.Model!); 
                break;

                case "modelDesc": AddOrderByDescending(p => p.Model!); 
                break;

                default: AddOrderBy(p => p.LastHireDate!);
                break; 
            }
        }
        else
        {
            AddOrderBy(p => p.LastHireDate!);
        }

        
    }
}