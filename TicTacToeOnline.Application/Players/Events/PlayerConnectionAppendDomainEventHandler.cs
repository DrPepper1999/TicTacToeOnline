using MediatR;
using TicTacToeOnline.Application.Common.Interfaces.Persistence;
using TicTacToeOnline.Domain.DomainEvents;

namespace TicTacToeOnline.Application.Players.Events
{
    public class PlayerConnectionAppendDomainEventHandler : INotificationHandler<PlayerConnectionAppendDomainEvent>
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerConnectionAppendDomainEventHandler(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task Handle(PlayerConnectionAppendDomainEvent notification, CancellationToken cancellationToken)
        {

            await _playerRepository.UpdateAsync(notification.Player, cancellationToken);
        }
    }
}
