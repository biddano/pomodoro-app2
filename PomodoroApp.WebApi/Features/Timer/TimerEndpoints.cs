using PomodoroApp.Application.Abstractions;
using PomodoroApp.Application.DTOs;
using PomodoroApp.Domain;

namespace PomodoroApp.WebApi.Features.Timer;

public static class TimerEndpoints
{
    public static void MapTimerEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/timer")
            .WithName("Timer")
            .WithOpenApi();

        group.MapGet("/state", GetCurrentState)
            .WithName("GetTimerState")
            .WithDescription("Get the current timer state");

        group.MapPost("/start", StartTimer)
            .WithName("StartTimer")
            .WithDescription("Start the timer");

        group.MapPost("/pause", PauseTimer)
            .WithName("PauseTimer")
            .WithDescription("Pause the timer");

        group.MapPost("/reset", ResetTimer)
            .WithName("ResetTimer")
            .WithDescription("Reset the timer");

        group.MapPost("/mode/{mode}", SwitchMode)
            .WithName("SwitchMode")
            .WithDescription("Switch between focus and break modes");

        group.MapPost("/task", SetKeyTask)
            .WithName("SetKeyTask")
            .WithDescription("Set the key task for the session");

        group.MapPost("/tick", Tick)
            .WithName("Tick")
            .WithDescription("Advance the timer by one second");
    }

    private static async Task<TimerSessionDto> GetCurrentState(ITimerService timerService)
    {
        var session = await timerService.GetCurrentStateAsync();
        return MapToDto(session);
    }

    private static async Task<TimerSessionDto> StartTimer(ITimerService timerService)
    {
        var session = await timerService.StartTimerAsync();
        return MapToDto(session);
    }

    private static async Task<TimerSessionDto> PauseTimer(ITimerService timerService)
    {
        var session = await timerService.PauseTimerAsync();
        return MapToDto(session);
    }

    private static async Task<TimerSessionDto> ResetTimer(ITimerService timerService)
    {
        var session = await timerService.ResetTimerAsync();
        return MapToDto(session);
    }

    private static async Task<TimerSessionDto> SwitchMode(ITimerService timerService, string mode)
    {
        var timerMode = Enum.Parse<TimerMode>(mode);
        var session = await timerService.SwitchModeAsync(timerMode);
        return MapToDto(session);
    }

    private static async Task<TimerSessionDto> SetKeyTask(ITimerService timerService, SetKeyTaskRequest request)
    {
        var session = await timerService.SetKeyTaskAsync(request.Task);
        return MapToDto(session);
    }

    private static async Task<TimerSessionDto> Tick(ITimerService timerService)
    {
        var session = await timerService.TickAsync();
        return MapToDto(session);
    }

    private static TimerSessionDto MapToDto(Domain.TimerSession session) => new()
    {
        Id = session.Id,
        Mode = session.Mode,
        SecondsRemaining = session.SecondsRemaining,
        IsRunning = session.IsRunning,
        KeyTask = session.KeyTask
    };
}

public class SetKeyTaskRequest
{
    public string? Task { get; set; }
}
