using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeOnline.Application.Common.Interfaces.Authentication;
using TicTacToeOnline.Application.Common.Interfaces.Persistence;
using TicTacToeOnline.Domain.Entities;

namespace TicTacToeOnline.Application.Services.Authentication
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthenticationServices(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public AuthenticationResult Login(string email, string password)
        {
            if (_userRepository.GetUserByEmail(email) is not User user)
            {
                throw new Exception("User with given email already exists");
            }

            if (user.Password != password)
            {
                throw new Exception("Invalid password");
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }

        public AuthenticationResult Register(string email, string password, string name)
        {
            if (_userRepository.GetUserByEmail(email) is not null)
            {
                throw new Exception("User with given email already exists");
            }

            var user = new User { Email = email, Password = password, Name = name };

            _userRepository.Add(user);

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}
