using PomodoroApp.Data;

namespace PomodoroApp.Features.Timer;

public interface ITimerService
{
    TimerStateDto GetCurrentTimer();
    TimerStateDto StartTimer();
    TimerStateDto PauseTimer();
    TimerStateDto ResetTimer();
    TimerStateDto SwitchMode(string mode);
    void UpdateTimerCountdown();
}

public class TimerService : ITimerService
{
    private readonly PomodoroDbContext _context;
    private TimerSession? _currentSession;

    public const int FocusDurationSeconds = 25 * 60;
    public const int BreakDurationSeconds = 5 * 60;

    public TimerService(PomodoroDbContext context)
    {
        _context = context;
        _currentSession = _context.TimerSessions.OrderByDescending(s => s.CreatedAt).FirstOrDefault();

        if (_currentSession == null)
        {
            _currentSession = new TimerSession
            {
                Mode = "Focus",
                Duration = FocusDurationSeconds,
                TimeRemaining = FocusDurationSeconds,
                IsRunning = false,
                CreatedAt = DateTime.UtcNow
            };
            _context.TimerSessions.Add(_currentSession);
            _context.SaveChanges();
        }
    }

    public TimerStateDto GetCurrentTimer()
    {
        _currentSession ??= _context.TimerSessions.OrderByDescending(s => s.CreatedAt).FirstOrDefault()!;
        return MapToDto(_currentSession);
    }

    public TimerStateDto StartTimer()
    {
        if (_currentSession == null) throw new InvalidOperationException("No timer session found");

        _currentSession.IsRunning = true;
        _context.SaveChanges();
        return MapToDto(_currentSession);
    }

    public TimerStateDto PauseTimer()
    {
        if (_currentSession == null) throw new InvalidOperationException("No timer session found");

        _currentSession.IsRunning = false;
        _context.SaveChanges();
        return MapToDto(_currentSession);
    }

    public TimerStateDto ResetTimer()
    {
        if (_currentSession == null) throw new InvalidOperationException("No timer session found");

        _currentSession.IsRunning = false;
        _currentSession.TimeRemaining = _currentSession.Duration;
        _context.SaveChanges();
        return MapToDto(_currentSession);
    }

    public TimerStateDto SwitchMode(string mode)
    {
        if (mode != "Focus" && mode != "Break")
            throw new ArgumentException("Mode must be 'Focus' or 'Break'");

        if (_currentSession == null) throw new InvalidOperationException("No timer session found");

        _currentSession.Mode = mode;
        _currentSession.IsRunning = false;
        _currentSession.Duration = mode == "Focus" ? FocusDurationSeconds : BreakDurationSeconds;
        _currentSession.TimeRemaining = _currentSession.Duration;
        _context.SaveChanges();
        return MapToDto(_currentSession);
    }

    public void UpdateTimerCountdown()
    {
        if (_currentSession == null || !_currentSession.IsRunning) return;

        if (_currentSession.TimeRemaining > 0)
        {
            _currentSession.TimeRemaining--;
        }
        else
        {
            _currentSession.IsRunning = false;
        }

        _context.SaveChanges();
    }

    private static TimerStateDto MapToDto(TimerSession session) =>
        new()
        {
            Id = session.Id,
            Mode = session.Mode,
            Duration = session.Duration,
            TimeRemaining = session.TimeRemaining,
            IsRunning = session.IsRunning,
            DisplayTime = FormatTime(session.TimeRemaining)
        };

    private static string FormatTime(int seconds)
    {
        var minutes = seconds / 60;
        var secs = seconds % 60;
        return $"{minutes:D2}:{secs:D2}";
    }
}

public class TimerStateDto
{
    public int Id { get; set; }
    public string Mode { get; set; } = string.Empty;
    public int Duration { get; set; }
    public int TimeRemaining { get; set; }
    public bool IsRunning { get; set; }
    public string DisplayTime { get; set; } = "00:00";
}
