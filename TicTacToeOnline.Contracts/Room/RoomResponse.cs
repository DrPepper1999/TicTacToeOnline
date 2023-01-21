using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeOnline.Contracts.Room
{
    public record RoomResponse(
        string Id,
        string Name,
        string Status,
        int PlayersForStart,
        string GameId,
        DateTime CreatedDateTime,
        DateTime UpdateDateTime
    );
}
