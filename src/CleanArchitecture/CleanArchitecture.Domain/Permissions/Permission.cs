using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Permissions;

public sealed class Permission : Entity<PermissionId>
{
    private Permission()
    {
    }

    public Permission(PermissionId id, Name name) : base(id)
    {
        Name = name;
    }

    public Permission(Name name) : base()
    {
        Name = name;
    }

    public Name? Name { get; init; }

    public static Result<Permission> Create(Name name)
    {
        return new Permission(name);
    }
}