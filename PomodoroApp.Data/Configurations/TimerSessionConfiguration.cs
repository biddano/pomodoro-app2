using Microsoft.EntityFrameworkCore;
using PomodoroApp.Domain;

namespace PomodoroApp.Data.Configurations;

public static class TimerSessionConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TimerSession>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Mode).IsRequired();
            entity.Property(e => e.SecondsRemaining).IsRequired();
            entity.Property(e => e.IsRunning).IsRequired();
            entity.Property(e => e.KeyTask).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).IsRequired();
        });
    }
}
