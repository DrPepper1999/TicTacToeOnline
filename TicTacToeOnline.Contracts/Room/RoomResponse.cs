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
        GameResponse Game,
        DateTime CreatedDateTime,
        DateTime UpdateDateTime
    );

    public record GameResponse(
        string Id,
        string PlayerTurn,
        MapResponse Map,
        List<Guid> PlayerIds,
        DateTime CreatedDateTime,
        DateTime UpdateDateTime
        );

    public record MapResponse(
        string Id,
        string Fields,
        int Size,
        bool IsAllCellFilled,
        DateTime CreatedDateTime,
        DateTime UpdateDateTime
    );
}
