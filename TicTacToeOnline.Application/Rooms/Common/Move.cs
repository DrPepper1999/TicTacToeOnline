using System.Drawing;
using TicTacToeOnline.Domain.Common.Enums;
using TicTacToeOnline.Domain.GameAggregate.ValueObjects;
using TicTacToeOnline.Domain.TeamAggregate.ValueObjects;

namespace TicTacToeOnline.Application.Rooms.Common
{
    public record Move(Point PlayerMove, Mark Mark, TeamId TeamId, GameId GameId);
}
