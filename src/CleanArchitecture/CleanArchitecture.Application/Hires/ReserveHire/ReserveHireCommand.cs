namespace CleanArchitecture.Application.Hires.ReserveHire;

using CleanArchitecture.Application.Abstractions.Messaging;

public record ReserveHireCommand(
    Guid VehicleId,
    Guid UserId,
    DateOnly StartDate,
    DateOnly EndDate
) : ICommand<Guid>;