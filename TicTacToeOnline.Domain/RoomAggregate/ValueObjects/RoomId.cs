using TicTacToeOnline.Domain.Common.Models;

namespace TicTacToeOnline.Domain.RoomAggregate.ValueObjects
{
    public sealed class RoomId : ValueObject
    {
        public Guid Value { get; }

        private RoomId(Guid value)
        {
            Value = value;
        }

        public static RoomId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public static RoomId Create(Guid roomId)
        {
            return new(roomId);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
