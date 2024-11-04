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
            a.price_amount as Price,
            a.price_currency_type as CurrencyType,
            a.direction_country as Country,
            a.direction_department as Department,
            a.direction_province as Province,
            a.direction_city as City,
            a.direction_street as Street
            FROM vehicles AS a
            WHERE NOT EXISTS
            (
                SELECT 1 
                FROM hires AS b
                WHERE 
                    b.vehicle_id = a.id AND
                    b.duration_start <= @EndDate AND
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