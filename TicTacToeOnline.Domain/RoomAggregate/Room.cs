﻿using ErrorOr;
using TicTacToeOnline.Domain.Common.Errors;
using TicTacToeOnline.Domain.Common.Models;
using TicTacToeOnline.Domain.Common.ValueObjects;
using TicTacToeOnline.Domain.DomainEvents;
using TicTacToeOnline.Domain.PlayerAggregate.ValueObjects;
using TicTacToeOnline.Domain.RoomAggregate.Enums;
using TicTacToeOnline.Domain.RoomAggregate.ValueObjects;
using TicTacToeOnline.Domain.TeamAggregate.ValueObjects;

namespace TicTacToeOnline.Domain.RoomAggregate
{
    public class Room : AggregateRoot<RoomId>
    {
        private readonly List<TeamId> _teamIds = new();
        private readonly List<PlayerId> _playerIds = new();

        public string Name { get; private set; } = null!;
        public string? Password { get; private set; }
        public RoomStatus Status { get; private set; }
        public GameSetting GameSetting { get; private set; }
        public IReadOnlyList<TeamId> TeamIds => _teamIds.AsReadOnly();
        public IReadOnlyList<PlayerId> PlayerIds => _playerIds.AsReadOnly();
        public int CountPlayers => _playerIds.Count;



        private Room(RoomId id, string name, string? password, GameSetting gameSetting)
            : base(id)
        {
            Name = name;
            Password = password;
            Status = RoomStatus.Init;
            GameSetting = gameSetting;
        }

        public static Room Create(
            string name,
            PlayerId playerId,
            GameSetting gameSetting,
            string? password = null)
        {
            var room = new Room(
                RoomId.CreateUnique(),
                name,
                password,
                gameSetting);

            room.AddPlayer(playerId);

            room.RaiseDomainEvent(new RoomCreatedDomainEvent(Guid.NewGuid(), room.Id));

            return room;
        }

        public void AddTeamId(TeamId teamId)
        {
            _teamIds.Add(teamId);
        }

        public Error? AddPlayer(PlayerId playerId)
        {
            if (CountPlayers > GameSetting.MaxPlayers)
            {
                return Errors.Room.LimitPlayers(GameSetting.MaxPlayers);
            }

            _playerIds.Add(playerId);


            if (CountPlayers == 1)
            {
                Status = RoomStatus.Wait;
            }

            return null;
        }

#pragma warning disable CS8618
        private Room()
        {
        }
        #pragma warning disable CS8618
    }
}
