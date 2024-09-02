
using ToDo.Infrastructure.Extensions;
using Serilog;
using ToDo.API.Services.Interfaces;
using ToDo.API.Services;

try
{

    var builder = WebApplication.CreateBuilder(args);
    builder.AddSerilog();
    Log.Information("Starting API");


    // Add services to the container.
    builder.Services.AddSingleton<ITaskService, TaskService>();
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
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
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.Information("Server Shutting down...");
    Log.CloseAndFlush();
}