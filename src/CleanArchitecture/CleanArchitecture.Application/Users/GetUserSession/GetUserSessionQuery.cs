using CleanArchitecture.Applications.Abstractions.Messaging;

namespace CleanArchitecture.Application.Users.GetUserSessions;

public sealed record GetUserSessionQuery:
IQuery<UserResponse>;