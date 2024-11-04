using System.Linq.Expressions;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Users;


namespace CleanArchitecture.Application.Paginations;

public interface IPaginationRepository
{
    Task<PagedResults<Domain.Users.User, UserId>> GetPaginationAsync(
        Expression<Func<Domain.Users.User, bool>>? predicate,
        Func<IQueryable<Domain.Users.User>, Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Domain.Users.User, object >> includes,
        int page,
        int pageSize,
        string orderBy,
        bool ascending,
        bool disableTracking = true
    );

}