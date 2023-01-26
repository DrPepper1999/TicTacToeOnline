using ErrorOr;
using MediatR;
using TicTacToeOnline.Application.Common.Interfaces.Persistence;
using TicTacToeOnline.Domain.Common.Errors;
using TicTacToeOnline.Domain.RoomAggregate;

namespace TicTacToeOnline.Application.Rooms.Queries.GetRoom
{
    public class GetRoomQueryHandler : IRequestHandler<GetRoomQuery, ErrorOr<Room>>
    {
        private readonly IRoomRepository _roomRepository;

        public GetRoomQueryHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<ErrorOr<Room>> Handle(GetRoomQuery request, CancellationToken cancellationToken)
        {
            var room = await _roomRepository.GetByIdAsync(request.Id, cancellationToken);

            if (room is null)
            {
                return Errors.Room.NotFound;
            }

            return room;
        }
    }
}
