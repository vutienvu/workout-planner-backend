using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WorkoutPlanner.Request;
using WorkoutPlanner.Response;
using WorkoutPlanner.Service.Interface;

namespace WorkoutPlanner.Controller;

public class WorkoutController(IWorkoutService workoutService) : BaseController
{
    [HttpGet]
    public async Task<Ok<List<WorkoutResponse>>> GetAllWorkouts()
    {
        return Ok(await workoutService.GetAllWorkouts());
    }

    [HttpPost]
    public async Task<Ok<WorkoutResponse>> CreateWorkout([FromBody] WorkoutRequest workoutRequest)
    {
        return Ok(await workoutService.CreateWorkout(workoutRequest));
    }
    
    [HttpGet("{workoutId}")]
    public async Task<Results<Ok<WorkoutDetailResponse>, NotFound>> GetWorkoutById(int workoutId)
    {
        return Ok(await workoutService.GetWorkoutById(workoutId));
    }

    [HttpDelete("{workoutId}")]
    public async Task<Results<NoContent, NotFound>> RemoveWorkoutById(int workoutId)
    {
        await workoutService.DeleteWorkoutById(workoutId);
        return NoContent();
    }

    [HttpPut("{workoutId}")]
    public async Task<Results<NoContent, NotFound>> UpdateWorkoutById(int workoutId, [FromBody] WorkoutRequest workoutRequest)
    {
        await workoutService.UpdateWorkoutById(workoutId, workoutRequest);
        return NoContent();
    }
}