namespace PomodoroApp.Domain;

public class Timer
{
    public int Id { get; set; }
    public TimerMode Mode { get; set; } = TimerMode.Focus;
    public int RemainingSeconds { get; set; }
    public TimerStatus Status { get; set; } = TimerStatus.Stopped;
    public string? Task { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}

public enum TimerMode
{
    Focus = 0,
    Break = 1
}

public enum TimerStatus
{
    Stopped = 0,
    Running = 1,
    Paused = 2
}
