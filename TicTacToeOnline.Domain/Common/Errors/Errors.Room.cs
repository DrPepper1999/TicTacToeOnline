using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
using TicTacToeOnline.Domain.RoomAggregate.Enums;

namespace TicTacToeOnline.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Room
        {
            public static Error NotFound =>
                Error.NotFound(code: "Room.NotFound", description: "Room not found");

            public static Error LimitPlayers (int maxPlayers)=>
                Error.Conflict(code: "Room.LimitPlayers", description: $"players cannot be more than {maxPlayers}");

            public static Error TeamCannotBeNone =>
                Error.Conflict(code: "Player.TeamCannotBeNone",
                    description: $"Team can take the following values" +
                                 $" {string.Join(' ', Enum.GetNames(typeof(Team)))}");
        }
    }
}
