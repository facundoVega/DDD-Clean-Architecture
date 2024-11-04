using CleanArchitecture.Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Infrastructure.Authentication;

internal sealed class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccesor;

    public UserContext(IHttpContextAccessor httpContextAccesor)
    {
        _httpContextAccesor = httpContextAccesor;
    }

    public string Email => _httpContextAccesor.HttpContext?
    .User.GetUserMail() ?? throw new ApplicationException("The user context is invalid.");

    public Guid UserId => _httpContextAccesor
    .HttpContext?.User.GetUserId() ?? throw new ApplicationException("The user context is invalid.");
}