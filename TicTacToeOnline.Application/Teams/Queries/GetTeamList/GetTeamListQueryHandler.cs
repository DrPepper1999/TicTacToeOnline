using ErrorOr;
using MediatR;
using TicTacToeOnline.Application.Common.Interfaces.Persistence;
using TicTacToeOnline.Domain.TeamAggregate;

namespace TicTacToeOnline.Application.Teams.Queries.GetTeamList
{
    public class GetTeamListQueryHandler : 
        IRequestHandler<GetTeamListQuery, IEnumerable<Team>>
    {

        private readonly ITeamRepository _teamRepository;

        public GetTeamListQueryHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<IEnumerable<Team>> Handle(GetTeamListQuery request,
            CancellationToken cancellationToken)
        {
            return await _teamRepository.GetAllAsync(cancellationToken);
        }
    }
}
