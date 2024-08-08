using CleanArchitecture.Domain.Hires;
using CleanArchitecture.Domain.Vehicles;


public interface IHireRepository 
{
    Task<Hire?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> IsOverlappingAsync(
        Vehicle vehicle,
        DateRange duration,
        CancellationToken cancellationToken = default);
    
    void Add(Hire hire);
}