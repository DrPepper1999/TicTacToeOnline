using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using TicTacToeOnline.Domain.PlayerAggregate;
using TicTacToeOnline.Domain.RoomAggregate.Enums;

namespace TicTacToeOnline.Application.Players.Commands.CreatePlayer
{
    public record CreatePlayerCommand(string Name, string ConnectionId) : IRequest<ErrorOr<Player>>;
}
