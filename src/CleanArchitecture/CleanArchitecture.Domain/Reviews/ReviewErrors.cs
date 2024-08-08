using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Reviews;

public static class ReviewErrors 
{
    public static readonly Error NotEligible = new Error(
        "Review.NotEligible",
        "This review and calification for the vehicle is not eligible because hire is not completed"
    );

}