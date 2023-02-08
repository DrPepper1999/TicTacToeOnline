using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using TicTacToeOnline.Application.Common.Interfaces.Authentication;
using TicTacToeOnline.Application.Common.Interfaces.Persistence;
using TicTacToeOnline.Application.Common.Interfaces.Services;
using TicTacToeOnline.Infrastructure.Authentication.JwtToken;
using TicTacToeOnline.Infrastructure.Authentication.RefreshToken;
using TicTacToeOnline.Infrastructure.BackgroundJobs;
using TicTacToeOnline.Infrastructure.Idempotence;
using TicTacToeOnline.Infrastructure.Persistence;
using TicTacToeOnline.Infrastructure.Persistence.Interceptors;
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
                .AddPersistence(configuration)
                .AddQuartz(configuration);

            services.AddSingleton<IDataTimeProvider, DataTimeProvider>();

            return services;
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services,
            ConfigurationManager configuration)
        {
            services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

            services.Decorate(typeof(INotificationHandler<>), typeof(IdempotentDomainEventHandler<>));

            services.AddDbContext<TicTacToeOnlineDbContext>((sp, option) =>
            {
                var interceptor = sp.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();
                option
                    .UseNpgsql("Host=localhost;Port=5432;Database=TicTacToeOnlineDb;Username=postgres;Password=Lfybbk1999")
                    .AddInterceptors(interceptor!);
            });

            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();

            return services;
        }

        public static IServiceCollection AddAuth(this IServiceCollection service,
            ConfigurationManager configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings.SectionName, jwtSettings); // мапим конфигуарцию и объект

            var refreshTokenSettings = new RefreshTokenSettings();
            configuration.Bind(RefreshTokenSettings.SectionName, refreshTokenSettings);

            service.AddSingleton(Options.Create(refreshTokenSettings));
            service.AddSingleton<IRefreshTokenGenerator, RefreshTokenGenerator>();

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

        public static IServiceCollection AddQuartz(this IServiceCollection service,
            ConfigurationManager configuration)
        {
            var backgroundJobsSettings = new BackgroundJobsSettings();
            configuration.Bind(BackgroundJobsSettings.SectionName, backgroundJobsSettings);

            service.AddQuartz(configure =>
            {
                var jobKey = new JobKey(nameof(ProcessOutMessagesJob));

                configure
                    .AddJob<ProcessOutMessagesJob>(jobKey)
                    .AddTrigger(trigger =>
                        trigger.ForJob(jobKey)
                            .WithSimpleSchedule(schedule =>
                                schedule.WithIntervalInSeconds(backgroundJobsSettings.IntervalInSeconds)
                                    .RepeatForever()));

                configure.UseMicrosoftDependencyInjectionJobFactory();
            });

            service.AddQuartzHostedService();

            return service;
        }
    }
}
