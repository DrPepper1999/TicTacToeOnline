using Newtonsoft.Json;
using TicTacToeOnline.Domain.Common.Models;

namespace TicTacToeOnline.Domain.RoomAggregate.ValueObjects
{
    public sealed class MapId : ValueObject
    {
        public Guid Value { get; private set; }

        [JsonConstructor]
        private MapId(Guid value)
        {
            Value = value;
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static MapId Create(Guid value)
        {
            return new MapId(value);
        }

        public static MapId CreateUnique()
        {
            return new MapId(Guid.NewGuid());
        }
    }
}
