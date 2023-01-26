using System.Drawing;
using ErrorOr;
using TicTacToeOnline.Domain.Common.Models;
using TicTacToeOnline.Domain.PlayerAggregate.ValueObjects;
using TicTacToeOnline.Domain.RoomAggregate.Enums;
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

        public Game(GameId id, PlayerId playerId, int sizeMap) : base(id)
        {
            Map = Map.Create(sizeMap);
            PlayerTurn = PlayerId.Create(Guid.Empty);
            AddPlayer(playerId);
        }

        public static Game Create(PlayerId playerId, int mapSize = 3)
        {
            return new Game(GameId.CreateUnique(), playerId, mapSize);
        }

        public void SetPlayerTurn(Guid playerId)
        {
            PlayerTurn = PlayerId.Create(playerId);
        }

        public Error? AddPlayer(PlayerId playerId)
        {
            _playerIds.Add(playerId);
            // DomainEvent
            return null;
        }

        public GameResult? TryGetGameResult(Mark mark, Point move)
        {
            var isWin = HasWinSequence(mark, move);
            if (isWin)
            {
                if (mark == Mark.Cross)
                    return GameResult.CrossWin;
                if (mark == Mark.Circle)
                    return GameResult.CircleWin;
            }

            if (Map.IsAllCellFilled) return GameResult.Draw;

            return null;
        }

        private bool HasWinSequence(Mark mark, Point move)
        {
            return IsLine(mark, 0, move.Y, 1, 0)
                   || IsLine(mark, move.X, 0, 0, 1)
                   || IsLine(mark, move.X, 0, 0, 1)
                   || IsLine(mark, 0, 0, 1, 1)
                   || IsLine(mark, 0, Map.Size - 1, 1, -1);
        }

        private bool IsLine(Mark mark, int x0, int y0, int dx, int dy)
        {
            for (int i = 0; i < Map.Size; i++)
            {
                if (Map[x0 + (i * dx), y0 + (i * dy)] != mark)
                    return false;
            }
            return true;
        }

#pragma warning disable CS8618
        private Game()
        {
        }
        #pragma warning disable CS8618
    }
}
