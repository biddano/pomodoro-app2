using PomodoroApp.Domain;

namespace PomodoroApp.Application.Abstractions;

public interface ITimerSessionRepository
{
    Task<TimerSession?> GetCurrentSessionAsync();
    Task<TimerSession> CreateOrUpdateSessionAsync(TimerSession session);
    Task SaveChangesAsync();
}
