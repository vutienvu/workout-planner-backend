using Microsoft.AspNetCore.Mvc;
using WorkoutPlanner.Request;
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

    [HttpPost(Name = "Create workout")]
    public async Task<IActionResult> CreateWorkout([FromBody] WorkoutRequest workoutRequest)
    {
        var workoutResponse = await workoutService.CreateWorkout(workoutRequest);
        return Ok(workoutResponse);
    }
}