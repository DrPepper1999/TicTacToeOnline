using MediatR;
using TicTacToeOnline.Domain.TeamAggregate;

namespace TicTacToeOnline.Application.Teams.Queries.GetTeamList
{
    public record GetTeamListQuery() : IRequest<IEnumerable<Team>>;
}
