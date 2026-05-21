using Microsoft.EntityFrameworkCore;
using PomodoroApp.Domain;
using PomodoroApp.Data.Configurations;

namespace PomodoroApp.Data;

public class PomodoroDbContext(DbContextOptions<PomodoroDbContext> options) : DbContext(options)
{
    public DbSet<TimerSession> TimerSessions => Set<TimerSession>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        TimerSessionConfiguration.Configure(modelBuilder);
    }
}
