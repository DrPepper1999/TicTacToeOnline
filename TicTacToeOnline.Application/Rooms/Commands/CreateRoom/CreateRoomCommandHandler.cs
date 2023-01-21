using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using TicTacToeOnline.Application.Common.Interfaces.Persistence;
using TicTacToeOnline.Domain.GameAggregate.ValueObjects;
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
            await Task.CompletedTask;

            var room = Room.Create(
                request.Name,
                request.PlayersForStart,
                request.Password);

            _roomRepository.Add(room);

            return room;
        }
    }
}
