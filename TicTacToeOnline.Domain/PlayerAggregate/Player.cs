using ErrorOr;
using TicTacToeOnline.Domain.Common.Errors;
using TicTacToeOnline.Domain.Common.Models;
using TicTacToeOnline.Domain.GameAggregate.Enums;
using TicTacToeOnline.Domain.PlayerAggregate.Entities;
using TicTacToeOnline.Domain.PlayerAggregate.ValueObjects;
using TicTacToeOnline.Domain.UserAggregate.ValueObjects;

namespace TicTacToeOnline.Domain.PlayerAggregate
{
    public class Player : AggregateRoot<PlayerId>
    {
        private readonly List<ConnectionInfo> _connections = new();

        public string Name { get; private set; }
        public Mark Mark { get; private set; }
        public int Score { get; private set; }
        public UserId UserId { get; private set; }

        public IReadOnlyList<ConnectionInfo> Connections => _connections.AsReadOnly();

        public DateTime CreatedDateTime { get; private set; }
        public DateTime UpdateDateTime { get; private set; }

        public Player(PlayerId id, UserId userId, string name, Mark mark) : base(id)
        {
            UserId = userId;
            Name = name;
            Mark = mark;
            Score = 0;
        }

        public static ErrorOr<Player> Create(UserId userId, string name, Mark mark)
        {
            if (mark == Mark.Empty) return Errors.Player.MarkCannotBeEmpty;

            return new Player(PlayerId.CreateUnique(), userId, name, mark);
        }

        public Error? SetMark(Mark mark)
        {
            if (mark == Mark.Empty) return Errors.Player.MarkCannotBeEmpty;

            Mark = mark;

            return null;
        }

        public void IncrementScore()
        {
            Score++;
        }

        public void AppendConnection(string connectionId)
        {
            if (string.IsNullOrEmpty(connectionId))
            {
                throw new ArgumentException(nameof(connectionId));
            }

            var connection = ConnectionInfo.Create(DateTime.UtcNow, connectionId);

            _connections.Add(connection);
        }

        /// <summary>
        /// Remove connection from user
        /// </summary>
        public void RemoveConnection(string connectionId)
        {
            if (string.IsNullOrEmpty(connectionId))
            {
                throw new ArgumentException(nameof(connectionId));
            }

            var connection = _connections
                .FirstOrDefault(c => c.ConnectionId.Equals(connectionId));

            if (connection is null)
                return;

            _connections.Remove(connection);

        }
    }
}
