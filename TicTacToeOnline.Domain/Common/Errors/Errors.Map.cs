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
        public static class Map
        {
            public static Error OutOfBoundsMap =>
                Error.Conflict(code: "Map.OutOfBoundsMap", description: "The point is off the map");
        }
    }
}
