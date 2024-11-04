using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Hires.Events;

public sealed record HireRejectedDomainEvent(HireId HireID): IDomainEvent;