using CleanArchitecture.Application.Abstractions.Messaging;

namespace CleanArchitecture.Application.Users.RegisterUser;

public sealed record RegisterUserCommand(
    string Email, 
    string Name, 
    string LastNames, 
    string Password) : ICommand<Guid>;