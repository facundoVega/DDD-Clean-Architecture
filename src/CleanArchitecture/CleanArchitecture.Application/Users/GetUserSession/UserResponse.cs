namespace CleanArchitecture.Application.Users.GetUserSessions;

public sealed class UserResponse 
{
    public Guid id { get; init; }
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? Lastname { get; set; }

}