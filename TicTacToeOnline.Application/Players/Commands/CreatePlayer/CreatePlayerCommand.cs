using ErrorOr;
using MediatR;
using TicTacToeOnline.Domain.PlayerAggregate;
using TicTacToeOnline.Domain.UserAggregate.ValueObjects;

namespace TicTacToeOnline.Application.Players.Commands.CreatePlayer
{
    public record CreatePlayerCommand(string Name, string ConnectionId, UserId? UserId) : IRequest<ErrorOr<Player>>;
}
