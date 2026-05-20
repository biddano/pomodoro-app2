using PomodoroApp.Application.Abstractions;
using PomodoroApp.Data;
using Microsoft.EntityFrameworkCore;
using DomainTimer = PomodoroApp.Domain.Timer;
using DomainTimerStatus = PomodoroApp.Domain.TimerStatus;

namespace PomodoroApp.WebApi.Services;

public class TimerBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public TimerBackgroundService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<PomodoroDbContext>();
                    var timer = await db.Timers.OrderByDescending(t => t.CreatedAt).FirstOrDefaultAsync(stoppingToken);

                    if (timer != null && timer.Status == DomainTimerStatus.Running && timer.RemainingSeconds > 0)
                    {
                        timer.RemainingSeconds--;
                        timer.UpdatedAt = DateTime.UtcNow;

                        if (timer.RemainingSeconds == 0)
                        {
                            timer.Status = DomainTimerStatus.Stopped;
                        }

                        db.Timers.Update(timer);
                        await db.SaveChangesAsync(stoppingToken);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in TimerBackgroundService: {ex.Message}");
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}
