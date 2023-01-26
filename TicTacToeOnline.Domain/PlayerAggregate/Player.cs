using ErrorOr;
using TicTacToeOnline.Domain.Common.Errors;
using TicTacToeOnline.Domain.Common.Models;
using TicTacToeOnline.Domain.Common.ValueObjects;
using TicTacToeOnline.Domain.PlayerAggregate.ValueObjects;
using TicTacToeOnline.Domain.RoomAggregate.Enums;
using TicTacToeOnline.Domain.UserAggregate.ValueObjects;

namespace TicTacToeOnline.Domain.PlayerAggregate
{
    public class Player : AggregateRoot<PlayerId>
    {
        private readonly List<ConnectionInfo> _connections = new();

        public string Name { get; private set; }
        public Mark Mark { get; private set; }
        public AverageRating AverageRating { get; private set; }
        public UserId? UserId { get; private set; }

        public IReadOnlyList<ConnectionInfo> Connections => _connections.AsReadOnly();

        public DateTime CreatedDateTime { get; private set; }
        public DateTime UpdateDateTime { get; private set; }

        private Player(
            PlayerId id,
            UserId? userId,
            string name,
            Mark mark,
            AverageRating averageRating)
            : base(id)
        {
            UserId = userId;
            Name = name;
            Mark = mark;
            AverageRating = averageRating;
        }

        public static ErrorOr<Player> Create(UserId? userId, string name)
        {
            return new Player(PlayerId.CreateUnique(), userId, name, Mark.Empty, AverageRating.CreateNew());
        }

        public Error? SetMark(Mark mark)
        {
            if (mark == Mark.Empty) return Errors.Player.MarkCannotBeEmpty;

            Mark = mark;

            return null;
        }

        public void AppendConnection(string connectionId)
        {
            if (string.IsNullOrEmpty(connectionId))
            {
                throw new ArgumentException(nameof(connectionId));
            }

            var connection = ConnectionInfo.Create(connectionId);

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

        #pragma warning disable CS8618
        private Player()
        {
        }
        #pragma warning disable CS8618
    }
}
