using Microsoft.EntityFrameworkCore;
using PomodoroApp.Application.Abstractions;
using DomainTimer = PomodoroApp.Domain.Timer;

namespace PomodoroApp.Data.Repositories;

public class TimerRepository(PomodoroDbContext context) : ITimerRepository
{
    public async Task<DomainTimer?> GetCurrentAsync()
    {
        return await context.Timers.OrderByDescending(t => t.CreatedAt).FirstOrDefaultAsync();
    }

    public async Task<DomainTimer> CreateAsync(DomainTimer timer)
    {
        context.Timers.Add(timer);
        await context.SaveChangesAsync();
        return timer;
    }

    public async Task<DomainTimer> UpdateAsync(DomainTimer timer)
    {
        context.Timers.Update(timer);
        await context.SaveChangesAsync();
        return timer;
    }
}
