using MediatR;

namespace Enquizitive.Common;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
    
}

public interface ICommand : IRequest
{
    
}