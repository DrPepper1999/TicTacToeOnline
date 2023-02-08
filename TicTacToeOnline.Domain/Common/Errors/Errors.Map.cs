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
