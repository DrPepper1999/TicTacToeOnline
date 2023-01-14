using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace TicTacToeOnline.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection service)
        {
            service.AddMediatR(typeof(DependencyInjection).Assembly);

            return service;
        }
    }
}
