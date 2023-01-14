using System.Reflection;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TicTacToeOnline.Application.Authentication.Commands.Register;
using TicTacToeOnline.Application.Authentication.Common;
using TicTacToeOnline.Application.Common.Behaviors;

namespace TicTacToeOnline.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection service)
        {
            service.AddMediatR(typeof(DependencyInjection).Assembly);
            service.AddScoped(typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>));
            service.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return service;
        }
    }
}
