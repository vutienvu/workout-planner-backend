using Microsoft.AspNetCore.Mvc;
using WorkoutPlanner.Request;
using WorkoutPlanner.Service.Interface;

namespace WorkoutPlanner.Controller;

[ApiController]
[Route("exercises")]
public class ExerciseController(IExerciseService exerciseService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllExercises()
    {
        return Ok(await exerciseService.GetAllExercises());
    }

    [HttpPost]
    public async Task<IActionResult> CreateExercise([FromBody] ExerciseRequest exerciseRequest)
    {
        try
        {
            var exerciseResponse = await exerciseService.CreateExercise(exerciseRequest);
            return Ok(exerciseResponse);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{exerciseId}")]
    public async Task<IActionResult> RemoveExerciseById(int exerciseId)
    {
        try
        {
            await exerciseService.DeleteExerciseById(exerciseId);
            return NoContent();
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    } 
}