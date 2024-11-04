using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Application.Paginations;
using CleanArchitecture.Application.User.GetUsersPagination;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Users;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Users.GetUsersPagination;

internal sealed class GetUsersPaginatinQueryHanlder
: IQueryHandler<GetUsersPaginationQuery, PagedResults<Domain.Users.User, UserId>>
{
    private readonly IPaginationRepository _paginationRepository;

    public GetUsersPaginatinQueryHanlder(IPaginationRepository paginationRepository)
    {
        _paginationRepository = paginationRepository;
    }

    public async Task<Result<PagedResults<Domain.Users.User, UserId>>> Handle(GetUsersPaginationQuery request, CancellationToken cancellationToken)
    {
        var predicateb = PredicateBuilder.New<Domain.Users.User>(true);

        if(!string.IsNullOrEmpty(request.Search))
        {
            predicateb = predicateb.Or(p => p.Name ==  new Name(request.Search));
            predicateb = predicateb.Or(p => p.Email == new Email(request.Search));
        }
        
        var pagedResultUsers = await _paginationRepository.GetPaginationAsync(
            predicateb,
            p => p.Include(x => x.Roles!).ThenInclude(y => y.Permissions!),
            request.PageNumber,
            request.PageSize,
            request.OrderBy!,
            request.OrderAsc
        );

        return pagedResultUsers;

    }
}