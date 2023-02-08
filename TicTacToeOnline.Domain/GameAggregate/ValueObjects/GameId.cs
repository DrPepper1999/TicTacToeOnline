using Newtonsoft.Json;
using TicTacToeOnline.Domain.Common.Models;

namespace TicTacToeOnline.Domain.GameAggregate.ValueObjects
{
    public sealed class GameId : ValueObject
    {
        public Guid Value { get; }

        [JsonConstructor]
        private GameId(Guid value)
        {
            Value = value;
        }

        public static GameId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public static GameId Create(Guid gameId)
        {
            return new(gameId);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
