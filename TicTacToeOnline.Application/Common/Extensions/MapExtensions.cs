using System.Text.Json;
using TicTacToeOnline.Domain.RoomAggregate.Enums;

namespace TicTacToeOnline.Application.Common.Extensions
{
    public static class MapExtensions
    {
        public static IEnumerable<string> ConvertToString(this Mark[,] Map)
        {
            return Map.Cast<Mark>().Select(x => x.ToString());
        }

        public static Mark[,] ConvertStringFieldToMap(this string field)
        {
            return MarkMarkArray(JsonSerializer
                .Deserialize<string[]>(field, (JsonSerializerOptions)null!)!
                .Select(x => (Mark)Enum.Parse(typeof(Mark), x))
                .ToArray());
        }

        private static Mark[,] MarkMarkArray(Mark[] marks)
        {
            var result = new Mark[3, 3];

            var c = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    result[i, j] = marks[j + c];
                }

                c += 3;
            }

            return result;
        }
    }
}
