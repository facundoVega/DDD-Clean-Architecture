using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Applications.Abstractions.Clock;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Ingrastructure.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CleanArchitecture.Infrastructure;

public sealed class ApplicationDbContext: DbContext, IUnitOfWork 
{
    private static readonly JsonSerializerSettings jsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All
    };
    private readonly IDateTimeProvider _dateTimeProvider;
    public ApplicationDbContext(
        DbContextOptions options, 
        IDateTimeProvider dateTimeProvider) : base(options)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            AddDomainEventsToOutboxMessages();

            var result = await base.SaveChangesAsync(cancellationToken);


            return result;
        }
        catch(DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("The exception for concurrency has been thrown", ex);
        }

    }

    private void AddDomainEventsToOutboxMessages()
    {
        var outboxMessages = ChangeTracker
            .Entries<IEntity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity => 
            {
                var domainEvents = entity.GetDomainEvents();
                entity.ClearDomainEvents();
                return domainEvents;
            })
            .Select(domainEvent  => new OutboxMessage (
                Guid.NewGuid(),
                _dateTimeProvider.currentTime,
                domainEvent.GetType().Name,
                JsonConvert.SerializeObject(domainEvent, jsonSerializerSettings)
            ))
            .ToList();         

        AddRange(outboxMessages);
    }
}