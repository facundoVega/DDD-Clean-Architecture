using CleanArchitecture.Domain.Hires;
using CleanArchitecture.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories;

internal sealed class HireRepository : Repository<Hire, HireId>, IHireRepository
{
    private static readonly HireStatus[] ActiveHireStatuses = {
        HireStatus.Reserved,
        HireStatus.Confirmed,
        HireStatus.Completed
    };
    public HireRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> IsOverlappingAsync(
        Vehicle vehicle, 
        DateRange duration, 
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Hire>()
        .AnyAsync(
            hire => 
                hire.VehicleId == vehicle.Id &&
                hire.Duration!.Start <= duration.End &&
                hire.Duration.End >= duration.Start &&
                ActiveHireStatuses.Contains(hire.Status),
            cancellationToken
        );
    }
    
}