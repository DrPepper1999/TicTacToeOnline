using FluentValidation;

namespace TicTacToeOnline.Application.Authentication.Queries.GetToken
{
    public class GetTokenByRefreshTokenQueryValidator : AbstractValidator<GetTokenByRefreshTokenQuery>
    {
        public GetTokenByRefreshTokenQueryValidator()
        {
            RuleFor(x => x.RefreshToken).NotEmpty();
        }
    }
}
