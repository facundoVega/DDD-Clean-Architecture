using System.Linq.Expressions;
using CleanArchitecture.Application.Paginations;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace CleanArchitecture.Infrastructure.Repositories;

internal sealed class UserRepository 
: Repository<User, UserId>, IUserRepository, IPaginationRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<User>()
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }

    public async Task<bool> IsUserExists(
        Email email, 
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<User>()
        .AnyAsync(x => x.Email == email);
    }

    public override void Add(User user)
    {
        foreach(var role in user.Roles!)
        {
            _dbContext.Attach(role);
        }

        _dbContext.Add(user);
    }
}