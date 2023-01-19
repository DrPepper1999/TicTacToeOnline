using TicTacToeOnline.Domain.Common.Models;
using TicTacToeOnline.Domain.GameAggregate.ValueObjects;
using TicTacToeOnline.Domain.RoomAggregate.Enums;
using TicTacToeOnline.Domain.RoomAggregate.ValueObjects;

namespace TicTacToeOnline.Domain.RoomAggregate
{
    public class Room : AggregateRoot<RoomId>
    {
        public GameId GameId { get; private set; }
        public string Name { get; private set; } = null!;
        public string? Password { get; private set; }
        public RoomStatus Status { get; private set; }
        public int PlayersForStart { get; private set; } = 2;

        public DateTime CreatedDateTime { get; private set; }
        public DateTime UpdateDateTime { get; private set; }

        private Room(RoomId id, GameId gameId, string name, string? password, int playerForStart) : base(id)
        {
            Name = name;
            Password = password;
            PlayersForStart = playerForStart;
            Status = RoomStatus.Init;
            GameId = gameId;
        }

        public static Room Create(
            string name,
            GameId gameId,
            string? password = null,
            int playerForStart = 2)
        {
            return new Room(RoomId.CreateUnique(), gameId, name, password, playerForStart);
        }
    }
}
