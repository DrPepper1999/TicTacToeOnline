using ErrorOr;
using MediatR;
using TicTacToeOnline.Domain.PlayerAggregate.ValueObjects;
using TicTacToeOnline.Domain.RoomAggregate;

namespace TicTacToeOnline.Application.Rooms.Commands.CreateRoom
{
    public record CreateRoomCommand(
        string Name,
        string? Password,
        int MaxPlayers,
        int MapSize,
        int TeamCount,
        Guid PlayerId) : IRequest<ErrorOr<Room>>;
}
