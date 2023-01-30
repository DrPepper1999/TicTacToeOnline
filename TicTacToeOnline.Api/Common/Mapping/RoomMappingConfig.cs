using Mapster;
using TicTacToeOnline.Application.Common.Extensions;
using TicTacToeOnline.Application.Rooms.Commands.CreateRoom;
using TicTacToeOnline.Application.Rooms.Commands.DeleteRoom;
using TicTacToeOnline.Application.Rooms.Queries.GetRoom;
using TicTacToeOnline.Contracts.Room;
using TicTacToeOnline.Domain.PlayerAggregate.ValueObjects;
using TicTacToeOnline.Domain.RoomAggregate;
using TicTacToeOnline.Domain.RoomAggregate.Entities;

namespace TicTacToeOnline.Api.Common.Mapping
{
    public class RoomMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(CreateRoomRequest request, string playerId), CreateRoomCommand>()
                .Map(dest => dest.PlayerId, src => PlayerId.Create(Guid.Parse(src.playerId)))
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
                .Map(dest => dest.Game.Map, src => src.Game.Map);

            config.NewConfig<Game, GameResponse>()
                .Map(dest => dest.Id, src => src.Id.Value)
                .Map(dest => dest.PlayerIds, src =>
                    src.PlayerIds.Select(x => x.Value))
                .Map(dest => dest.PlayerTurn, src => src.PlayerTurn.Value);

            config.NewConfig<Map, MapResponse>()
                .Map(dest => dest.Id, src => src.Id.Value)
                .Map(dest => dest.Fields, src =>
                    string.Join(' ', src.GetFields().ConvertToString()));
        }
    }
}
