using TicTacToeOnline.Domain.Common.Interfaces;
using TicTacToeOnline.Domain.PlayerAggregate;

namespace TicTacToeOnline.Domain.DomainEvents
{
    public sealed record PlayerConnectionAppendDomainEvent(Guid Id, Player Player) : IDomainEvent;
}
