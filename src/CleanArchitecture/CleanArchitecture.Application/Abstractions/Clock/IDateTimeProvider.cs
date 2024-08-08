namespace CleanArchitecture.Applications.Abstractions.Clock;

public interface IDateTimeProvider
{
    DateTime currentTime { get; }
}