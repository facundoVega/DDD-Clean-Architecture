using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Hires;

public static class HireErrors 
{
    public static Error NotFound = new Error(
        "Hire.NotFound",
        "The hire with the specified ID has not been found."
    );

    public static Error NotConfirmed = new Error(
        "Hire.NotConfirmed",
        "The hire has not been confirmed."
    );
    
    public static Error Overlap = new Error(
        "Hire.Overlap",
        "The hire is being taken for two or more clients at the same dates."
    );

    public static Error NotReserved = new Error(
        "Hire.NotReserved",
        "The hire is not reserved."
    );

    public static Error AlreadyStarted = new Error(
        "Hire.AlreadyStarted",
        "The hire has been already started."
    );
}