using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using TicTacToeOnline.Application.Players.Commands.CreatePlayer;
using TicTacToeOnline.Contracts.Player;
using TicTacToeOnline.Domain.PlayerAggregate;
using TicTacToeOnline.Domain.RoomAggregate.Entities;
using TicTacToeOnline.Domain.RoomAggregate.Enums;
using TicTacToeOnline.Domain.UserAggregate.ValueObjects;
using ConnectionInfo = TicTacToeOnline.Domain.PlayerAggregate.ValueObjects.ConnectionInfo;

namespace TicTacToeOnline.Api.Common.Mapping
{
    public class PlayerMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(CreatePlayerRequest request, Guid userId), CreatePlayerCommand>();

            config.NewConfig<Player, PlayerResponse>()
                .Map(dest => dest.Id, src => src.Id.Value)
                .Map(dest => dest.UserId, src => ConvertUserId(src.UserId))
                .Map(dest => dest.Mark, src => src.Mark.ToString())
                .Map(dest => dest.AverageRating, src => src.AverageRating.Value);


            config.NewConfig<ConnectionInfo, ConnectionInfoResponse>();
        }

        private static string? ConvertUserId(UserId? userId) => userId?.Value.ToString();
    }
}
