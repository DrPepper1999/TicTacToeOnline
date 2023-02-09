using Newtonsoft.Json;
using TicTacToeOnline.Domain.Common.Models;
using TicTacToeOnline.Domain.UserAggregate.ValueObjects;

namespace TicTacToeOnline.Domain.PlayerAggregate.ValueObjects
{
    public sealed class PlayerId : ValueObject
    {
        public string Value { get; private set; }

        [JsonConstructor]
        private PlayerId(string value)
        {
            Value = value;
        }

        public static PlayerId CreateUnique()
        {
            return new PlayerId(Guid.NewGuid().ToString());
        }

        public static PlayerId Create(UserId userId)
        {
            return new PlayerId($"Player_{userId.Value}");
        }

        public static PlayerId Create(string playerId)
        {
            return new PlayerId(playerId);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
