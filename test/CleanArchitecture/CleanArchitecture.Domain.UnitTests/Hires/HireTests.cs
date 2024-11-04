using CleanArchitecture.Domain.Hires;
using CleanArchitecture.Domain.Hires.Events;
using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.UnitTests.Infrastructure;
using CleanArchitecture.Domain.UnitTests.Users;
using CleanArchitecture.Domain.UnitTests.Vehicles;
using CleanArchitecture.Domain.Users;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Domain.UnitTests.Hires;

public class HireTests: BaseTest
{
    [Fact]
    public void Reserve_Should_RaiseHireReserveDomainEvent()
    {

        var user = User.Create(
            UserMock.Name,
            UserMock.LastName,
            UserMock.Email,
            UserMock.Password
        );

        var price = new Currency(10.0m, CurrencyType.Usd);
        var duration = DateRange.Create(
            new DateOnly(2024, 1,1),
            new DateOnly(2025, 1, 1)
        );

        var vehicle = VehicleMock.Create(price);
        var priceService = new PriceService();


        var hire = Hire.Reserve(
            vehicle,
            user.Id!,
            duration,
            DateTime.UtcNow,
            priceService
        );

        var hireReserveDomainEvent = AssertDomainEventWasPublished<ReservedHireDomainEvent>(hire);

        hireReserveDomainEvent.hireId.Should().Be(hire.Id);
    }
}