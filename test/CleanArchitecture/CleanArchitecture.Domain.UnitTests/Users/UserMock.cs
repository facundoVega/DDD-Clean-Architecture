using CleanArchitecture.Domain.Users;

namespace CleanArchitecture.Domain.UnitTests.Users;

internal class UserMock
{

    public static readonly Name Name = new Name("Alfonso");
    public static readonly LastName LastName = new LastName("Ramos");
    public static readonly Email Email = new Email("alfonzo.ramos@gmail.com");
    public static readonly PasswordHash Password = new("Test234Test4%");

}