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
}