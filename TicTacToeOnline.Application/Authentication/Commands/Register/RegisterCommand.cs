using ErrorOr;
using MediatR;
using TicTacToeOnline.Application.Authentication.Common;

namespace TicTacToeOnline.Application.Authentication.Commands.Register
{
    public record RegisterCommand(string Name, string Email, string Password)
        : IRequest<ErrorOr<AuthenticationResult>>;
}
