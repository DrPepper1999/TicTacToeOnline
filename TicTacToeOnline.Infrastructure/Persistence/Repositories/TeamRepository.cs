using Microsoft.EntityFrameworkCore;
using TicTacToeOnline.Application.Common.Interfaces.Persistence;
using TicTacToeOnline.Domain.TeamAggregate;
using TicTacToeOnline.Domain.TeamAggregate.ValueObjects;

namespace TicTacToeOnline.Infrastructure.Persistence.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly TicTacToeOnlineDbContext _dbContext;

        public TeamRepository(TicTacToeOnlineDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUnitOfWork UnitOfWork => _dbContext;
        public async Task AddAsync(Team team, CancellationToken cancellationToken = default)
        {
            await _dbContext.AddAsync(team, cancellationToken);
        }

        public async Task<IEnumerable<Team>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Teams.ToListAsync(cancellationToken);
        }

        public async Task<Team?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _dbContext.Teams
                .FirstOrDefaultAsync(x => x.Id == TeamId.Create(id), cancellationToken);

            return entity;
        }

        public async Task<IEnumerable<Team>> GetRangeByIdsAsync(List<Guid> ids, CancellationToken cancellationToken = default)
        {
            var entities = await _dbContext
                .Teams
                .Where(t => ids.Contains(t.Id.Value))
                .ToListAsync(cancellationToken);

            return entities;
        }

        public async Task DeleteAsync(Team team)
        {
            await Task.CompletedTask;

            //_dbContext.Teams.Remove(team);
        }
    }
}
