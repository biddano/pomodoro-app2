using PomodoroApp.Domain;

namespace PomodoroApp.Application.DTOs;

public class TimerSessionDto
{
    public int Id { get; set; }
    public TimerMode Mode { get; set; }
    public int SecondsRemaining { get; set; }
    public bool IsRunning { get; set; }
    public string? KeyTask { get; set; }
}
