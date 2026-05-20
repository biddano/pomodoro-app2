using PomodoroApp.Application.DTOs;

namespace PomodoroApp.Application.Abstractions;

public interface ITimerService
{
    Task<TimerDto> GetCurrentAsync();
    Task<TimerDto> StartAsync(string? task = null);
    Task<TimerDto> PauseAsync();
    Task<TimerDto> ResumeAsync();
    Task<TimerDto> StopAsync();
    Task<TimerDto> ResetAsync();
    Task<TimerDto> SwitchModeAsync(TimerModeDto mode);
    Task<TimerDto> SetTaskAsync(string? task);
}
