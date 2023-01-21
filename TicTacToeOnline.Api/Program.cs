using TicTacToeOnline.Api;
using TicTacToeOnline.Application;
using TicTacToeOnline.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    app.UseExceptionHandler("/error");


    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();


    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapControllers();

    app.Run();
}

