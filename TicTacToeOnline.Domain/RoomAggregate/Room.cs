﻿using TicTacToeOnline.Domain.Common.Models;
using TicTacToeOnline.Domain.GameAggregate;
using TicTacToeOnline.Domain.GameAggregate.ValueObjects;
using TicTacToeOnline.Domain.PlayerAggregate.ValueObjects;
using TicTacToeOnline.Domain.RoomAggregate.Enums;
using TicTacToeOnline.Domain.RoomAggregate.ValueObjects;

namespace TicTacToeOnline.Domain.RoomAggregate
{
    public class Room : AggregateRoot<RoomId>
    {
        private readonly List<PlayerId> _playerIds = new();

        public string Name { get; private set; } = null!;
        public string? Password { get; private set; }
        public RoomStatus Status { get; private set; }
        public int PlayersForStart { get; private set; }
        public IReadOnlyList<PlayerId> PlayerIds => _playerIds.AsReadOnly(); // _playerIds.ToList().AsReadOnly();
        public Game Game { get; private set; }

        public DateTime CreatedDateTime { get; private set; }
        public DateTime UpdateDateTime { get; private set; }

        private Room(RoomId id, string name, string? password, int playerForStart, Game game)
            : base(id)
        {
            Name = name;
            Password = password;
            PlayersForStart = playerForStart;
            Status = RoomStatus.Init;
            Game = game;
        }

        public static Room Create(
            string name,
            int? playerForStart = null,
            string? password = null,
            int mapSize = 3)
        {
            return new Room(
                RoomId.CreateUnique(),
                name,
                password,
                playerForStart ?? 2,
                Game.Create(mapSize));
        }

        #pragma warning disable CS8618
        private Room()
        {
        }
        #pragma warning disable CS8618
    }
}