using CleanArchitecture.Domain.Hires;
using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.UnitTests.Vehicles;
using CleanArchitecture.Domain.Vehicles;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Domain.UnitTests.Hires;

public class PriceServiceTests
{
    [Fact]
    public void CalculatePrice_Should_ReturnCorrectTotalPrice()
    {
        var price = new Currency(10.0m, CurrencyType.Usd);
        var period = DateRange.Create(
            new DateOnly(2024,1,1), 
            new DateOnly(2025,1,1)
        );

        var expectedTotalPrice = new Currency(
            price.Amount * period.DaysAmount, 
            price.CurrencyType 
        );

        var vehicle = VehicleMock.Create(price);
        var priceService = new PriceService();

        var detailPrice = priceService.CalculatePrice(vehicle,period);

        detailPrice.TotalPrice.Should().Be(expectedTotalPrice);
    }

    [Fact]
    public void CalculatePrice_Should_ReturnCorrectTotalPrice_WhenMaintenanceIsIncluded()
    {
        var price = new Currency(10.0m, CurrencyType.Usd);
        var priceMaintenance = new Currency(100.00m, CurrencyType.Usd);

        var period = DateRange.Create(
            new DateOnly(2024,1,1), 
            new DateOnly(2025,1,1)
        );

        var expectedTotalPrice = new Currency(
            (price.Amount * period.DaysAmount) + priceMaintenance.Amount, 
            price.CurrencyType 
        );

        var vehicle = VehicleMock.Create(price, priceMaintenance);
        var priceService = new PriceService();

        var detailPrice = priceService.CalculatePrice(vehicle,period);

        detailPrice.TotalPrice.Should().Be(expectedTotalPrice);
    }
}

