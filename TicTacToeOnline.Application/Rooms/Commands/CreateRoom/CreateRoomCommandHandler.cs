using ErrorOr;
using MediatR;
using TicTacToeOnline.Application.Common.Interfaces.Persistence;
using TicTacToeOnline.Domain.Common.ValueObjects;
using TicTacToeOnline.Domain.PlayerAggregate.ValueObjects;
using TicTacToeOnline.Domain.RoomAggregate;

namespace TicTacToeOnline.Application.Rooms.Commands.CreateRoom
{
    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, ErrorOr<Room>>
    {
        private readonly IRoomRepository _roomRepository;

        public CreateRoomCommandHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<ErrorOr<Room>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {

            var room = Room.Create(
                request.Name,
                PlayerId.Create(request.PlayerId), 
                GameSetting.Create(request.MapSize, request.MaxPlayers, request.TeamCount),
                request.Password);

            await _roomRepository.AddAsync(room, cancellationToken);

            await _roomRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return room;
        }
    }
}
