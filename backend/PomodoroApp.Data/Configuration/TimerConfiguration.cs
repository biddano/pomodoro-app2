using Microsoft.EntityFrameworkCore;
using DomainTimer = PomodoroApp.Domain.Timer;

namespace PomodoroApp.Data.Configuration;

public static class TimerConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DomainTimer>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Mode)
                .HasConversion<int>();

            entity.Property(e => e.Status)
                .HasConversion<int>();

            entity.Property(e => e.Task)
                .HasMaxLength(500)
                .IsRequired(false);

            entity.Property(e => e.CreatedAt)
                .HasDefaultValue(DateTime.UtcNow);

            entity.Property(e => e.UpdatedAt)
                .IsRequired(false);
        });
    }
}
