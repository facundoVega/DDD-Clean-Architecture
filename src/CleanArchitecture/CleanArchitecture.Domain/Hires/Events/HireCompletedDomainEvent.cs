using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Hires.Events;

public sealed record HireCompletedDomainEvent(Guid HireID): IDomainEvent;