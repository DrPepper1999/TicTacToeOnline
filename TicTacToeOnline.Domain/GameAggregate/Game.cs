using TicTacToeOnline.Domain.Common.Models;
using TicTacToeOnline.Domain.GameAggregate.Entities;
using TicTacToeOnline.Domain.GameAggregate.ValueObjects;
using TicTacToeOnline.Domain.PlayerAggregate.ValueObjects;

namespace TicTacToeOnline.Domain.GameAggregate
{
    public class Game : AggregateRoot<GameId>
    {
        private readonly List<PlayerId> _playerIds = new();

        public IReadOnlyList<PlayerId> PlayersIds => _playerIds.AsReadOnly(); // _playerIds.ToList().AsReadOnly();
        public PlayerId PlayerTurn { get; private set; }
        public Map Map { get; private set; }

        public DateTime CreatedDateTime { get; private set; }
        public DateTime UpdateDateTime { get; private set; }

        public Game(GameId id, int sizeMap) : base(id)
        {
            Map = Map.Create(sizeMap);
            PlayerTurn = PlayerId.Create(Guid.Empty);
        }

        public static Game Create(int sizeMap = 3)
        {
            return new Game(GameId.CreateUnique(), sizeMap);
        }

        public void SetPlayerTurn(Guid playerId)
        {
            PlayerTurn = PlayerId.Create(playerId);
        }
    }
}
