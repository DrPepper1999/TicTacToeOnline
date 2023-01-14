using Mapster;
using TicTacToeOnline.Application.Authentication.Commands.Register;
using TicTacToeOnline.Application.Authentication.Common;
using TicTacToeOnline.Application.Authentication.Queries.Login;
using TicTacToeOnline.Contracts.Authentication;

namespace TicTacToeOnline.Api.Common.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, RegisterCommand>();

            config.NewConfig<LoginRequest, LoginQuery>();

            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest, src => src.User);
        }
    }
}
