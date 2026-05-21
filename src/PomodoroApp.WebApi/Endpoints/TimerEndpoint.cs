namespace PomodoroApp.WebApi.Endpoints;

public class GetTimerStatusRequest { }

public class GetTimerStatusResponse
{
    public string Mode { get; set; } = "Focus";
    public int RemainingSeconds { get; set; } = 1500;
    public bool IsRunning { get; set; } = false;
}

public class TimerEndpoint
{
    public static void MapTimerEndpoints(this WebApplication app)
    {
        app.MapGet("/api/timer/status", GetTimerStatus)
            .WithName("GetTimerStatus")
            .WithOpenApi();
    }

    private static GetTimerStatusResponse GetTimerStatus()
    {
        return new GetTimerStatusResponse
        {
            Mode = "Focus",
            RemainingSeconds = 1500,
            IsRunning = false
        };
    }
}
