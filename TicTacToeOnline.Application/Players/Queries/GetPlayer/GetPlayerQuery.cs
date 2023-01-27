using ErrorOr;
using MediatR;
using System.Linq.Expressions;
using TicTacToeOnline.Domain.PlayerAggregate;

namespace TicTacToeOnline.Application.Players.Queries.GetPlayer
{
    public record GetPlayerQuery(Expression<Func<Player, bool>> predicate) : IRequest<ErrorOr<Player>>;
}
