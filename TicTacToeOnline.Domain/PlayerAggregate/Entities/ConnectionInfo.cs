using TicTacToeOnline.Domain.Common.Models;
using TicTacToeOnline.Domain.PlayerAggregate.ValueObjects;

namespace TicTacToeOnline.Domain.PlayerAggregate.Entities
{
    public class ConnectionInfo : Entity<ConnectionInfoId>
    {
        /// <summary>
        /// Registered at time
        /// </summary>
        public DateTime ConnectedAt { get; private set; }

        /// <summary>
        /// Connection Id from client
        /// </summary>
        public string ConnectionId { get; private set; } = null!;

        private ConnectionInfo(ConnectionInfoId id, DateTime connectedAt, string connectionId) : base(id)
        {
            ConnectedAt = connectedAt;
            ConnectionId = connectionId;
        }

        public static ConnectionInfo Create(DateTime connectedAt, string connectionId)
        {
            return new ConnectionInfo(ConnectionInfoId.CreateUnique(), connectedAt, connectionId);
        }
    }
}
