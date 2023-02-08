using System.Security.Cryptography;
using TicTacToeOnline.Application.Common.Interfaces.Authentication;

namespace TicTacToeOnline.Infrastructure.Authentication.RefreshToken
{
    public class RefreshTokenGenerator : IRefreshTokenGenerator
    {
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
