using CleanArchitecture.Domain.Hires;
using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Configurations;

internal sealed class HireConfiguration : IEntityTypeConfiguration<Hire>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Hire> builder)
    {
        builder.ToTable("hires");
        builder.HasKey(hire => hire.Id);

        builder.OwnsOne(hire => hire.PeriodPrice, priceBuilder => {
            priceBuilder.Property(currency => currency.CurrencyType)
            .HasConversion(currencyType => currencyType.Code, code => CurrencyType.FromCode(code!));
        });

        builder.OwnsOne(hire => hire.Maintenance, priceBuilder => {
            priceBuilder.Property(currency => currency.CurrencyType)
            .HasConversion(currencyType => currencyType.Code, code => CurrencyType.FromCode(code!));
        });

        builder.OwnsOne(hire => hire.Appliances, priceBuilder => {
            priceBuilder.Property(currency => currency.CurrencyType)
            .HasConversion(currencyType => currencyType.Code, code => CurrencyType.FromCode(code!));
        });
        
        builder.OwnsOne(hire => hire.TotalPrice, priceBuilder => {
            priceBuilder.Property(currency => currency.CurrencyType)
            .HasConversion(currencyType => currencyType.Code, code => CurrencyType.FromCode(code!));
        });

        builder.OwnsOne(hire => hire.Duration);

        builder.HasOne<Vehicle>()
            .WithMany()
            .HasForeignKey(hire => hire.VehicleId);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(hire => hire.UserId);
    }
}