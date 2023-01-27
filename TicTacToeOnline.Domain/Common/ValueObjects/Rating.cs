using Newtonsoft.Json;
using TicTacToeOnline.Domain.Common.Models;

namespace TicTacToeOnline.Domain.Common.ValueObjects
{
    public sealed class Rating : ValueObject
    {
        [JsonConstructor]
        private Rating(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public static Rating Create(int value)
        {
            // TODO: Enforce invariants
            return new Rating(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
