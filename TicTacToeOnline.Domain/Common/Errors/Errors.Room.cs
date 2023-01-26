using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;

namespace TicTacToeOnline.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Room
        {
            public static Error NotFound =>
                Error.NotFound(code: "Room.NotFound", description: "Room not found");
        }
    }
}
