using TicTacToeOnline.Domain.Common.Interfaces;
using TicTacToeOnline.Domain.UserAggregate;

namespace TicTacToeOnline.Domain.DomainEvents
{
    public record RefreshTokenSetDomainEvent(Guid Id, User User) : IDomainEvent;
}
