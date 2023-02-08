using ErrorOr;
using MediatR;
using TicTacToeOnline.Application.Common.Interfaces.Persistence;
using TicTacToeOnline.Application.Rooms.Common;
using TicTacToeOnline.Domain.Common.Errors;

namespace TicTacToeOnline.Application.Games.Commands.MakeMove
{
    public class MakeMoveCommandHandler : IRequestHandler<MakeMoveCommand, ErrorOr<Move>>
    {
        private readonly IGameRepository _gameRepository;

        public MakeMoveCommandHandler(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<ErrorOr<Move>> Handle(MakeMoveCommand request, CancellationToken cancellationToken)
        {
            var game = await _gameRepository.GetByIdAsync(request.GameId.Value, cancellationToken);

            if (game is null)
            {
                return Errors.Game.NotFound;
            }

            if (request.TeamId != game.PlayerTurn)
            {
                return Errors.Game.AnotherTeamTurn;
            }

            var makeMoveResult = game.MakeMove(request.Move, request.Mark);

            if (makeMoveResult.HasValue)
            {
                return makeMoveResult.Value;
            }

            return new Move(request.Move, request.Mark, request.TeamId, request.GameId);
        }
    }
}
