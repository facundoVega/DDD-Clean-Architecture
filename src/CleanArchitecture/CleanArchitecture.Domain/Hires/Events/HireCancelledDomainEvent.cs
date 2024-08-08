using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Hires.Events;

public sealed record HireCancelledDomainEvent(Guid HireID): IDomainEvent;