﻿using Newtonsoft.Json;
using TicTacToeOnline.Domain.Common.Models;

namespace TicTacToeOnline.Domain.PlayerAggregate.ValueObjects
{
    public sealed class ConnectionInfo : ValueObject
    {
        /// <summary>
        /// Registered at time
        /// </summary>
        public DateTime ConnectedAt { get; private set; }

        /// <summary>
        /// Connection Id from client
        /// </summary>
        public string ConnectionId { get; private set; } = null!;

        [JsonConstructor]
        private ConnectionInfo(string connectionId)
        {
            ConnectionId = connectionId;
            ConnectedAt = DateTime.UtcNow;
        }

        public static ConnectionInfo Create(string connectionId)
        {
            return new ConnectionInfo(connectionId);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return ConnectionId;
            yield return ConnectedAt;
        }
    }
}
