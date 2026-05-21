using Microsoft.EntityFrameworkCore;
using PomodoroApp.Application.Abstractions;
using PomodoroApp.Application.Services;
using PomodoroApp.Data;
using PomodoroApp.Data.Repositories;
using PomodoroApp.WebApi.Features.Timer;

var builder = WebApplication.CreateBuilder(args);

var dbPath = Path.Combine(AppContext.BaseDirectory, "pomodoro.db");
builder.Services.AddDbContext<PomodoroDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

builder.Services.AddScoped<ITimerSessionRepository, TimerSessionRepository>();
builder.Services.AddScoped<ITimerService, TimerService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000", "http://localhost:3001", "http://localhost:3002")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddOpenApi();

var app = builder.Build();

// Initialize database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PomodoroDbContext>();
    await context.Database.EnsureCreatedAsync();
}

app.UseCors("AllowReactApp");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapTimerEndpoints();

app.Run();
