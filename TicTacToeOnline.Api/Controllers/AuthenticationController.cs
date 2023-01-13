using Microsoft.AspNetCore.Mvc;
using TicTacToeOnline.Application.Services.Authentication;
using TicTacToeOnline.Contracts.Authentication;

namespace TicTacToeOnline.Api.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationServices _authenticationServices;
        public AuthenticationController(IAuthenticationServices authenticationServices)
        {
            _authenticationServices = authenticationServices;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            var authResult = _authenticationServices
                .Register(request.Email, request.Password, request.Name);

            var response = new AuthenticationResponse(
                authResult.User.Id,
                authResult.User.Email,
                authResult.User.Name,
                authResult.Token);

            return Ok(response);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var authResult = _authenticationServices
                .Login(request.Email, request.Password);

            var response = new AuthenticationResponse(
                authResult.User.Id,
                authResult.User.Email,
                authResult.User.Name,
                authResult.Token);

            return Ok(response);
        }
    }
}