namespace TicTacToeOnline.Contracts.Room
{
    public record MoveRequest(int X, int Y, string TeamId, string Mark, string GameId);
}
