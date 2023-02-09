using TicTacToeOnline.Domain.RoomAggregate.ValueObjects;
using TicTacToeOnline.Domain.TeamAggregate;
using TicTacToeOnline.Domain.TeamAggregate.ValueObjects;

namespace TicTacToeOnline.Application.Common.Interfaces.Persistence
{
    public interface ITeamRepository : IRepository<Team, TeamId>
    {
        Task AddAsync(Team team, CancellationToken cancellationToken = default);

        Task<IEnumerable<Team>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Team?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<Team>> GetRangeByIdsAsync(List<TeamId> ids, CancellationToken cancellationToken = default);

        Task DeleteAsync(Team team);
    }
}
