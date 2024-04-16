using MediatR;

namespace Enquizitive.Common;

/// <summary>
/// Command handler interface for a command that returns a response
/// </summary>
/// <typeparam name="TCommand"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public interface ICommandHandler<in TCommand, TResponse>: IRequestHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
{
    
}

/// <summary>
/// Command handler interface for a command that does not return a response
/// </summary>
/// <typeparam name="TCommand"></typeparam>
public interface ICommandHandler<in TCommand>: IRequestHandler<TCommand> where TCommand : ICommand
{
    
}