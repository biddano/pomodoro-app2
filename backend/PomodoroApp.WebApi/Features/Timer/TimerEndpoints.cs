using PomodoroApp.Application.Abstractions;
using PomodoroApp.Application.DTOs;

namespace PomodoroApp.WebApi.Features.Timer;

public static class TimerEndpoints
{
    public static void MapEndpoints(WebApplication app)
    {
        var group = app.MapGroup("/api/timer");

        group.MapGet("/", GetCurrent);
        group.MapPost("/start", Start);
        group.MapPost("/pause", Pause);
        group.MapPost("/resume", Resume);
        group.MapPost("/stop", Stop);
        group.MapPost("/reset", Reset);
        group.MapPost("/switch-mode", SwitchMode);
        group.MapPost("/set-task", SetTask);
    }

    private static async Task<TimerDto> GetCurrent(ITimerService timerService)
    {
        return await timerService.GetCurrentAsync();
    }

    private static async Task<TimerDto> Start(ITimerService timerService, StartTimerRequest request)
    {
        return await timerService.StartAsync(request.Task);
    }

    private static async Task<TimerDto> Pause(ITimerService timerService)
    {
        return await timerService.PauseAsync();
    }

    private static async Task<TimerDto> Resume(ITimerService timerService)
    {
        return await timerService.ResumeAsync();
    }

    private static async Task<TimerDto> Stop(ITimerService timerService)
    {
        return await timerService.StopAsync();
    }

    private static async Task<TimerDto> Reset(ITimerService timerService)
    {
        return await timerService.ResetAsync();
    }

    private static async Task<TimerDto> SwitchMode(ITimerService timerService, SwitchModeRequest request)
    {
        return await timerService.SwitchModeAsync(request.Mode);
    }

    private static async Task<TimerDto> SetTask(ITimerService timerService, SetTaskRequest request)
    {
        return await timerService.SetTaskAsync(request.Task);
    }
}

public record StartTimerRequest(string? Task);
public record SwitchModeRequest(TimerModeDto Mode);
public record SetTaskRequest(string? Task);
