namespace CleanArchitecture.Domain.Abstractions;

public record Error(string Code, string Name )
{
    public static Error None = new(string.Empty, string.Empty);
    public static Error NullValue = new("Eror.NullValue","A null value has been set");

}