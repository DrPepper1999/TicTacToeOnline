using ErrorOr;
using MediatR;
using TicTacToeOnline.Application.Authentication.Common;

namespace TicTacToeOnline.Application.Authentication.Queries.Login
{
    public record LoginQuery(string Email, string Password) : IRequest<ErrorOr<AuthenticationResult>>;
}
