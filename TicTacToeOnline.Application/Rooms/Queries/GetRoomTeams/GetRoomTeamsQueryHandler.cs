using ErrorOr;
using MediatR;
using TicTacToeOnline.Application.Common.Interfaces.Persistence;
using TicTacToeOnline.Domain.Common.Errors;
using TicTacToeOnline.Domain.TeamAggregate;

namespace TicTacToeOnline.Application.Rooms.Queries.GetTeams
{
    public class GetRoomTeamsQueryHandler : IRequestHandler<GetRoomTeamsQuery, ErrorOr<IEnumerable<Team>>>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly ITeamRepository _teamRepository;

        public GetRoomTeamsQueryHandler(IRoomRepository roomRepository, ITeamRepository teamRepository)
        {
            _roomRepository = roomRepository;
            _teamRepository = teamRepository;
        }

        public async Task<ErrorOr<IEnumerable<Team>>> Handle(GetRoomTeamsQuery request, CancellationToken cancellationToken)
        {
            var room = await _roomRepository.GetByIdAsync(request.RoomId, cancellationToken);

            if (room is null)
            {
                return Errors.Room.NotFound;
            }

            var teams = await _teamRepository
                .GetRangeByIdsAsync(room.TeamIds.ToList(), cancellationToken);

           return ErrorOrFactory
               .From(teams);
        }
    }
}
