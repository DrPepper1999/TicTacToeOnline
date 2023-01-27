using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TicTacToeOnline.Api.Hubs.TicTacToe;
using TicTacToeOnline.Application.Players.Commands.CreatePlayer;
using TicTacToeOnline.Contracts.Player;

namespace TicTacToeOnline.Api.Controllers
{
    [Route("[controller]/[action]")]
    public class PlayersController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;
        private readonly IHubContext<TicTacToeHub> _ticTacToeHubContext;

        public PlayersController(IMapper mapper, ISender mediator, IHubContext<TicTacToeHub> ticTacToeHubContext)
        {
            _mapper = mapper;
            _mediator = mediator;
            _ticTacToeHubContext = ticTacToeHubContext;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePlayerRequest request)
        {
            var command = _mapper.Map<CreatePlayerCommand>(request);

            var createPlayerResult = await _mediator.Send(command);

            return createPlayerResult.Match(
                room => Ok(_mapper.Map<PlayerResponse>(room)), // CreatedAtAction(nameof(GetPlayer), new {roomId = room.Id}, room)
                Problem
            );
        }
    }
}
