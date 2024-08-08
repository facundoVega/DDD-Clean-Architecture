using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Hires;
using CleanArchitecture.Domain.Vehicles;
using CleanArchitecture.Domain.Reviews.Events;

namespace CleanArchitecture.Domain.Reviews;

public sealed class Review : Entity
{
    private Review()
    {
        
    }
    private Review(
        Guid id,
        Guid vehicleId,
        Guid hireId,
        Guid userId,
        Rating rating,
        Comment comment,
        DateTime creationDate
    ) : base(id)
    {
        VehicleId = vehicleId;
        HireId = hireId;
        UserId = userId;
        Rating = rating;
        Comment = comment;
        CreationDate = creationDate;
    }

    public Guid VehicleId { get; private set; }
    public Guid HireId { get; private set; }
    public Guid UserId { get; private set; }
    public Rating Rating { get; private set; }
    public Comment Comment { get; private set; }
    public DateTime? CreationDate { get; private set; }

    public static Result<Review> Create(
        Hire hire,
        Rating rating,
        Comment comment,
        DateTime creationDate
    )
    {
        if(hire.Status != HireStatus.Completed)
        {
            return Result.Failure<Review>(ReviewErrors.NotEligible);
        }

        var review = new Review(
            Guid.NewGuid(),
            hire.VehicleId,
            hire.Id,
            hire.UserId,
            rating,
            comment,
            creationDate
        );

        review.RaiseDomainEvent(new ReviewCreatedDomainEvent(review.Id));
        return review;
    }
}