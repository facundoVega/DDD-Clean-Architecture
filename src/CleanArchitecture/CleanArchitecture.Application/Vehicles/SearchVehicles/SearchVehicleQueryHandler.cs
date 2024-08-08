using System.Reflection.Metadata.Ecma335;
using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Application.SearchVehicles;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Hires;
using CleanArchitecture.Domain.Vehicles;
using CleanArchitecture.Applications.Abstractions.Data;
using Dapper;

namespace CleanArchitecture.Applications.Vehicles.SearchVehicles;

internal sealed class SearchVehiclesQueryHandler
: IQueryHandler<SearchVehiclesQuery, IReadOnlyList<VehicleResponse>>
{
    private static readonly int[] ActiveHireStatuses = 
    {
        (int)HireStatus.Reserved,
        (int)HireStatus.Confirmed,
        (int)HireStatus.Completed
    };
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public SearchVehiclesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<VehicleResponse>>> Handle(SearchVehiclesQuery request, CancellationToken cancellationToken)
    {
        if(request.startDate > request.dateEnd)
        {
            return new List<VehicleResponse>();
        }

        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """ 
        
            SELECT 
            a.id as Id,
            a.model as Model,
            a.vin as Vin,
            a.amount_price as Price,
            a.currency_type_price as CurrencyType,
            a.country_direction as Country,
            a.department_direction as Department,
            a.province_direction as Province,
            a.city_direction as City,
            a.street_direction as Street
            FROM vehicles AS a
            WHERE NOT EXIST 
            (
                SELECT 1 
                FROM hires AS b
                WHERE 
                    b.vehicle_id = a.id AND
                    b.duration_Start <= @EndDate AND
                    b.duration_end >= @StartDate AND
                    b.status = ANY(@ActiveHireStatuses)
            )
        """;
    
        var vehicles = await connection
            .QueryAsync<VehicleResponse, ResponseDirection, VehicleResponse>
            (
                sql,
                (vehicle, direction ) => {
                    vehicle.Direction = direction;
                    return vehicle;
                },
                new
                {
                    StartDate = request.startDate,
                    EndDate = request.dateEnd,
                    ActiveHireStatuses
                },
                splitOn: "Country"
            );

        return vehicles.ToList();
    }
}