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

    [HttpGet("{exerciseId}")]
    public async Task<IActionResult> GetExerciseById(int exerciseId)
    {
        try
        {
            var exerciseResponse = await exerciseService.GetExerciseById(exerciseId);
            return Ok(exerciseResponse);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
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

    [HttpPut("{exerciseId}")]
    public async Task<IActionResult> UpdateExerciseById(int exerciseId, [FromBody] ExerciseRequest exerciseRequest)
    {
        try
        {
            await exerciseService.UpdateExerciseById(exerciseId, exerciseRequest);
            return NoContent();
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
}