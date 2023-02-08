namespace TicTacToeOnline.Application.Common.Interfaces.Services
{
    public interface IDataTimeProvider
    {
        DateTime UtcNow { get; }
        DateTime ExpiryMinutesRefreshToken { get; }
    }
}
