using TicTacToeOnline.Domain.Common.Models;
using TicTacToeOnline.Domain.PlayerAggregate.ValueObjects;
using TicTacToeOnline.Domain.RoomAggregate.ValueObjects;
using TicTacToeOnline.Domain.UserAggregate.ValueObjects;

namespace TicTacToeOnline.Domain.RoomAggregate.Entities
{
    public class Game : Entity<GameId>
    {
        private readonly List<PlayerId> _playerIds = new();

        public PlayerId PlayerTurn { get; private set; }
        public Map Map { get; private set; }
        public IReadOnlyList<PlayerId> PlayerIds => _playerIds.AsReadOnly(); // _playerIds.ToList().AsReadOnly();

        public DateTime CreatedDateTime { get; private set; }
        public DateTime UpdateDateTime { get; private set; }

        public Game(GameId id, int sizeMap) : base(id)
        {
            Map = Map.Create(sizeMap);
            PlayerTurn = PlayerId.Create(Guid.Empty);
        }

        public static Game Create(int mapSize = 3)
        {
            return new Game(GameId.CreateUnique(), mapSize);
        }

#pragma warning disable CS8618
        private Game()
        {
        }
#pragma warning disable CS8618

        public void SetPlayerTurn(Guid playerId)
        {
            PlayerTurn = PlayerId.Create(playerId);
        }
    }
}
