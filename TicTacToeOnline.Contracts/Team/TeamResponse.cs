namespace TicTacToeOnline.Contracts.Team
{
    public record TeamResponse(
        string Id, 
        string Mark,
        int Score,
        List<string> PlayerIds,
        DateTime CreatedDateTime,
        DateTime UpdateDateTime);
}
