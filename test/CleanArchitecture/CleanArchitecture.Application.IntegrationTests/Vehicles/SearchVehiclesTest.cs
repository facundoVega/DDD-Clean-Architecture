using CleanArchitecture.Applications.Vehicles.SearchVehicles;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Application.IntegrationTests.Vehicles;

public class SearchVehicles : BaseIntegrationTest
{
    public SearchVehicles(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task SearchVehicles_ShouldReturnEmptyList_WhenDateRangeInvalid()
    {
        var query = new SearchVehiclesQuery(
            new DateOnly(2023,1,1),
            new DateOnly(2022,1,1) 
        );

        var result = await Sender.Send(query);

        result.Value.Should().BeEmpty();
    }

    [Fact]
    public async Task SearchVehicles_ShouldReturnVehicles_WhenDateRangeIsValid()
    {
        var query = new SearchVehiclesQuery(
            new DateOnly(2023,1,1),
            new DateOnly(2026,1,1)
        );

        var result = await Sender.Send(query);

        result.IsSuccess.Should().BeTrue();
    }
}
