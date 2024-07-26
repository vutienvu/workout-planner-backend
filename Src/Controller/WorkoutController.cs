using Microsoft.AspNetCore.Mvc;
using WorkoutPlanner.Service.Interface;

namespace WorkoutPlanner.Controller;

[ApiController]
[Route("[controller]")]
public class WorkoutController(IWorkoutService workoutService) : ControllerBase
{
    [HttpGet(Name = "Get workouts")]
    public async Task<IActionResult> GetAllWorkouts()
    {
        return Ok(await workoutService.GetAllWorkouts());
    }
}