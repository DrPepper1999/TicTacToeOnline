using MediatR;
using TicTacToeOnline.Domain.Common.Interfaces;

namespace TicTacToeOnline.Application.Common.Interfaces.Messaging
{
    public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent>
        where TEvent : IDomainEvent
    {
    }
}
