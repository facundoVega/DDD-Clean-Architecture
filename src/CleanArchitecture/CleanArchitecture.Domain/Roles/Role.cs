using CleanArchitecture.Domain.Permissions;
using CleanArchitecture.Domain.Shared;

namespace CleanArchitecture.Domain.Roles;

public sealed class Role : Enumeration<Role>
{
    public static readonly Role Client = new Role(1,  "Client");
    public static readonly Role Admin = new Role(2,  "Admin");

    public Role(int id, string name) : base(id, name)
    {
    }

    public ICollection<Permission>? Permissions { get; set; }

}