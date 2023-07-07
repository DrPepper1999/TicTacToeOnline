namespace TicTacToeOnline.Application.Authentication.Common
{
    public record RefreshTokenResult(string RefreshToken, DateTime Expires);
}
