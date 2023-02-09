using ErrorOr;
using MediatR;
using TicTacToeOnline.Domain.TeamAggregate;

namespace TicTacToeOnline.Application.Rooms.Queries.GetTeams
{
    public record GetRoomTeamsQuery(Guid RoomId) : IRequest<ErrorOr<IEnumerable<Team>>>;
}
