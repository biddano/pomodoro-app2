namespace PomodoroApp.Application.DTOs;

public class TimerDto
{
    public int Id { get; set; }
    public TimerModeDto Mode { get; set; }
    public int RemainingSeconds { get; set; }
    public TimerStatusDto Status { get; set; }
    public string? Task { get; set; }
}

public enum TimerModeDto
{
    Focus = 0,
    Break = 1
}

public enum TimerStatusDto
{
    Stopped = 0,
    Running = 1,
    Paused = 2
}
