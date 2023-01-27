using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using TicTacToeOnline.Application.Common.Interfaces.Persistence;
using TicTacToeOnline.Domain.PlayerAggregate;

namespace TicTacToeOnline.Application.Players.Commands.CreatePlayer
{
    public class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, ErrorOr<Player>>
    {
        private readonly IPlayerRepository _playerRepository;

        public CreatePlayerCommandHandler(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<ErrorOr<Player>> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
        {
            var player = Player.Create(request.UserId, request.Name, request.ConnectionId);

            await _playerRepository.AddAsync(player, cancellationToken);

            await _playerRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return player;
        }
    }
}
