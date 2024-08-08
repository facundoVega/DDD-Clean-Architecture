using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehicles;
using CleanArchitecture.Domain.Vehicles;

namespace CleanArchitecture.Domain.Hires;

public class PriceService 
{
    public DetailPrice CalculatePrice(Vehicle vehicle, DateRange period)
    {
        var currencyType = vehicle.Price!.CurrencyType;

        var pricePerPeriod = new Currency(  
            period.DaysAmount * vehicle.Price.Amount ,
            currencyType);

        decimal percentageChange = 0;

        foreach(var appliance in vehicle.Appliances)
        {
            percentageChange += appliance switch 
            {
                Appliance.AppleCar or Appliance.AndroidCar => 0.05m,
                Appliance.AirConditioning => 0.01m,
                Appliance.Maps => 0.01m,
                _ => 0
            };
        }

        var applianceCharges = Currency.Zero(currencyType);

        if(percentageChange  > 0)
        {
            applianceCharges = new Currency(
                pricePerPeriod.Amount * percentageChange,
                currencyType
            );
        }
        
        var totalPrice = Currency.Zero(currencyType);
        totalPrice = pricePerPeriod;

        if(!vehicle.Maintenance!.IsZero())
        {
            totalPrice += vehicle.Maintenance;
        }

        totalPrice += applianceCharges;

        return new DetailPrice(
            pricePerPeriod, 
            vehicle.Maintenance, 
            applianceCharges,
            totalPrice);
    }
}