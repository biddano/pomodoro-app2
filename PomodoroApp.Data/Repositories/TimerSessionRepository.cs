using Microsoft.EntityFrameworkCore;
using PomodoroApp.Application.Abstractions;
using PomodoroApp.Domain;

namespace PomodoroApp.Data.Repositories;

public class TimerSessionRepository(PomodoroDbContext context) : ITimerSessionRepository
{
    public async Task<TimerSession?> GetCurrentSessionAsync()
    {
        return await context.TimerSessions.OrderByDescending(s => s.CreatedAt).FirstOrDefaultAsync();
    }

    public async Task<TimerSession> CreateOrUpdateSessionAsync(TimerSession session)
    {
        if (session.Id == 0)
        {
            context.TimerSessions.Add(session);
        }
        else
        {
            context.TimerSessions.Update(session);
        }

        await context.SaveChangesAsync();
        return session;
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}
