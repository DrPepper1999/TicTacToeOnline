using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TicTacToeOnline.Application.Common.Interfaces.Authentication;
using TicTacToeOnline.Application.Common.Interfaces.Persistence;
using TicTacToeOnline.Application.Common.Interfaces.Services;
using TicTacToeOnline.Infrastructure.Authentication;
using TicTacToeOnline.Infrastructure.Persistence;
using TicTacToeOnline.Infrastructure.Persistence.Repositories;
using TicTacToeOnline.Infrastructure.Services;
namespace TicTacToeOnline.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            ConfigurationManager configuration)
        {
            services
                .AddAuth(configuration)
                .AddPersistence(configuration);

            services.AddSingleton<IDataTimeProvider, DataTimeProvider>();

            return services;
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services,
            ConfigurationManager configuration)
        {
            services.AddDbContext<TicTacToeOnlineDbContext>(option =>
                option.UseNpgsql("Host=localhost;Port=5432;Database=TicTacToeOnline;Username=postgres;Password=Lfybbk1999"));

                services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();

            return services;
        }

        public static IServiceCollection AddAuth(this IServiceCollection service,
            ConfigurationManager configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings.SectionName, jwtSettings); // мапим конфигуарцию и объект

            service.AddSingleton(Options.Create(jwtSettings));
            service.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Secret))
                });

            return service;
        }
    }
}
