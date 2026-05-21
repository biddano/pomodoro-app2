namespace PomodoroApp.Domain;

public class TimerSession
{
    public int Id { get; set; }
    public TimerMode Mode { get; set; } = TimerMode.Focus;
    public int SecondsRemaining { get; set; } = 1500; // 25 minutes
    public bool IsRunning { get; set; } = false;
    public string? KeyTask { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastUpdatedAt { get; set; }
}
