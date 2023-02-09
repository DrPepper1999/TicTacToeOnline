using MediatR;
using TicTacToeOnline.Application.Common.Interfaces.Persistence;
using TicTacToeOnline.Domain.TeamAggregate;

namespace TicTacToeOnline.Application.Teams.Queries.GetRangeTeamList
{
    public class GetRangeTeamListQueryHandler : IRequestHandler<GetRangeTeamListQuery, IEnumerable<Team>>
    {
        private readonly ITeamRepository _teamRepository;

        public GetRangeTeamListQueryHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<IEnumerable<Team>> Handle(GetRangeTeamListQuery request, CancellationToken cancellationToken)
        {
            //return await _teamRepository.GetRangeByIdsAsync(request.TeamIds, cancellationToken);
            return Array.Empty<Team>();
        }
    }
}
