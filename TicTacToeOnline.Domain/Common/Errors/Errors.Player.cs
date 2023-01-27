using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
using TicTacToeOnline.Domain.RoomAggregate.Enums;

namespace TicTacToeOnline.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Player
        {
            public static Error NotFound =>
                Error.NotFound(code: "Player.NotFound", description: "Player not found");

            public static Error MarkCannotBeEmpty =>
                Error.Conflict(code: "Player.MarkCannotBeEmpty",
                    description: $"Mark can take the following values" +
                                 $" {string.Join(' ', Enum.GetNames(typeof(Mark)))}");
        }
    }
}
