using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Hires.Events;

public sealed record HireConfirmedDomainEvent(Guid HireID): IDomainEvent;