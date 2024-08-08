namespace CleanArchitecture.Api.Controllers.Hires;
public sealed record ReserveHireRequest(
    Guid VehicleId,
    Guid UserId,
    DateOnly StartDate,
    DateOnly EndDate
);