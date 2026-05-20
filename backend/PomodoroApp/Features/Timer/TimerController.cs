using Microsoft.AspNetCore.Mvc;

namespace PomodoroApp.Features.Timer;

[ApiController]
[Route("api/[controller]")]
public class TimerController : ControllerBase
{
    private readonly ITimerService _timerService;

    public TimerController(ITimerService timerService)
    {
        _timerService = timerService;
    }

    [HttpGet("current")]
    public ActionResult<TimerStateDto> GetCurrent()
    {
        return Ok(_timerService.GetCurrentTimer());
    }

    [HttpPost("start")]
    public ActionResult<TimerStateDto> Start()
    {
        return Ok(_timerService.StartTimer());
    }

    [HttpPost("pause")]
    public ActionResult<TimerStateDto> Pause()
    {
        return Ok(_timerService.PauseTimer());
    }

    [HttpPost("reset")]
    public ActionResult<TimerStateDto> Reset()
    {
        return Ok(_timerService.ResetTimer());
    }

    [HttpPost("switch-mode")]
    public ActionResult<TimerStateDto> SwitchMode([FromQuery] string mode)
    {
        return Ok(_timerService.SwitchMode(mode));
    }
}
