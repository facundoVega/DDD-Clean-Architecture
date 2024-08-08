using CleanArchitecture.Application.Abstractions.Email;
using CleanArchitecture.Domain.Hires.Events;
using CleanArchitecture.Domain.Users;
using MediatR;

namespace CleanArchitecture.Application.Hires.ReserveHire;

internal sealed class ReserveHireDomainEventHandler
: INotificationHandler<ReservedHireDomainEvent>
{
    private readonly IHireRepository _hireRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public ReserveHireDomainEventHandler(
        IHireRepository hireRepository,
        IUserRepository userRepository,
        IEmailService emailService
    )
    {
        _hireRepository = hireRepository;
        _userRepository = userRepository;
        _emailService = emailService;
    }
    

    public async Task Handle(ReservedHireDomainEvent notification, CancellationToken cancellationToken)
    {
        var hire = await _hireRepository.GetByIdAsync(notification.hireId, cancellationToken);

        if(hire is null)
        {
            return;
        }

        var user = await _userRepository.GetByIdAsync(hire.UserId, cancellationToken);

        if(user is null)
        {
            return;
        }

        await _emailService.SendAsync(
            user.Email!, 
            "Hire reserved",
            "You must confirm the reserver otherwise It will be lost.");
    }
}