namespace CleanArchitecture.Domain.Hires;

public record HireId(Guid Value)
{
    public static HireId New() => new (Guid.NewGuid());
}