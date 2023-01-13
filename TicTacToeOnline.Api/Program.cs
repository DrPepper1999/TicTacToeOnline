using Microsoft.AspNetCore.Mvc.Infrastructure;
using TicTacToeOnline.Api.Errors;
using TicTacToeOnline.Application;
using TicTacToeOnline.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);

    builder.Services.AddControllers();

    builder.Services.AddSingleton<ProblemDetailsFactory, TicTacToeOnlineProblemDetailsFactory>();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();
{
    app.UseExceptionHandler("/error"); 
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}

