using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehicles;

namespace CleanArchitecture.Domain.Vehicles;

public sealed class Vehicle : Entity<VehicleId>
{
    private Vehicle()
    {
        
    }
    public Vehicle(
        VehicleId id,
        Model model,
        Vin vin,
        Currency price,
        Currency maintenance,
        DateTime? lastHireDate,
        List<Appliance> appliances,
        Direction? direction
    ) : base(id)
    {
        Model = model;
        Vin = vin;
        Price = price;
        Maintenance = maintenance;
        LastHireDate = lastHireDate;
        Appliances = appliances;
        Direction = direction;
    }

    public Model? Model { get; private set; }
    public Vin? Vin { get; private set;}
    public Direction? Direction { get; private set;}
    public Currency? Price { get; private set;}
    public Currency? Maintenance { get; private set;}
    public DateTime? LastHireDate { get; internal set; }
    public List<Appliance> Appliances{ get; private set; } = new();
}