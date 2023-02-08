using System.Drawing;
using Mapster;
using TicTacToeOnline.Application.Games.Commands.MakeMove;
using TicTacToeOnline.Application.Rooms.Common;
using TicTacToeOnline.Contracts.Room;
using TicTacToeOnline.Domain.Common.Enums;
using TicTacToeOnline.Domain.RoomAggregate.ValueObjects;
using TicTacToeOnline.Domain.TeamAggregate.ValueObjects;

namespace TicTacToeOnline.Api.Common.Mapping
{
    public class GameMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<MoveRequest, MakeMoveCommand>()
                .Map(dest => dest.Move,
                    src => new Point(src.X, src.Y))
                .Map(dest => dest.TeamId,
                    src => TeamId.Create(Guid.Parse(src.TeamId)))
                .Map(dest => dest.Mark,
                    src => (Mark)Enum.Parse(typeof(Mark), src.Mark))
                .Map(dest => dest.GameId,
                    src => RoomId.Create(Guid.Parse(src.GameId)));

            config.NewConfig<Move, MoveResponse>()
                .Map(dest => dest.X, src => src.PlayerMove.X)
                .Map(dest => dest.Y, src => src.PlayerMove.Y)
                .Map(dest => dest.Mark, src => src.Mark.ToString())
                .Map(dest => dest.TeamId, src => src.TeamId.Value);
        }
    }
}
