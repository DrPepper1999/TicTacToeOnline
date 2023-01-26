using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicTacToeOnline.Application.Players.Commands.CreatePlayer;
using TicTacToeOnline.Contracts.Player;

namespace TicTacToeOnline.Api.Controllers
{
    [Route("[controller]/[action]")]
    public class PlayersController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;

        public PlayersController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePlayerRequest request)
        {
            var command = _mapper.Map<CreatePlayerCommand>(request);

            var createPlayerResult = await _mediator.Send(command);

            return createPlayerResult.Match(
                room => Ok(_mapper.Map<PlayerResponse>(room)), // CreatedAtAction(nameof(GetRoom), new {roomId = room.Id}, room)
                Problem
            );
        }
    }
}
