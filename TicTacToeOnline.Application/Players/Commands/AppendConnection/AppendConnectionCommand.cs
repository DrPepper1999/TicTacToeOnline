using ErrorOr;
using MediatR;

namespace TicTacToeOnline.Application.Players.Commands.AppendConnection
{
    public record AppendConnectionCommand(string PlayerId, string ConnectionId) : IRequest<Error?>;
}
