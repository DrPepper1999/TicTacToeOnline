using Newtonsoft.Json;
using TicTacToeOnline.Domain.Common.Models;

namespace TicTacToeOnline.Domain.UserAggregate.ValueObjects
{
    public sealed class UserId : ValueObject
    {
        [JsonConstructor]
        private UserId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

        public static UserId CreateUnique()
        {
            return new UserId(Guid.NewGuid());
        }

        public static UserId Create(Guid userId)
        {
            return new UserId(userId);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
