using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicTacToeOnline.Application.Rooms.Commands.CreateRoom;
using TicTacToeOnline.Contracts.Room;

namespace TicTacToeOnline.Api.Controllers
{
    [Route("[controller]")] 
    public class RoomsController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;

        public RoomsController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(CreateRoomRequest request)
        {
            var command = _mapper.Map<CreateRoomCommand>(request);

            var createRoomResult = await _mediator.Send(command);

            return createRoomResult.Match(
                room => Ok(_mapper.Map<RoomResponse>(room)), // CreatedAtAction(nameof(GetRoom), new {roomId = room.Id}, room)
                Problem
            );
        }
    }
}
