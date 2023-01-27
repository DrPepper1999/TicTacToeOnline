using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<Player?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _dbContext.Players
                .FirstOrDefaultAsync(x => x.Id == PlayerId.Create(id), cancellationToken);

            return entity;
        }

        public async Task<Player?> GetFirstWhere(Expression<Func<Player, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Players.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task DeleteAsync(Player player)
        {
            await Task.CompletedTask;

            _dbContext.Players.Remove(player);
        }
    }
}
