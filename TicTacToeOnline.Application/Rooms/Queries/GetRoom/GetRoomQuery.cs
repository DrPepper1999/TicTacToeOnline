using ErrorOr;
using MediatR;
using TicTacToeOnline.Domain.RoomAggregate;

namespace TicTacToeOnline.Application.Rooms.Queries.GetRoom
{
    public record GetRoomQuery(Guid Id) : IRequest<ErrorOr<Room>>;
}
