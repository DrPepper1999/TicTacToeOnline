using ErrorOr;
using MediatR;
using TicTacToeOnline.Application.Authentication.Common;

namespace TicTacToeOnline.Application.Authentication.Queries.GetToken
{
    public record GetTokenByRefreshTokenQuery(string RefreshToken) : IRequest<ErrorOr<TokenResult>>;
}
