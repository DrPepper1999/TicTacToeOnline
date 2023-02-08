namespace TicTacToeOnline.Infrastructure.Authentication.RefreshToken
{
    public class RefreshTokenSettings
    {
        public const string SectionName = "RefreshTokenSettings";
        public int ExpiryMinutes { get; init; }
    }
}
