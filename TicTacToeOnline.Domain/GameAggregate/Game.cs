using System.Drawing;
using ErrorOr;
using TicTacToeOnline.Domain.Common.Enums;
using TicTacToeOnline.Domain.Common.Errors;
using TicTacToeOnline.Domain.Common.Models;
using TicTacToeOnline.Domain.GameAggregate.Entities;
using TicTacToeOnline.Domain.GameAggregate.Enums;
using TicTacToeOnline.Domain.GameAggregate.ValueObjects;
using TicTacToeOnline.Domain.RoomAggregate.ValueObjects;
using TicTacToeOnline.Domain.TeamAggregate.ValueObjects;

namespace TicTacToeOnline.Domain.GameAggregate
{
    public class Game : AggregateRoot<GameId>
    {
        private readonly List<TeamId> _teamIds = new();

        public TeamId PlayerTurn { get; private set; }
        public Map Map { get; private set; }
        public IReadOnlyList<TeamId> TeamIds => _teamIds.AsReadOnly();
        public RoomId RoomId { get; set; }

        private Mark _currentMarkMove = Mark.Cross;

        public DateTime CreatedDateTime { get; private set; }
        public DateTime UpdateDateTime { get; private set; }

        public Game(GameId id, int sizeMap, int teamCount, RoomId roomId) : base(id)
        {
            Map = Map.Create(sizeMap);
            PlayerTurn = TeamId.Create(Guid.Empty);
            RoomId = roomId;
            _currentMarkMove = GetNextMarkMove(_currentMarkMove);

        }

        public static Game Create(RoomId roomId, int mapSize = 3, int teamCount = 2)
        {
            return new Game(GameId.CreateUnique(), mapSize, teamCount, roomId);
        }

        public Error? MakeMove(Point move, Mark mark)
        {
            if (mark != _currentMarkMove)
            {
                return Errors.Team.DifferentMark;
            }

            Map.SetField(move, mark);

            _currentMarkMove = GetNextMarkMove(_currentMarkMove);

            return null;

            // TODO Менять PlayerTurn
        }

        public void SetPlayerTurn(TeamId teamId)
        {
            PlayerTurn = teamId;
        }

        public void AddTeamId(TeamId teamId)
        {
            _teamIds.Add(teamId);
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
                if (Map[x0 + i * dx, y0 + i * dy] != mark)
                    return false;
            }
            return true;
        }

        private Mark GetNextMarkMove(Mark mark)
        {
            if ((int)mark >= Enum.GetValues<Mark>().Length - 1)
            {
                return (Mark)(1);
            }

            var nextMark = (Mark)((int)mark + 1);

            if (nextMark == Mark.Empty)
            {
                nextMark = GetNextMarkMove(nextMark);
            }

            return nextMark;
        }

#pragma warning disable CS8618
        private Game()
        {
        }
#pragma warning disable CS8618
    }
}
