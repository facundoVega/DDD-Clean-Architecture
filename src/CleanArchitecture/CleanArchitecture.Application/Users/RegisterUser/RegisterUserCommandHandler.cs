using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Users;

namespace CleanArchitecture.Application.Users.RegisterUser;

internal class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork
        )
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        RegisterUserCommand request, 
        CancellationToken cancellationToken
        )
    {
        var email = new Email(request.Email);
        var userExist = await _userRepository.IsUserExists(email);

        if(userExist)
        {
            return Result.Failure<Guid>(UserErrors.AlreadyExists);
        }

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = Domain.Users.User.Create(
            new Name(request.Name),
            new LastName(request.LastNames),
            new Email(request.Email),
            new PasswordHash(passwordHash)
        );

        _userRepository.Add(user);

        await _unitOfWork.SaveChangesAsync();

        return user.Id!.Value;
    }
}