using Microsoft.EntityFrameworkCore;
using DomainTimer = PomodoroApp.Domain.Timer;
using PomodoroApp.Data.Configuration;

namespace PomodoroApp.Data;

public class PomodoroDbContext(DbContextOptions<PomodoroDbContext> options) : DbContext(options)
{
    public DbSet<DomainTimer> Timers => Set<DomainTimer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        TimerConfiguration.Configure(modelBuilder);
    }
}
