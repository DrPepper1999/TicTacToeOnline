using Newtonsoft.Json;
using TicTacToeOnline.Domain.Common.Models;

namespace TicTacToeOnline.Domain.Common.ValueObjects
{
    public class GameSetting : ValueObject
    {
        public int MapSize { get; }
        public int MaxPlayers { get; }
        public int TeamCount { get; }

        [JsonConstructor]
        private GameSetting(int mapSize, int maxPlayers, int teamCount)
        {
            MapSize = mapSize;
            MaxPlayers = maxPlayers;
            TeamCount = teamCount;
        }

        public static GameSetting Create(int mapSize = 3, int maxPlayers = 2, int teamCount = 2)
        {
            return new GameSetting(mapSize, maxPlayers, teamCount);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return MapSize;
            yield return MaxPlayers;
            yield return TeamCount;
        }

        private GameSetting()
        {

        }
    }
}
