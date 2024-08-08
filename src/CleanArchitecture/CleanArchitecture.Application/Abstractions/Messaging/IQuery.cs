using CleanArchitecture.Domain.Abstractions;
using MediatR;

namespace CleanArchitecture.Applications.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
    
}