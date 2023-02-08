using TicTacToeOnline.Domain.UserAggregate.ValueObjects;

namespace TicTacToeOnline.Application.Authentication.Common
{
    public record TokenResult(string Token, UserId UserId);
}
