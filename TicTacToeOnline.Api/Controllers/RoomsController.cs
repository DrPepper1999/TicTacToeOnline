using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicTacToeOnline.Application.Rooms.Commands.CreateRoom;
using TicTacToeOnline.Application.Rooms.Commands.DeleteRoom;
using TicTacToeOnline.Application.Rooms.Queries.GetRoom;
using TicTacToeOnline.Application.Rooms.Queries.GetTeams;
using TicTacToeOnline.Contracts.Room;
using TicTacToeOnline.Contracts.Team;

namespace TicTacToeOnline.Api.Controllers
{
    [Route("[controller]/[action]")] 
    public class RoomsController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;

        public RoomsController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(GetRoomRequest request, Guid id)
        {
            var query = _mapper.Map<GetRoomQuery>((request, id));

            var getRoomResult = await _mediator.Send(query);

            return getRoomResult.Match(
                room => Ok(_mapper.Map<RoomResponse>(room)),
                Problem
            );
        }

        [HttpPost("playerId")]
        public async Task<IActionResult> Create(CreateRoomRequest request, string playerId)
        {
            var command = _mapper.Map<CreateRoomCommand>((request, playerId));

            var createRoomResult = await _mediator.Send(command);

            return createRoomResult.Match(
                room => Ok(_mapper.Map<RoomResponse>(room)), // CreatedAtAction(nameof(GetRoom), new {RoomId = room.Id}, room)
                Problem
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(DeleteRoomRequest request, Guid id)
        {
            var command = _mapper.Map<DeleteRoomCommand>((request, id));

            var deleteRoomResult = await _mediator.Send(command);

            return deleteRoomResult.Match(
                room => NoContent(),
                Problem);
        }

        [HttpGet("{roomId}")]
        public async Task<IActionResult> GetTeamsAsync(Guid roomId)
        {
            var query = new GetRoomTeamsQuery(roomId);

            var teamsResult = await _mediator.Send(query);

            return teamsResult.Match(
                teams => Ok(teams.Adapt<IEnumerable<TeamResponse>>()),
                Problem);

        }
    }
}
