using CleanArchitecture.Domain.Users;

namespace CleanArchitecture.Application.UnitTests.Users;

internal static class UserMock
{
    public static Domain.Users.User Create() => Domain.Users.User.Create(
        Name,
        LastName,
        Email,
        password
    );     
    
    public static readonly Name Name = new("Eduardo");
    public static readonly LastName LastName = new("Garcia");
    public static readonly Email Email = new("Eduardo.Garcia@gmail.com");
    public static readonly PasswordHash password = new("123%%120abc");

}