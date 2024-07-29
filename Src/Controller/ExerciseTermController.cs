using Microsoft.AspNetCore.Mvc;
using WorkoutPlanner.Request;
using WorkoutPlanner.Service.Interface;

namespace WorkoutPlanner.Controller;

[ApiController]
[Route("exercise-terms")]
public class ExerciseTermController(IExerciseTermService exerciseTermService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllExerciseTerms()
    {
        return Ok(await exerciseTermService.GetAllExerciseTerms());
    }

    [HttpPost]
    public async Task<IActionResult> CreateExerciseTerm([FromBody] ExerciseTermRequest exerciseTermRequest)
    {
        try
        {
            var exerciseTermResponse = await exerciseTermService.CreateExerciseTerm(exerciseTermRequest);
            return Ok(exerciseTermResponse);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{exerciseTermId}")]
    public async Task<IActionResult> RemoveExerciseTermById(int exerciseTermId)
    {
        try
        {
            await exerciseTermService.DeleteExerciseTermById(exerciseTermId);
            return NoContent();
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPut("{exerciseTermId}")]
    public async Task<IActionResult> UpdateExerciseTermById(int exerciseTermId, [FromBody] ExerciseTermRequest exerciseTermRequest)
    {
        try
        {
            await exerciseTermService.UpdateExerciseTermById(exerciseTermId, exerciseTermRequest);
            return NoContent();
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
}