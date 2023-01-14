using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeOnline.Api.Common.Errors;
using TicTacToeOnline.Api.Common.Mapping;

namespace TicTacToeOnline.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection service)
        {
            service.AddControllers();
            service.AddSingleton<ProblemDetailsFactory, TicTacToeOnlineProblemDetailsFactory>();
            service.AddEndpointsApiExplorer();
            service.AddSwaggerGen();
            service.AddMappings();

            return service;
        }
    }
}
