using MediatR;

namespace TicTacToeOnline.Domain.Common.Interfaces
{
    public interface IDomainEvent : INotification
    {
        public Guid Id { get; init; }
    }
}
