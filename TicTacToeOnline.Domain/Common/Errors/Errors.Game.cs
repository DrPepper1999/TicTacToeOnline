using ErrorOr;

namespace TicTacToeOnline.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Game
        {
            public static Error NotFound =>
                Error.NotFound(code: "Game.NotFound", description: "Game not found");


            public static Error AnotherTeamTurn =>
                Error.Conflict(code: "Game.AnotherTeamTurn", description: "Now it's another team's turn");
        }
    }
}
