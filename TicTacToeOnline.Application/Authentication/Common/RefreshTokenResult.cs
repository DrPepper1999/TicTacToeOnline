using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeOnline.Application.Authentication.Common
{
    public record RefreshTokenResult(string RefreshToken, DateTime Expires);
}
