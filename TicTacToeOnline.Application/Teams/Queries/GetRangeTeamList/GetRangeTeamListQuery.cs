using MediatR;
using TicTacToeOnline.Domain.TeamAggregate;

namespace TicTacToeOnline.Application.Teams.Queries.GetRangeTeamList
{
    public record GetRangeTeamListQuery(IEnumerable<Guid> TeamIds) : IRequest<IEnumerable<Team>>;
}
