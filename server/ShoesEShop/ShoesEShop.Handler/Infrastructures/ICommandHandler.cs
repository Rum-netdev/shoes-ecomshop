using MediatR;

namespace ShoesEShop.Handler.Infrastructures
{
    public interface ICommandHandler<TCommand, TCommandResult> : IRequestHandler<TCommand, TCommandResult> 
        where TCommand : ICommand<TCommandResult>
    {
    }

    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand>
        where TCommand : ICommand
    {
    }
}
