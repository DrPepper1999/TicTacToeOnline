using ErrorOr;
using MediatR;
using TicTacToeOnline.Application.Authentication.Common;
using TicTacToeOnline.Domain.UserAggregate.ValueObjects;

namespace TicTacToeOnline.Application.Authentication.Commands.CreateRefreshToken
{
    public record CreateRefreshTokenCommand(UserId UserId) : IRequest<ErrorOr<RefreshTokenResult>>;
}
