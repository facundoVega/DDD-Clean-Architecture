using CleanArchitecture.Application.Hires.GetHire;
using CleanArchitecture.Applications.Abstractions.Messaging;

namespace CleanArchitecture.Applications.Hires.GetHire;

public sealed record GetHireQuery(Guid HireId) : IQuery<HireResponse>
{

}