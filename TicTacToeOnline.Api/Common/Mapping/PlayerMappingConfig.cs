using Mapster;
using TicTacToeOnline.Application.Players.Commands.CreatePlayer;
using TicTacToeOnline.Contracts.Player;
using TicTacToeOnline.Domain.PlayerAggregate;
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
                .Map(dest => dest.AverageRating, src => src.AverageRating.Value);


            config.NewConfig<ConnectionInfo, ConnectionInfoResponse>();
        }

        private static string? ConvertUserId(UserId? userId) => userId?.Value.ToString();
    }
}
