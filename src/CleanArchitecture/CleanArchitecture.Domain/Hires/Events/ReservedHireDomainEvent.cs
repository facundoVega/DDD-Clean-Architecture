using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Hires.Events;

public sealed record ReservedHireDomainEvent(Guid hireId) : IDomainEvent;