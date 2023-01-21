using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using TicTacToeOnline.Domain.GameAggregate.Enums;
using TicTacToeOnline.Domain.RoomAggregate;

namespace TicTacToeOnline.Application.Rooms.Commands.CreateRoom
{
    public record CreateRoomCommand(
        string Name,
        string? Password,
        int? PlayersForStart,
        string PlayerName,
        Mark PlayerMark) : IRequest<ErrorOr<Room>>;
}
