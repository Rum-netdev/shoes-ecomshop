using MediatR;

namespace ShoesEShop.Handler.Infrastructures
{
    public interface IQuery<TQueryResult> : IRequest<TQueryResult>
    {
    }

    public interface IQuery : IRequest
    {
    }
}
