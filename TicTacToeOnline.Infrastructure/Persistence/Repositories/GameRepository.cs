using Microsoft.EntityFrameworkCore;
using TicTacToeOnline.Application.Common.Interfaces.Persistence;
using TicTacToeOnline.Domain.GameAggregate;
using TicTacToeOnline.Domain.RoomAggregate.ValueObjects;

namespace TicTacToeOnline.Infrastructure.Persistence.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly TicTacToeOnlineDbContext _dbContext;

        public GameRepository(TicTacToeOnlineDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUnitOfWork UnitOfWork => _dbContext;
        public async Task AddAsync(Game game, CancellationToken cancellationToken = default)
        {
            await _dbContext.AddAsync(game, cancellationToken);
        }

        public async Task<Game?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _dbContext.Games
                .FirstOrDefaultAsync(x => x.Id == RoomId.Create(id), cancellationToken);

            return entity;
        }

        public async Task UpdateAsync(Game game, CancellationToken cancellationToken = default)
        {
            _dbContext.Entry(game).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Game game)
        {
            await Task.CompletedTask;

            _dbContext.Games.Remove(game);
        }
    }
}
