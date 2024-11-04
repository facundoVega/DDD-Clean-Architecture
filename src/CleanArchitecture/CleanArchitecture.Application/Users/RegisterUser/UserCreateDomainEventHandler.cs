using CleanArchitecture.Application.Abstractions.Email;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Users.Events;
using MediatR;

namespace CleanArchitecture.Application.Users.RegisterUser;

internal sealed class UserCreateDomainEventHandler
: INotificationHandler<UserCreatedDomainEvent>
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public UserCreateDomainEventHandler(IUserRepository userRepository, IEmailService emailService)
    {
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task Handle(
        UserCreatedDomainEvent notification, 
        CancellationToken cancellationToken)
    {
        var user =  await _userRepository.GetByIdAsync(
            notification.UserID,
            cancellationToken);

        if (user == null)
        {
            return;
        }

        await _emailService.SendAsync(
            user.Email!,
            "Your account has been created in our app",
            "You have a new account in CleanArchitecture"
        );
    }
}