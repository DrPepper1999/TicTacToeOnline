namespace TicTacToeOnline.Contracts.Room
{
    public record RoomResponse(
        string Id,
        string Name,
        string Status,
        int MapSize,
        int MaxPlayers,
        int TeamCount,
        List<string> TeamIds,
        List<string> PlayerIds,
        DateTime CreatedDateTime,
        DateTime UpdateDateTime
    );
}
