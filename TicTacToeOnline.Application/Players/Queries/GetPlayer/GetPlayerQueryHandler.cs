using ErrorOr;
using MediatR;
using TicTacToeOnline.Application.Common.Interfaces.Persistence;
using TicTacToeOnline.Domain.Common.Errors;
using TicTacToeOnline.Domain.PlayerAggregate;

namespace TicTacToeOnline.Application.Players.Queries.GetPlayer
{
    public class GetPlayerQueryHandler : IRequestHandler<GetPlayerQuery, ErrorOr<Player>>
    {
        private readonly IPlayerRepository _playerRepository;

        public GetPlayerQueryHandler(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<ErrorOr<Player>> Handle(GetPlayerQuery request, CancellationToken cancellationToken)
        {
            var player = await _playerRepository.GetFirstWhere(request.predicate, cancellationToken);

            if (player is null)
            {
                return Errors.Player.NotFound;
            }

            return player;
        }
    }
}
