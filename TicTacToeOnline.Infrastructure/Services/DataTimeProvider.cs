using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TicTacToeOnline.Application.Common.Interfaces.Services;
using TicTacToeOnline.Infrastructure.Authentication.JwtToken;
using TicTacToeOnline.Infrastructure.Authentication.RefreshToken;

namespace TicTacToeOnline.Infrastructure.Services
{
    public class DataTimeProvider : IDataTimeProvider
    {
        private readonly RefreshTokenSettings _refreshTokenSettings;

        public DataTimeProvider(IOptions<RefreshTokenSettings> refreshTokenSettings)
        {
            _refreshTokenSettings = refreshTokenSettings.Value;
        }

        public DateTime UtcNow => DateTime.UtcNow;
        public DateTime ExpiryMinutesRefreshToken =>
            UtcNow.AddMinutes(_refreshTokenSettings.ExpiryMinutes);
    }
}
