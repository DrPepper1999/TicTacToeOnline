using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicTacToeOnline.Application.Players.Commands.CreatePlayer;
using TicTacToeOnline.Application.Rooms.Commands.CreateRoom;
using TicTacToeOnline.Application.Rooms.Commands.DeleteRoom;
using TicTacToeOnline.Application.Rooms.Queries.GetRoom;
using TicTacToeOnline.Contracts.Player;
using TicTacToeOnline.Contracts.Room;

namespace TicTacToeOnline.Api.Controllers
{
    [Route("[controller]/{playerId}/[action]")] 
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

        [HttpPost]
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
    }
}
