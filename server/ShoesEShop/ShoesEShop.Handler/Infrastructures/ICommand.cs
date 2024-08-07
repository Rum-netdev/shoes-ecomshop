using MediatR;

namespace ShoesEShop.Handler.Infrastructures
{
    public interface ICommand<TCommandResult> : IRequest<TCommandResult> 
    {
    }

    public interface ICommand : IRequest
    {
    }
}
