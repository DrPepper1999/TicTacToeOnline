using Mapster;
using TicTacToeOnline.Application.Players.Commands.CreatePlayer;
using TicTacToeOnline.Contracts.Player;
using TicTacToeOnline.Contracts.Team;
using TicTacToeOnline.Domain.TeamAggregate;

namespace TicTacToeOnline.Api.Common.Mapping
{
    public class TeamMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Team, TeamResponse>()
                .Map(dest => dest.Id, src => src.Id.Value)
                .Map(dest => dest.Mark, src => src.Mark.ToString())
                .Map(dest => dest.PlayerIds, src => src.PlayerIds.Select(x => x.Value));
        }
    }
}
