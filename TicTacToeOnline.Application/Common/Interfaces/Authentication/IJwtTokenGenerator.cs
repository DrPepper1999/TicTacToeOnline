using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeOnline.Domain.Entities;

namespace TicTacToeOnline.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        public string GenerateToken(User user);
    }
}
