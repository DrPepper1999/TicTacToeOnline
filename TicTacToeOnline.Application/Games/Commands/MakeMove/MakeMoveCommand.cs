using System.Drawing;
using ErrorOr;
using MediatR;
using TicTacToeOnline.Application.Rooms.Common;
using TicTacToeOnline.Domain.Common.Enums;
using TicTacToeOnline.Domain.GameAggregate.ValueObjects;
using TicTacToeOnline.Domain.TeamAggregate.ValueObjects;

namespace TicTacToeOnline.Application.Games.Commands.MakeMove
{
    public record MakeMoveCommand(Point Move, TeamId TeamId, Mark Mark, GameId GameId) : IRequest<ErrorOr<Move>>;
}
