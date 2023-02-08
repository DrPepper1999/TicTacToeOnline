using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeOnline.Contracts.Player
{
    public record PlayerResponse(
        string Id,
        string Name,
        float? AverageRating,
        string? UserId,
        List<ConnectionInfoResponse> Connections,
        DateTime CreatedDateTime,
        DateTime UpdateDateTime);

    public record ConnectionInfoResponse(
        DateTime ConnectedAt,
        string ConnectionId);
}
