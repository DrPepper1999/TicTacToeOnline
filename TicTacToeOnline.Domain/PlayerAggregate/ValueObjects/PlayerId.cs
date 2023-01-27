using Newtonsoft.Json;
using TicTacToeOnline.Domain.Common.Models;

namespace TicTacToeOnline.Domain.PlayerAggregate.ValueObjects
{
    public sealed class PlayerId : ValueObject
    {
        public Guid Value { get; }

        [JsonConstructor]
        private PlayerId(Guid value)
        {
            Value = value;
        }

        public static PlayerId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public static PlayerId Create(Guid playerId)
        {
            return new PlayerId(playerId);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
