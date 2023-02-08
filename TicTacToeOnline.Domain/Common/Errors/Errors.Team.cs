using ErrorOr;

namespace TicTacToeOnline.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Team
        {
            public static Error NotFound =>
                Error.NotFound(code: "Team.NotFound", description: "Team not found");

            public static Error DifferentMark =>
                Error.Conflict(code: "Team.DifferentMark", description: "Team has a different mark");
        }
    }
}
