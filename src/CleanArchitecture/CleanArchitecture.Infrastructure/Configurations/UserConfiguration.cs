using CleanArchitecture.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(user => user.Id );

        builder.Property(user => user.Name)
        .HasMaxLength(200)
        .HasConversion(name => name!.value, value => new Name(value));

        builder.Property(user => user.LastName)
        .HasMaxLength(200)
        .HasConversion(lastname => lastname!.value, value => new LastName(value));

        builder.Property(user => user.Email)
        .HasMaxLength(400)
        .HasConversion(email => email!.value, value => new Email(value));

        builder.HasIndex(user => user.Email).IsUnique();
    }
}