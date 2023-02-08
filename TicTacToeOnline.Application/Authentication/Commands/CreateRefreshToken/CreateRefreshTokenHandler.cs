using ErrorOr;
using MediatR;
using TicTacToeOnline.Application.Authentication.Common;
using TicTacToeOnline.Application.Common.Interfaces.Authentication;
using TicTacToeOnline.Application.Common.Interfaces.Persistence;
using TicTacToeOnline.Application.Common.Interfaces.Services;
using TicTacToeOnline.Domain.Common.Errors;

namespace TicTacToeOnline.Application.Authentication.Commands.CreateRefreshToken
{
    public class CreateRefreshTokenHandler : IRequestHandler<CreateRefreshTokenCommand, ErrorOr<RefreshTokenResult>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;
        private readonly IDataTimeProvider _dataTimeProvider;

        public CreateRefreshTokenHandler(IUserRepository userRepository, IDataTimeProvider dataTimeProvider, IRefreshTokenGenerator refreshTokenGenerator)
        {
            _userRepository = userRepository;
            _dataTimeProvider = dataTimeProvider;
            _refreshTokenGenerator = refreshTokenGenerator;
        }

        public async Task<ErrorOr<RefreshTokenResult>> Handle(CreateRefreshTokenCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.UserId);

            if (user is null)
            {
                return Errors.User.NotFound;
            }

            var refreshToken = _refreshTokenGenerator.GenerateRefreshToken();
            var expiryMinutesRefreshToken = _dataTimeProvider.ExpiryMinutesRefreshToken;

            user.SetRefreshToken(refreshToken, expiryMinutesRefreshToken);

            await _userRepository.UpdateAsync(user);

            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return new RefreshTokenResult(refreshToken, expiryMinutesRefreshToken);
        }
    }
}
