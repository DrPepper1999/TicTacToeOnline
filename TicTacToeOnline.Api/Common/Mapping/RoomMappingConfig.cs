using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using TicTacToeOnline.Application.Rooms.Commands.CreateRoom;
using TicTacToeOnline.Contracts.Room;
using TicTacToeOnline.Domain.GameAggregate.Enums;
using TicTacToeOnline.Domain.RoomAggregate;

namespace TicTacToeOnline.Api.Common.Mapping
{
    public class RoomMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateRoomRequest, CreateRoomCommand>()
                .Map(dest => dest.PlayerMark, src =>
                    (Mark)Enum.Parse(typeof(Mark), src.PlayerMark))
                .Map(dest => dest, src => src);

            config.NewConfig<Room, RoomResponse>()
                .Map(dest => dest.Id, src => src.Id.Value);

        }
    }
}
