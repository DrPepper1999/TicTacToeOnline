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

        private Team(TeamId id)
            : base(id)
        {
            Mark = Mark.Empty;
            Score = 0;
        }

        public static Team Create()
        {
            return new Team(TeamId.CreateUnique());
        }

        public void AddPlayer(PlayerId playerId)
        {
            //_playerIds.Add(playerId);
        }

#pragma warning disable CS8618
        private Team()
        {
        }
#pragma warning disable CS8618
    }
}
