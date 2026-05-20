using DomainTimer = PomodoroApp.Domain.Timer;

namespace PomodoroApp.Application.Abstractions;

public interface ITimerRepository
{
    Task<DomainTimer?> GetCurrentAsync();
    Task<DomainTimer> CreateAsync(DomainTimer timer);
    Task<DomainTimer> UpdateAsync(DomainTimer timer);
}
