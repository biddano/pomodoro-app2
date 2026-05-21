using PomodoroApp.Domain;

namespace PomodoroApp.Application.Abstractions;

public interface ITimerService
{
    Task<TimerSession> GetCurrentStateAsync();
    Task<TimerSession> StartTimerAsync();
    Task<TimerSession> PauseTimerAsync();
    Task<TimerSession> ResetTimerAsync();
    Task<TimerSession> SwitchModeAsync(TimerMode mode);
    Task<TimerSession> SetKeyTaskAsync(string? task);
    Task<TimerSession> TickAsync();
}
