namespace CleanArchitecture.Application.Users.RegisterUser;

public record RegisterUserRequest(
    string Email,
    string Name,
    string LastNames,
    string Password
);