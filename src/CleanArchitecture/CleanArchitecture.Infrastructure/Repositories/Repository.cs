using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Infrastructure.Extensions;
using CleanArchitecture.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace CleanArchitecture.Infrastructure.Repositories;

internal abstract class Repository<TEntity, TEntityId>
where TEntity : Entity<TEntityId>
where TEntityId : class
{
    protected readonly ApplicationDbContext _dbContext;

    protected Repository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TEntity?> GetByIdAsync(TEntityId id, CancellationToken cancellationToken= default)
    {
        return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public virtual void Add(TEntity entity)
    {
        _dbContext.Add(entity);
    }

    public IQueryable<TEntity> ApplySpecification(
        ISpecification<TEntity, TEntityId> spec
    )
    {
        return SpecificationEvaluator<TEntity, TEntityId>
        .GetQuery(
            _dbContext.Set<TEntity>().AsQueryable(),
            spec    
        );
    }
    public async Task<IReadOnlyList<TEntity>> GetAllWithSpec(ISpecification<TEntity, TEntityId > spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    public async Task<int> CountAsync(ISpecification<TEntity, TEntityId> spec)
    {
        return await ApplySpecification(spec).CountAsync();
    }

    public async Task<PagedResults<TEntity, TEntityId>> GetPaginationAsync
    (
        Expression<Func<TEntity, bool>>? predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes,
        int page,
        int pageSize,
        string orderBy,
        bool ascending,
        bool disabledTracking = true
    )
    {
        IQueryable<TEntity> queryable = _dbContext.Set<TEntity>();

        if(disabledTracking) queryable = queryable.AsNoTracking();

        if (predicate is not null)
        {
            queryable = queryable.Where(predicate);
        }

        if (includes is not null)
        {
            queryable = includes(queryable);
        }

        var skipAmount = pageSize * (page -1);
        var totalNumberOfRecords = await queryable.CountAsync();

        var records = new List<TEntity>();

        if (string.IsNullOrEmpty(orderBy))
        {
            records = await queryable.Skip(skipAmount).Take(pageSize).ToListAsync();
        }
        else
        {
            records = await queryable
                .OrderBypropertyOrField(orderBy, ascending)
                .Skip(skipAmount)
                .Take(pageSize)
                .ToListAsync();
        }

        var mod = totalNumberOfRecords % pageSize;

        var totalPageCount = (totalNumberOfRecords/pageSize) + (mod == 0 ? 0 : 1);

        return new PagedResults<TEntity, TEntityId>
        {
            Results = records,
            PageNumber = page,
            PageSize = pageSize,
            TotalNumberOfPages = totalPageCount,
            TotalNumberOfRecords = totalNumberOfRecords
        };
    }
    
}