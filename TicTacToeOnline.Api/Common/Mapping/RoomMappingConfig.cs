using Mapster;
using TicTacToeOnline.Application.Rooms.Commands.CreateRoom;
using TicTacToeOnline.Application.Rooms.Commands.DeleteRoom;
using TicTacToeOnline.Application.Rooms.Queries.GetRoom;
using TicTacToeOnline.Contracts.Room;
using TicTacToeOnline.Domain.Common.ValueObjects;
using TicTacToeOnline.Domain.PlayerAggregate.ValueObjects;
using TicTacToeOnline.Domain.RoomAggregate;

namespace TicTacToeOnline.Api.Common.Mapping
{
    public class RoomMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(CreateRoomRequest request, string playerId), CreateRoomCommand>()
                .Map(dest => dest.PlayerId,
                    src => src.playerId)
                .Map(dest => dest, src => src.request);

            config.NewConfig<(CreateRoomRequest request, PlayerId playerId), CreateRoomCommand>()
                .Map(dest => dest.PlayerId, src => src.playerId)
                .Map(dest => dest, src => src.request);

            config.NewConfig<(GetRoomRequest request, Guid roomId), GetRoomQuery>()
                .Map(dest => dest.Id, src => src.roomId)
                .Map(dest => dest, src => src.request);

            config.NewConfig<(DeleteRoomRequest request, Guid roomId), DeleteRoomCommand>()
                .Map(dest => dest.Id, src => src.roomId)
                .Map(dest => dest, src => src.request);

            config.NewConfig<Room, RoomResponse>()
                .Map(dest => dest.Id, src => src.Id.Value)
                .Map(dest => dest.Status, src => src.Status.ToString())
                .Map(dest => dest.MapSize, src => src.GameSetting.MapSize)
                .Map(dest => dest.MaxPlayers, src => src.GameSetting.MaxPlayers)
                .Map(dest => dest.TeamCount, src => src.GameSetting.TeamCount)
                .Map(dest => dest.TeamIds,
                src => src.TeamIds.Select(x => x.Value))
                .Map(dest => dest.PlayerIds,
                src => src.PlayerIds.Select(x => x.Value));


        }
    }
}
