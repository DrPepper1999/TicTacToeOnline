using TicTacToeOnline.Domain.Common.Models;

namespace TicTacToeOnline.Domain.PlayerAggregate.ValueObjects
{
    public class ConnectionInfoId : ValueObject
    {
        public Guid Value { get; private set; }

        private ConnectionInfoId(Guid value)
        {
            Value = value;
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static ConnectionInfoId Create(Guid value)
        {
            return new ConnectionInfoId(value);
        }

        public static ConnectionInfoId CreateUnique()
        {
            return new ConnectionInfoId(Guid.NewGuid());
        }
    }
}
