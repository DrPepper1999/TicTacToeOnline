using ErrorOr;
using MediatR;
using TicTacToeOnline.Application.Authentication.Common;
using TicTacToeOnline.Application.Common.Interfaces.Authentication;
using TicTacToeOnline.Application.Common.Interfaces.Persistence;
using TicTacToeOnline.Domain.Common.Errors;

namespace TicTacToeOnline.Application.Authentication.Queries.GetToken
{
    public class GetTokenByRefreshTokenQueryHandler : IRequestHandler<GetTokenByRefreshTokenQuery, ErrorOr<TokenResult>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public GetTokenByRefreshTokenQueryHandler(IUserRepository userRepository,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ErrorOr<TokenResult>> Handle(GetTokenByRefreshTokenQuery request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository
                .GetFirstWhere(x =>
                    x.RefreshToken != null && x.RefreshToken.Equals(request.RefreshToken), cancellationToken);

            if (user is null)
            {
                return Errors.User.NotFoundRefreshToken;
            }

            if (user.TokenExpires < DateTime.Now)
            {
                return Errors.Authentication.TokenExpired;
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new TokenResult(token, user.Id);
        }
    }
}
