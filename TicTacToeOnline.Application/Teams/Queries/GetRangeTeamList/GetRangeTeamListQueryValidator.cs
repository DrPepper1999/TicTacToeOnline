using FluentValidation;
using TicTacToeOnline.Application.Teams.Queries.GetTeamList;

namespace TicTacToeOnline.Application.Teams.Queries.GetRangeTeamList
{
    public class GetRangeTeamListQueryValidator : AbstractValidator<GetRangeTeamListQuery>
    {
        public GetRangeTeamListQueryValidator()
        {
            RuleFor(x => x.TeamIds).NotEmpty().NotNull();
        }
    }
}
