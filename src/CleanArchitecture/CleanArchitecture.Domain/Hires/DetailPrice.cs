using CleanArchitecture.Domain.Shared;

namespace CleanArchitecture.Domain.Hires;

public record DetailPrice(
    Currency PeriodPrice,
    Currency Maintenance,
    Currency Appliances,
    Currency TotalPrice
);