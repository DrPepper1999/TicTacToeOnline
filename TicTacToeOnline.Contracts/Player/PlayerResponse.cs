namespace TicTacToeOnline.Contracts.Player
{
    public record PlayerResponse(
        string Id,
        string Name,
        string? Uri,
        float? AverageRating,
        string? UserId,
        List<ConnectionInfoResponse> Connections,
        DateTime CreatedDateTime,
        DateTime UpdateDateTime);

    public record ConnectionInfoResponse(
        DateTime ConnectedAt,
        string ConnectionId);
}
