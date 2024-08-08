namespace CleanArchitecture.Domain.Shared;

public record CurrencyType
{
    public static readonly CurrencyType None = new("");
    public static readonly CurrencyType Usd = new("USD");
    public static readonly CurrencyType  Eur = new("EUR");

    private CurrencyType(string code) => Code = code;
   
    public string? Code { get; init; }

    public static readonly IReadOnlyCollection<CurrencyType> All = new[]
    {
        Usd, 
        Eur
    };

    public static CurrencyType FromCode(string Code)
    {
        return All.FirstOrDefault(c => c.Code == Code) ?? 
            throw new ApplicationException("The currency type is invalid.");
    } 
}