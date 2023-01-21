using TicTacToeOnline.Domain.UserAggregate;

namespace TicTacToeOnline.Application.Authentication.Common
{
    public record AuthenticationResult
    (
        User User,
        string Token
    );
}
