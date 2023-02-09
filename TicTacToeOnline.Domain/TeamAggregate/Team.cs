using TicTacToeOnline.Domain.Common.Enums;
using TicTacToeOnline.Domain.Common.Models;
using TicTacToeOnline.Domain.PlayerAggregate.ValueObjects;
using TicTacToeOnline.Domain.TeamAggregate.ValueObjects;

namespace TicTacToeOnline.Domain.TeamAggregate
{
    public class Team : AggregateRoot<TeamId>
    {
        private readonly List<PlayerId> _playerIds = new();

        public Mark Mark { get; private set; }
        public int Score { get; private set; }
        public IReadOnlyList<PlayerId> PlayerIds => _playerIds.AsReadOnly();

        public DateTime CreatedDateTime { get; private set; }
        public DateTime UpdateDateTime { get; private set; }

        private Team(TeamId id, Mark mark)
            : base(id)
        {
            Mark = mark;
            Score = 0;
        }

        public static Team Create(Mark mark, TeamId? teamId = null)
        {
            if (mark == Mark.Empty)
            {
                // error
            }

            return new Team(teamId ?? TeamId.CreateUnique(), mark);
        }

        public void AddPlayer(PlayerId playerId)
        {
            _playerIds.Add(playerId);
        }

#pragma warning disable CS8618
        private Team()
        {
        }
#pragma warning disable CS8618
    }
}
