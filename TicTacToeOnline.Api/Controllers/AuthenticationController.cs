using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicTacToeOnline.Api.Common.Cookies;
using TicTacToeOnline.Application.Authentication.Commands.CreateRefreshToken;
using TicTacToeOnline.Application.Authentication.Commands.Register;
using TicTacToeOnline.Application.Authentication.Common;
using TicTacToeOnline.Application.Authentication.Queries.GetToken;
using TicTacToeOnline.Application.Authentication.Queries.Login;
using TicTacToeOnline.Contracts.Authentication;
using TicTacToeOnline.Domain.Common.Errors;

namespace TicTacToeOnline.Api.Controllers
{
    [Route("auth")]
    [AllowAnonymous]
    public class AuthenticationController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        public AuthenticationController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = _mapper.Map<RegisterCommand>(request);
            var authResult = await _mediator.Send(command);

            var test = _mapper.Map<AuthenticationResponse>(authResult);
           return authResult.Match(
                auth => Ok(_mapper.Map<AuthenticationResponse>(auth)),
                Problem);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = _mapper.Map<LoginQuery>(request);
            var authResult = await _mediator.Send(query);

            if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
            {
                return Problem(
                    statusCode: StatusCodes.Status401Unauthorized,
                    title: authResult.FirstError.Description);
            }

            var command = new CreateRefreshTokenCommand(authResult.Value.User.Id);

            var refreshToken = await _mediator.Send(command);

            if (refreshToken.IsError)
            {
                return Problem(refreshToken.Errors);
            }

            SetRefreshToken(refreshToken.Value);

            return authResult.Match(
                auth => Ok(_mapper.Map<AuthenticationResponse>(auth)),
                Problem 
            );
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            var refreshToken = Request.Cookies[CookieItemKeys.RefreshToken];

            if (refreshToken is null)
            {
               return Unauthorized("Cookies doesn't has Refresh Token");
            }

            var tokenResult = await GetTokenByRefreshToken(refreshToken);

            if (tokenResult.IsError)
            {
                return Problem(tokenResult.Errors);
            }

            var refreshTokenResult = await _mediator
                .Send(new CreateRefreshTokenCommand(tokenResult.Value.UserId));

            if (refreshTokenResult.IsError)
            {
                return Problem(refreshTokenResult.Errors);
            }

            SetRefreshToken(refreshTokenResult.Value);

            return Ok(tokenResult);
        }

        private async Task<ErrorOr<TokenResult>> GetTokenByRefreshToken(string? refreshToken)
        {
            var query = new GetTokenByRefreshTokenQuery(refreshToken!);

            var tokenResult = await _mediator.Send(query);

            return tokenResult;
        }


        private void SetRefreshToken(RefreshTokenResult refreshTokenResult)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true,
                Expires = refreshTokenResult.Expires
            };

            Response
                .Cookies
                .Append(CookieItemKeys.RefreshToken, refreshTokenResult.RefreshToken, cookieOptions);
        }
    }
}