using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeOnline.Application.Services.Authentication
{
    public interface IAuthenticationServices
    {
        AuthenticationResult Login(string email, string password);
        AuthenticationResult Register(string email, string password, string name);
    }
}
    