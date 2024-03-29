﻿using ErrorOr;
using MediatR;
using TicTacToeOnline.Application.Authentication.Common;
using TicTacToeOnline.Application.Common.Interfaces.Authentication;
using TicTacToeOnline.Application.Common.Interfaces.Persistence;
using TicTacToeOnline.Domain.Common.Errors;
using TicTacToeOnline.Domain.UserAggregate;

namespace TicTacToeOnline.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command,
            CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            if (await _userRepository.GetUserByEmail(command.Email) is not null)
            {
                return Errors.User.DuplicateEmail;
            }

            var user = User.Create(command.Name, command.Email, command.Password);

            await _userRepository.Add(user);

            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}
