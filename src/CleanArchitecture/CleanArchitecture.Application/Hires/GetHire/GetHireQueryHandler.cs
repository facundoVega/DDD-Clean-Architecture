using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Applications.Abstractions.Data;
using CleanArchitecture.Applications.Hires.GetHire;
using Dapper;

namespace CleanArchitecture.Application.Hires.GetHire;

internal sealed class GetQueryHandler : IQueryHandler<GetHireQuery, HireResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<HireResponse>> Handle(
        GetHireQuery request, 
        CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        var sql = """ 
            SELECT 
                id AS Id,
                vehicle_id AS VehicleId,
                user_id AS UserID,
                status AS Status,
                period_price_amount AS HirePrice,
                period_price_currency_type AS CurrencyTypeHire,
                maintenance_amount AS MaintenancePrice,
                maintenance_currency_type AS MaintenanceCurrencyType,
                appliances_amount AS AppliancePrice,
                appliances_currency_type AS ApplianceCurrencyType,
                total_price_amount AS TotalPrice,
                total_price_currency_type AS TotalPriceCurrencyType,
                duration_start AS DurationStart,
                duration_end AS DurationEnd,
                creation_date AS CreationDate
            FROM hires WHERE id=@hireId 
        """; 

        var hire = await connection.QueryFirstOrDefaultAsync<HireResponse>(
            sql,
            new {
                request.HireId
            }
        );

        return hire!;
    }
}