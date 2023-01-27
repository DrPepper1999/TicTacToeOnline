using Quartz;
using TicTacToeOnline.Api;
using TicTacToeOnline.Api.Hubs.TicTacToe;
using TicTacToeOnline.Application;
using TicTacToeOnline.Infrastructure;
using TicTacToeOnline.Infrastructure.BackgroundJobs;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);

    builder.Services.AddSignalR();

    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
    });
}

var app = builder.Build();
{
    app.UseExceptionHandler("/error");

    app.UseCors();

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();


    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapHub<TicTacToeHub>("/gameHub");

    app.MapControllers();

    app.Run();
}

