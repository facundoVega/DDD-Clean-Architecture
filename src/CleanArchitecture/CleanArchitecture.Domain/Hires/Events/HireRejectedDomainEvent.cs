using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Hires.Events;

public sealed record HireRejectedDomainEvent(Guid HireID): IDomainEvent;