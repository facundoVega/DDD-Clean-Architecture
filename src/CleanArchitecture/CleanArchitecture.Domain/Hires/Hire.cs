using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Hires.Events;
using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehicles;
using CleanArchitecture.Domain.Hires.Events;

namespace CleanArchitecture.Domain.Hires;

public sealed class Hire : Entity
{
    private Hire() 
    {

    }
    
    private Hire(
        Guid id,
        Guid vehicleId,
        Guid userId,
        DateRange duration,
        Currency periodPrice,
        Currency maintenance,
        Currency appliances,
        Currency totalPrice,
        HireStatus status,
        DateTime creationDate

        ) : base(id)
    {
        VehicleId = vehicleId;
        UserId = userId;
        Duration = duration;
        PeriodPrice = periodPrice;
        Maintenance = maintenance;
        Appliances = appliances;
        TotalPrice = totalPrice;
        Status = status;
        CreationDate = creationDate;
    }

    public Guid VehicleId { get; private set; }
    public Guid UserId { get; private set; }
    public Currency? PeriodPrice { get; private set; }
    public Currency? Maintenance { get; private set; }
    public Currency? Appliances { get; private set; }
    public Currency? TotalPrice { get; private set; }
    public HireStatus Status { get; private set; }
    public DateRange Duration { get; private set; }
    public DateTime? CreationDate { get; private set; }
    public DateTime? ConfirmationDate { get; private set; }
    public DateTime? NegationtionDate { get; private set; }
    public DateTime? CompletedDate { get; private set; }
    public DateTime? CancellationDate { get; private set; }

    public static Hire Reserve(
        Vehicle vehicle,
        Guid userId,
        DateRange duration,
        DateTime creationDate,
        PriceService priceService
    )
    {

        var detailPrice = priceService.CalculatePrice(
            vehicle,
            duration
        );

        var hire = new Hire(
            Guid.NewGuid(),
            vehicle.Id,
            userId,
            duration,
            detailPrice.PeriodPrice,
            detailPrice.Maintenance,
            detailPrice.Appliances,
            detailPrice.TotalPrice,
            HireStatus.Reserved,
            creationDate
        );

        hire.RaiseDomainEvent(new ReservedHireDomainEvent(hire.Id));

        vehicle.LastHireDate = creationDate;

        return hire;
    }

    public Result Confirm(DateTime utcNow)
    {
        if(Status != HireStatus.Reserved)
        {
            return Result.Failure(HireErrors.NotReserved);
        }

        Status = HireStatus.Confirmed;
        ConfirmationDate = utcNow;

        RaiseDomainEvent(new HireConfirmedDomainEvent(Id));
        return Result.Success();
    }

    public Result Reject(DateTime utcNow)
    {
        if(Status != HireStatus.Reserved)
        {
            return Result.Failure(HireErrors.NotReserved);
        }

        Status = HireStatus.Rejected;
        ConfirmationDate = utcNow;

        RaiseDomainEvent(new HireRejectedDomainEvent(Id));
        return Result.Success();
    }

    public Result Cancel(DateTime utcNow)
    {
        if(Status != HireStatus.Confirmed)
        {
            return Result.Failure(HireErrors.NotConfirmed);
        }

        var currentDate = DateOnly.FromDateTime(utcNow);

        if(currentDate > Duration.Start) 
        {
            return Result.Failure(HireErrors.AlreadyStarted);
        } 

        Status = HireStatus.Cancelled;
        CancellationDate = utcNow;

        RaiseDomainEvent(new HireCancelledDomainEvent(Id));
        return Result.Success();
    }

    public Result Complete(DateTime utcNow)
    {
        if(Status != HireStatus.Confirmed)
        {
            return Result.Failure(HireErrors.NotConfirmed);
        }

        Status = HireStatus.Completed;
        CompletedDate = utcNow;

        RaiseDomainEvent(new HireCompletedDomainEvent(Id));
        return Result.Success();
    }

}