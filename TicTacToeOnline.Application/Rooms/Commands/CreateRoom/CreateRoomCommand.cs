using ErrorOr;
using MediatR;
using TicTacToeOnline.Domain.PlayerAggregate.ValueObjects;
using TicTacToeOnline.Domain.RoomAggregate;
using TicTacToeOnline.Domain.RoomAggregate.Enums;

namespace TicTacToeOnline.Application.Rooms.Commands.CreateRoom
{
    public record CreateRoomCommand(
        string Name,
        string? Password,
        int PlayersForStart,
        int MapSize,
        PlayerId PlayerId) : IRequest<ErrorOr<Room>>;
}
