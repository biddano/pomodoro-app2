using PomodoroApp.Application.Abstractions;
using PomodoroApp.Domain;

namespace PomodoroApp.Application.Services;

public class TimerService(ITimerSessionRepository repository) : ITimerService
{
    private const int FocusSeconds = 25 * 60;
    private const int BreakSeconds = 5 * 60;

    public async Task<TimerSession> GetCurrentStateAsync()
    {
        var session = await repository.GetCurrentSessionAsync();
        return session ?? new TimerSession();
    }

    public async Task<TimerSession> StartTimerAsync()
    {
        var session = await GetCurrentStateAsync();
        session.IsRunning = true;
        session.LastUpdatedAt = DateTime.UtcNow;
        return await repository.CreateOrUpdateSessionAsync(session);
    }

    public async Task<TimerSession> PauseTimerAsync()
    {
        var session = await GetCurrentStateAsync();
        session.IsRunning = false;
        session.LastUpdatedAt = DateTime.UtcNow;
        return await repository.CreateOrUpdateSessionAsync(session);
    }

    public async Task<TimerSession> ResetTimerAsync()
    {
        var session = await GetCurrentStateAsync();
        session.IsRunning = false;
        session.SecondsRemaining = session.Mode == TimerMode.Focus ? FocusSeconds : BreakSeconds;
        session.LastUpdatedAt = DateTime.UtcNow;
        return await repository.CreateOrUpdateSessionAsync(session);
    }

    public async Task<TimerSession> SwitchModeAsync(TimerMode mode)
    {
        var session = await GetCurrentStateAsync();
        session.Mode = mode;
        session.IsRunning = false;
        session.SecondsRemaining = mode == TimerMode.Focus ? FocusSeconds : BreakSeconds;
        session.LastUpdatedAt = DateTime.UtcNow;
        return await repository.CreateOrUpdateSessionAsync(session);
    }

    public async Task<TimerSession> SetKeyTaskAsync(string? task)
    {
        var session = await GetCurrentStateAsync();
        session.KeyTask = task;
        session.LastUpdatedAt = DateTime.UtcNow;
        return await repository.CreateOrUpdateSessionAsync(session);
    }

    public async Task<TimerSession> TickAsync()
    {
        var session = await GetCurrentStateAsync();

        if (!session.IsRunning || session.SecondsRemaining <= 0)
            return session;

        session.SecondsRemaining--;
        session.LastUpdatedAt = DateTime.UtcNow;
        return await repository.CreateOrUpdateSessionAsync(session);
    }
}
