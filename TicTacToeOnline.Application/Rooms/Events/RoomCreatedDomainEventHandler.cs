using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeOnline.Domain.DomainEvents;

namespace TicTacToeOnline.Application.Rooms.Events
{
    public class RoomCreatedDomainEventHandler : INotificationHandler<RoomCreatedDomainEvent>
    {
        public async Task Handle(RoomCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await Task.Delay(100, cancellationToken);
        }
    }
}
