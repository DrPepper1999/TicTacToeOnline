using TicTacToeOnline.Domain.Common.Models;

namespace TicTacToeOnline.Domain.GameAggregate.ValueObjects
{
    public sealed class GameId : ValueObject
    {
        private Guid Value { get; }

        public GameId(Guid value)
        {
            Value = value;
        }

        public static GameId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
