using Microsoft.EntityFrameworkCore;

namespace PomodoroApp.Data;

public class PomodoroDbContext : DbContext
{
    public PomodoroDbContext(DbContextOptions<PomodoroDbContext> options) : base(options) { }

    public DbSet<TimerSession> TimerSessions { get; set; }
    public DbSet<TaskItem> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TimerSession>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Mode).IsRequired();
            entity.Property(e => e.Duration).IsRequired();
            entity.Property(e => e.TimeRemaining).IsRequired();
            entity.Property(e => e.IsRunning).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
        });

        modelBuilder.Entity<TaskItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).IsRequired();
        });
    }
}

public class TimerSession
{
    public int Id { get; set; }
    public string Mode { get; set; } = "Focus";
    public int Duration { get; set; }
    public int TimeRemaining { get; set; }
    public bool IsRunning { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class TaskItem
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public int? SessionId { get; set; }
    public DateTime CreatedAt { get; set; }
}
