using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicTacToeOnline.Application.Teams.Queries.GetTeamList;
using TicTacToeOnline.Contracts.Player;
using TicTacToeOnline.Contracts.Team;

namespace TicTacToeOnline.Api.Controllers
{
    [Route("[controller]/{id}/[action]")]
    public class TeamController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;

        public TeamController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<TeamResponse>> GetTeamsAsync()
        {
            var players = await _mediator.Send(new GetTeamListQuery());

            return Ok(players.Select(p => _mapper.Map<TeamResponse>(p)));
        }
    }
}
