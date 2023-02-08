using TicTacToeOnline.Domain.GameAggregate;
using TicTacToeOnline.Domain.GameAggregate.ValueObjects;

namespace TicTacToeOnline.Application.Common.Interfaces.Persistence
{
    public interface IGameRepository : IRepository<Game, GameId>
    {
        Task AddAsync(Game game, CancellationToken cancellationToken = default);

        Task<Game?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task UpdateAsync(Game game, CancellationToken cancellationToken = default);

        Task DeleteAsync(Game game);
    }
}
