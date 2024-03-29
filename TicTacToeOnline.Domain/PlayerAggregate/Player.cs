﻿using TicTacToeOnline.Domain.Common.Models;
using TicTacToeOnline.Domain.Common.ValueObjects;
using TicTacToeOnline.Domain.DomainEvents;
using TicTacToeOnline.Domain.PlayerAggregate.ValueObjects;
using TicTacToeOnline.Domain.UserAggregate.ValueObjects;

namespace TicTacToeOnline.Domain.PlayerAggregate
{
    public class Player : AggregateRoot<PlayerId>
    {
        private readonly List<ConnectionInfo> _connections = new();

        public string Name { get; private set; }
        public AverageRating AverageRating { get; private set; }
        public Uri? ProfileImage { get; private set; }
        public UserId? UserId { get; private set; }

        public IReadOnlyList<ConnectionInfo> Connections => _connections.AsReadOnly();

        public DateTime CreatedDateTime { get; private set; }
        public DateTime UpdateDateTime { get; private set; }

        private Player(
            PlayerId id,
            UserId? userId,
            string name,
            Uri? profileImage,
            AverageRating averageRating)
            : base(id)
        {
            UserId = userId;
            Name = name;
            AverageRating = averageRating;
        }

        public static Player Create(UserId? userId, string name, Uri? profileImage = null)
        {
            var playerId = userId is not null ? PlayerId.Create(userId) : PlayerId.CreateUnique();
            return new Player(
                playerId,
                userId, name,
                profileImage,
                AverageRating.CreateNew());
        }

        public void AppendConnection(string connectionId)
        {
            var connectionInfo = ConnectionInfo.Create(connectionId);

            _connections.Add(connectionInfo);
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
