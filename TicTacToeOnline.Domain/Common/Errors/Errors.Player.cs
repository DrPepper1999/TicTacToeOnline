using ErrorOr;

namespace TicTacToeOnline.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Player
        {
            public static Error NotFound =>
                Error.NotFound(code: "Player.NotFound", description: "Player not found");
        }
    }
}
