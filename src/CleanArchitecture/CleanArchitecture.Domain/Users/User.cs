using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Roles;
using CleanArchitecture.Domain.Users.Events;

namespace CleanArchitecture.Domain.Users;

public sealed class User : Entity<UserId>
{
    private readonly List<Role> _roles = new();
    private User()
    {
        
    }
    private User(
        UserId id, 
        Name name, 
        LastName lastName,
        Email email,
        PasswordHash passwordHash ): base(id)
    {
        Name = name;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
    }

    public Name? Name { get; private set; }
    public LastName? LastName { get; private set; }
    public Email? Email { get; private set; }
    public PasswordHash? PasswordHash { get; private set; }
    public static User Create(
        Name name, 
        LastName lastName, 
        Email email,
        PasswordHash passwordHash)
    {
        var user =  new User(UserId.New(), name, lastName, email, passwordHash);
        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id!));
        user._roles.Add(Role.Client);

        return user;
    }

    public IReadOnlyCollection<Role>? Roles => _roles.ToList();
}