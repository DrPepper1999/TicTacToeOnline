using Newtonsoft.Json;
using TicTacToeOnline.Domain.Common.Models;

namespace TicTacToeOnline.Domain.TeamAggregate.ValueObjects
{
    public class TeamId : ValueObject
    {
        public Guid Value { get; }

        [JsonConstructor]
        private TeamId(Guid value)
        {
            Value = value;
        }

        public static TeamId CreateUnique()
        {
            return new TeamId(Guid.NewGuid());
        }

        public static TeamId Create(Guid teamId)
        {
            return new TeamId(teamId);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
