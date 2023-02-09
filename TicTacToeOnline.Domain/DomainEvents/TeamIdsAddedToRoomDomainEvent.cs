using TicTacToeOnline.Domain.Common.Interfaces;
using TicTacToeOnline.Domain.RoomAggregate.ValueObjects;
using TicTacToeOnline.Domain.TeamAggregate.ValueObjects;

namespace TicTacToeOnline.Domain.DomainEvents
{
    public record TeamIdsAddedToRoomDomainEvent(Guid Id, RoomId RoomId, IEnumerable<TeamId> TeamIds) : IDomainEvent;
}
