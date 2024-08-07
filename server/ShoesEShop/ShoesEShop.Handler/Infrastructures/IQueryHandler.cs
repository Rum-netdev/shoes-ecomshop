using MediatR;

namespace ShoesEShop.Handler.Infrastructures
{
    public interface IQueryHandler<TQuery, TQueryResult> : IRequestHandler<TQuery, TQueryResult>
     where TQuery : IQuery<TQueryResult>
    {
    }

    public interface IQueryHandler<TQuery> : IRequestHandler<TQuery>
     where TQuery : IQuery
    {
    }
}
