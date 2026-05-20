using PomodoroApp.Application.Abstractions;
using PomodoroApp.Application.DTOs;
using DomainTimer = PomodoroApp.Domain.Timer;
using DomainTimerMode = PomodoroApp.Domain.TimerMode;
using DomainTimerStatus = PomodoroApp.Domain.TimerStatus;

namespace PomodoroApp.Application.Services;

public class TimerService(ITimerRepository repository) : ITimerService
{
    private const int FocusDurationSeconds = 25 * 60;
    private const int BreakDurationSeconds = 5 * 60;

    public async Task<TimerDto> GetCurrentAsync()
    {
        var timer = await repository.GetCurrentAsync();
        if (timer == null)
        {
            var newTimer = new DomainTimer { RemainingSeconds = FocusDurationSeconds };
            timer = await repository.CreateAsync(newTimer);
        }
        return MapToDto(timer);
    }

    public async Task<TimerDto> StartAsync(string? task = null)
    {
        var timer = await repository.GetCurrentAsync() ?? new DomainTimer { RemainingSeconds = FocusDurationSeconds };

        if (!string.IsNullOrWhiteSpace(task))
            timer.Task = task;

        timer.Status = DomainTimerStatus.Running;
        timer.UpdatedAt = DateTime.UtcNow;

        timer = await repository.UpdateAsync(timer);
        return MapToDto(timer);
    }

    public async Task<TimerDto> PauseAsync()
    {
        var timer = await GetOrCreateTimer();
        timer.Status = DomainTimerStatus.Paused;
        timer.UpdatedAt = DateTime.UtcNow;
        timer = await repository.UpdateAsync(timer);
        return MapToDto(timer);
    }

    public async Task<TimerDto> ResumeAsync()
    {
        var timer = await GetOrCreateTimer();
        timer.Status = DomainTimerStatus.Running;
        timer.UpdatedAt = DateTime.UtcNow;
        timer = await repository.UpdateAsync(timer);
        return MapToDto(timer);
    }

    public async Task<TimerDto> StopAsync()
    {
        var timer = await GetOrCreateTimer();
        timer.Status = DomainTimerStatus.Stopped;
        timer.RemainingSeconds = timer.Mode == DomainTimerMode.Focus ? FocusDurationSeconds : BreakDurationSeconds;
        timer.UpdatedAt = DateTime.UtcNow;
        timer = await repository.UpdateAsync(timer);
        return MapToDto(timer);
    }

    public async Task<TimerDto> ResetAsync()
    {
        var timer = await GetOrCreateTimer();
        timer.Status = DomainTimerStatus.Stopped;
        timer.RemainingSeconds = timer.Mode == DomainTimerMode.Focus ? FocusDurationSeconds : BreakDurationSeconds;
        timer.UpdatedAt = DateTime.UtcNow;
        timer = await repository.UpdateAsync(timer);
        return MapToDto(timer);
    }

    public async Task<TimerDto> SwitchModeAsync(TimerModeDto mode)
    {
        var timer = await GetOrCreateTimer();
        var newMode = (DomainTimerMode)mode;

        timer.Mode = newMode;
        timer.Status = DomainTimerStatus.Stopped;
        timer.RemainingSeconds = newMode == DomainTimerMode.Focus ? FocusDurationSeconds : BreakDurationSeconds;
        timer.UpdatedAt = DateTime.UtcNow;

        timer = await repository.UpdateAsync(timer);
        return MapToDto(timer);
    }

    public async Task<TimerDto> SetTaskAsync(string? task)
    {
        var timer = await GetOrCreateTimer();
        timer.Task = task;
        timer.UpdatedAt = DateTime.UtcNow;
        timer = await repository.UpdateAsync(timer);
        return MapToDto(timer);
    }

    private async Task<DomainTimer> GetOrCreateTimer()
    {
        return await repository.GetCurrentAsync() ?? new DomainTimer { RemainingSeconds = FocusDurationSeconds };
    }

    private TimerDto MapToDto(DomainTimer timer) => new()
    {
        Id = timer.Id,
        Mode = (TimerModeDto)timer.Mode,
        RemainingSeconds = timer.RemainingSeconds,
        Status = (TimerStatusDto)timer.Status,
        Task = timer.Task
    };
}
