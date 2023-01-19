using TicTacToeOnline.Domain.Common.Models;

namespace TicTacToeOnline.Domain.RoomAggregate.ValueObjects
{
    public sealed class RoomId : ValueObject
    {
        private Guid Value { get; }

        public RoomId(Guid value)
        {
            Value = value;
        }

        public static RoomId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
