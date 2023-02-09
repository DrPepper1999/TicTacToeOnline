using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TicTacToeOnline.Application.Common.Interfaces.Persistence;
using TicTacToeOnline.Domain.PlayerAggregate;
using TicTacToeOnline.Domain.PlayerAggregate.ValueObjects;

namespace TicTacToeOnline.Infrastructure.Persistence.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly TicTacToeOnlineDbContext _dbContext;

        public PlayerRepository(TicTacToeOnlineDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUnitOfWork UnitOfWork => _dbContext;
        public async Task AddAsync(Player player, CancellationToken cancellationToken = default)
        {
            await _dbContext.AddAsync(player, cancellationToken);
        }

        public async Task<Player?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var entity = await _dbContext.Players
                .FirstOrDefaultAsync(x => x.Id == PlayerId.Create(id.ToString()), cancellationToken);

            return entity;
        }

        public async Task<Player?> GetFirstWhere(Expression<Func<Player, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Players.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<IEnumerable<Player>> GetRangeByIdsAsync(List<string> ids, CancellationToken cancellationToken = default)
        {
            var entities = await _dbContext
                .Players
                .Where(t => ids.Contains(t.Id.Value))
                .ToListAsync(cancellationToken);

            return entities;
        }

        public async Task UpdateAsync(Player player, CancellationToken cancellationToken = default)
        {
            _dbContext.Entry(player).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Player player)
        {
            await Task.CompletedTask;

            _dbContext.Players.Remove(player);
        }
    }
}
