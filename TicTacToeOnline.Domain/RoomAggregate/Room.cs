using ErrorOr;
using TicTacToeOnline.Domain.Common.Errors;
using TicTacToeOnline.Domain.Common.Models;
using TicTacToeOnline.Domain.DomainEvents;
using TicTacToeOnline.Domain.PlayerAggregate;
using TicTacToeOnline.Domain.PlayerAggregate.ValueObjects;
using TicTacToeOnline.Domain.RoomAggregate.Entities;
using TicTacToeOnline.Domain.RoomAggregate.Enums;
using TicTacToeOnline.Domain.RoomAggregate.ValueObjects;

namespace TicTacToeOnline.Domain.RoomAggregate
{
    public class Room : AggregateRoot<RoomId>
    {
        public string Name { get; private set; } = null!;
        public string? Password { get; private set; }
        public RoomStatus Status { get; private set; }
        public int CountPlayers => Game.PlayerIds.Count;
        public int PlayersForStart { get; private set; }
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

        public static ErrorOr<Room> Create(
            string name,
            PlayerId playerId,
            int? playerForStart = null,
            string? password = null,
            int mapSize = 3)
        {
            var room = new Room(
                RoomId.CreateUnique(),
                name,
                password,
                playerForStart ?? 2,
                Game.Create(mapSize));

            var addPlayerResult = room.AddPlayer(playerId);

            if (addPlayerResult.HasValue)
            {
                return addPlayerResult!.Value;
            }

            room.RaiseDomainEvent(new RoomCreatedDomainEvent(room.Id));

            return room;
        }

        public Error? AddPlayer(PlayerId playerId)
        {
            if (CountPlayers > PlayersForStart)
            {
                return Errors.Room.LimitPlayers(PlayersForStart);
            }

            Status = RoomStatus.Wait;
            Game.AddPlayer(playerId);

            return null;
        }

        #pragma warning disable CS8618
        private Room()
        {
        }
        #pragma warning disable CS8618
    }
}
