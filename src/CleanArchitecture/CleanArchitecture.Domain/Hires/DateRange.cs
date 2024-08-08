namespace CleanArchitecture.Domain.Hires;

public sealed record DateRange 
{
    private DateRange(DateOnly start, DateOnly end)
    {
        Start = start;
        End = end;
    }

    public DateOnly Start { get; init; }
    public DateOnly End { get; init; }

    public int DaysAmount => End.DayNumber - Start.DayNumber;

    public static DateRange Create(DateOnly start, DateOnly end)
    {
        if(start > end)
        {
            throw new ApplicationException("End date  is greater than start date.");
        }

        return new DateRange(start, end);
    }

}