using TicTacToeOnline.Domain.Common.Models;
using TicTacToeOnline.Domain.GameAggregate.Entities;
using TicTacToeOnline.Domain.GameAggregate.ValueObjects;
using TicTacToeOnline.Domain.PlayerAggregate.ValueObjects;

namespace TicTacToeOnline.Domain.GameAggregate
{
    public class Game : AggregateRoot<GameId>
    {
        public PlayerId PlayerTurn { get; private set; }
        public Map Map { get; private set; }

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
