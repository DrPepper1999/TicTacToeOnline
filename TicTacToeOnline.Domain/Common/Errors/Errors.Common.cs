using ErrorOr;
using TicTacToeOnline.Domain.Common.Enums;

namespace TicTacToeOnline.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Common
        {
            public static Error MarkCannotBeEmpty =>
                Error.Conflict(code: "Player.MarkCannotBeEmpty",
                    description: $"Mark can take the following values" +
                                 $" {string.Join(' ', Enum.GetNames(typeof(Mark)))}");
        }
    }
}
