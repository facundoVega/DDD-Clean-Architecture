using CleanArchitecture.Applications.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Users;

namespace CleanArchitecture.Application.User.GetUsersPagination;

public record GetUsersPaginationQuery 
: PaginationParams, IQuery<PagedResults<Domain.Users.User, UserId>> 
{
    
}