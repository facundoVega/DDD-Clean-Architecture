using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Users;

public static class UserErrors 
{
    public static Error NotFound = new Error(
        "User.Found",
        "The user with this Id does not exist"
    );

    public static Error InvalidCredentials = new Error(
        "User.InvalidCredentials",
        "Credentials are wrong"
    );

    
    public static Error  AlreadyExists = new Error(
        "User.AlreadyExists",
        "The user already exists on the Database"
    );
}