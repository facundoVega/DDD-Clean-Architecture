using System.Data.SqlTypes;
using Bogus;
using CleanArchitecture.Applications.Abstractions.Data;
using CleanArchitecture.Domain.Vehicles;
using Dapper;

namespace CleanArchitecture.Api.Extensions;

public static class SeedDataExtensions 
{
    public static void SeedData(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();
        using var connection = sqlConnectionFactory.CreateConnection();

        var faker  =  new Faker();

        List<object> vehicles = new();

        for(var i = 0; i < 100; i++)
        {
            vehicles.Add(new 
            {
                Id = Guid.NewGuid(),
                Vin = faker.Vehicle.Vin(),
                Model = faker.Vehicle.Model(),
                Country = faker.Address.Country(),
                Department = faker.Address.State(),
                Province = faker.Address.County(),
                City = faker.Address.City(),
                Street = faker.Address.StreetAddress(),
                PriceAmount = faker.Random.Decimal(1000, 20000),
                PriceCurrencyType = "USD",
                MaintenanceAmount = faker.Random.Decimal(100, 200),
                MaintenanaceCurrencyType = "USD",
                Appliances = new List<int>{ (int)Appliance.Wifi, (int)Appliance.AppleCar },
                LastHireDate = DateTime.MinValue
            });
        }

        const string sql = """ 
                INSERT INTO public.vehicles
                (id, vin, model, direction_country, direction_department, direction_province, direction_city, direction_street, price_amount, price_currency_type, maintenance_amount, maintenance_currency_type, appliances, last_hire_date )
                VALUES(@id, @Vin, @Model, @Country, @Department, @Province, @City, @Street, @PriceAmount, @PriceCurrencyType, @MaintenanceAmount, @MaintenanaceCurrencyType, @Appliances, @LastHireDate ) 
        """;

        connection.Execute(sql, vehicles);
    }
}