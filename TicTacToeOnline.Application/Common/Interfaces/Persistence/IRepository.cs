using TicTacToeOnline.Domain.Common.Models;

namespace TicTacToeOnline.Application.Common.Interfaces.Persistence
{
    public interface IRepository<T, TId>
        where T : AggregateRoot<TId>
        where TId : ValueObject
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
