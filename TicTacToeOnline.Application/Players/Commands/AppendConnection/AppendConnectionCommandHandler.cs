using ErrorOr;
using MediatR;
using TicTacToeOnline.Application.Common.Interfaces.Persistence;
using TicTacToeOnline.Domain.Common.Errors;

namespace TicTacToeOnline.Application.Players.Commands.AppendConnection
{
    public class AppendConnectionCommandHandler : IRequestHandler<AppendConnectionCommand, Error?>
    {
        private readonly IPlayerRepository _playerRepository;

        public AppendConnectionCommandHandler(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<Error?> Handle(AppendConnectionCommand request,
            CancellationToken cancellationToken)
        {
            var player = await _playerRepository.GetByIdAsync(request.PlayerId, cancellationToken);

            if (player is null)
            {
                return Errors.Player.NotFound;
            }

            player.AppendConnection(request.ConnectionId);

            await _playerRepository.UpdateAsync(player, cancellationToken);

            return null;
        }
    }
}
