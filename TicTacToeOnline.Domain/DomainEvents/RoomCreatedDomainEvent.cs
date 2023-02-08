using TicTacToeOnline.Domain.Common.Interfaces;
using TicTacToeOnline.Domain.RoomAggregate.ValueObjects;

namespace TicTacToeOnline.Domain.DomainEvents
{
    public sealed record RoomCreatedDomainEvent(Guid Id, RoomId RoomId) : IDomainEvent;
}
