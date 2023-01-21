using TicTacToeOnline.Domain.UserAggregate;

namespace TicTacToeOnline.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        public string GenerateToken(User user);
    }
}
