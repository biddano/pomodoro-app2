using Microsoft.EntityFrameworkCore;
using PomodoroApp.Application.Abstractions;
using PomodoroApp.Application.Services;
using PomodoroApp.Data;
using PomodoroApp.Data.Repositories;
using PomodoroApp.WebApi.Features.Timer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var dbPath = Path.Combine(AppContext.BaseDirectory, "pomodoro.db");
builder.Services.AddDbContext<PomodoroDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

builder.Services.AddScoped<ITimerRepository, TimerRepository>();
builder.Services.AddScoped<ITimerService, TimerService>();
builder.Services.AddHostedService<PomodoroApp.WebApi.Services.TimerBackgroundService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PomodoroDbContext>();
    db.Database.EnsureCreated();
}

app.UseCors("ReactApp");

TimerEndpoints.MapEndpoints(app);

app.Run("http://localhost:5000");
