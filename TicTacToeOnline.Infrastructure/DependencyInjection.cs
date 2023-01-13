using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicTacToeOnline.Application.Common.Interfaces.Authentication;
using TicTacToeOnline.Application.Common.Interfaces.Persistence;
using TicTacToeOnline.Application.Common.Interfaces.Services;
using TicTacToeOnline.Infrastructure.Authentication;
using TicTacToeOnline.Infrastructure.Persistence;
using TicTacToeOnline.Infrastructure.Services;

namespace TicTacToeOnline.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection service,
            ConfigurationManager configuration)
        {
            service.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
            service.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            service.AddSingleton<IDataTimeProvider, DataTimeProvider>();

            service.AddScoped<IUserRepository, UserRepository>();

            return service;
        }
    }
}
