using PomodoroApp.Data;

namespace PomodoroApp.Features.Task;

public interface ITaskService
{
    TaskDto? GetCurrentTask();
    TaskDto CreateOrUpdateTask(string description);
    void ClearTask();
}

public class TaskService : ITaskService
{
    private readonly PomodoroDbContext _context;

    public TaskService(PomodoroDbContext context)
    {
        _context = context;
    }

    public TaskDto? GetCurrentTask()
    {
        var task = _context.Tasks.OrderByDescending(t => t.CreatedAt).FirstOrDefault();
        return task == null ? null : MapToDto(task);
    }

    public TaskDto CreateOrUpdateTask(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Task description cannot be empty");

        var existingTask = _context.Tasks.OrderByDescending(t => t.CreatedAt).FirstOrDefault();

        if (existingTask != null)
        {
            existingTask.Description = description;
            _context.Tasks.Update(existingTask);
        }
        else
        {
            var newTask = new TaskItem
            {
                Description = description,
                CreatedAt = DateTime.UtcNow
            };
            _context.Tasks.Add(newTask);
        }

        _context.SaveChanges();
        var task = _context.Tasks.OrderByDescending(t => t.CreatedAt).First();
        return MapToDto(task);
    }

    public void ClearTask()
    {
        var task = _context.Tasks.OrderByDescending(t => t.CreatedAt).FirstOrDefault();
        if (task != null)
        {
            task.Description = null;
            _context.Tasks.Update(task);
            _context.SaveChanges();
        }
    }

    private static TaskDto MapToDto(TaskItem task) =>
        new()
        {
            Id = task.Id,
            Description = task.Description
        };
}

public class TaskDto
{
    public int Id { get; set; }
    public string? Description { get; set; }
}
