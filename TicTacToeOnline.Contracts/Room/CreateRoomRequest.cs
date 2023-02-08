namespace TicTacToeOnline.Contracts.Room
{
    public record CreateRoomRequest(
        string Name,
        string? Password,
        int MaxPlayers,
        int MapSize,
        int TeamCount
    );
}
