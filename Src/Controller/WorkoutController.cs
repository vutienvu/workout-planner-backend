using Microsoft.AspNetCore.Mvc;
using WorkoutPlanner.Request;
using WorkoutPlanner.Service.Interface;

namespace WorkoutPlanner.Controller;

[ApiController]
[Route("workouts")]
public class WorkoutController(IWorkoutService workoutService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllWorkouts()
    {
        return Ok(await workoutService.GetAllWorkouts());
    }

    [HttpPost]
    public async Task<IActionResult> CreateWorkout([FromBody] WorkoutRequest workoutRequest)
    {
        var workoutResponse = await workoutService.CreateWorkout(workoutRequest);
        return Ok(workoutResponse);
    }
    
    [HttpGet("{workoutId}")]
    public async Task<IActionResult> GetWorkoutById(int workoutId)
    {
        try
        {
            var workoutResponse = await workoutService.GetWorkoutById(workoutId);
            return Ok(workoutResponse);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{workoutId}")]
    public async Task<IActionResult> RemoveWorkoutById(int workoutId)
    {
        try
        {
            await workoutService.DeleteWorkoutById(workoutId);
            return NoContent();
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPut("{workoutId}")]
    public async Task<IActionResult> UpdateWorkoutById(int workoutId, [FromBody] WorkoutRequest workoutRequest)
    {
        try
        {
            await workoutService.UpdateWorkoutById(workoutId, workoutRequest);
            return NoContent();
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
}