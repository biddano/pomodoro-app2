using Microsoft.AspNetCore.Mvc;

namespace PomodoroApp.Features.Task;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet("current")]
    public ActionResult<TaskDto?> GetCurrent()
    {
        return Ok(_taskService.GetCurrentTask());
    }

    [HttpPost("create")]
    public ActionResult<TaskDto> Create([FromBody] CreateTaskRequest request)
    {
        return Ok(_taskService.CreateOrUpdateTask(request.Description));
    }

    [HttpPost("clear")]
    public ActionResult Clear()
    {
        _taskService.ClearTask();
        return Ok();
    }
}

public class CreateTaskRequest
{
    public string Description { get; set; } = string.Empty;
}
