using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeOnline.Domain.Common.Interfaces;
using TicTacToeOnline.Domain.RoomAggregate.ValueObjects;

namespace TicTacToeOnline.Domain.DomainEvents
{
    public sealed record RoomCreatedDomainEvent(RoomId RoomId) : IDomainEvent;
}
