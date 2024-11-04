using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Hires;
using CleanArchitecture.Domain.Vehicles;
using CleanArchitecture.Domain.Reviews.Events;
using CleanArchitecture.Domain.Users;

namespace CleanArchitecture.Domain.Reviews;

public sealed class Review : Entity<ReviewId>
{
    private Review()
    {
        
    }
    private Review(
        ReviewId id,
        VehicleId vehicleId,
        HireId hireId,
        UserId userId,
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

    public VehicleId? VehicleId { get; private set; }
    public HireId? HireId { get; private set; }
    public UserId? UserId { get; private set; }
    public Rating? Rating { get; private set; }
    public Comment? Comment { get; private set; }
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
            ReviewId.New(),
            hire.VehicleId!,
            hire.Id!,
            hire.UserId!,
            rating,
            comment,
            creationDate
        );

        review.RaiseDomainEvent(new ReviewCreatedDomainEvent(review.Id!));
        return review;
    }
}