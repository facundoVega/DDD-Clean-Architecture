using FluentValidation;

namespace CleanArchitecture.Application.Hires.ReserveHire;

public class ReserveHireCommandValidator : AbstractValidator<ReserveHireCommand>
{
    
    public ReserveHireCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.VehicleId).NotEmpty();
        RuleFor(c => c.StartDate).LessThan(c => c.EndDate);
    }
}