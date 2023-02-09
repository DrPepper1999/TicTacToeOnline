using ErrorOr;
using MediatR;
using TicTacToeOnline.Domain.RoomAggregate;

namespace TicTacToeOnline.Application.Rooms.Commands.CreateRoom
{
    public record CreateRoomCommand(
        string Name,
        string? Password,
        int MaxPlayers,
        int MapSize,
        int TeamCount,
        string PlayerId) : IRequest<ErrorOr<Room>>;
}
