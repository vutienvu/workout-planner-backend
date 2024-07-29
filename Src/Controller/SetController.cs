using Microsoft.AspNetCore.Mvc;
using WorkoutPlanner.Request;
using WorkoutPlanner.Service.Interface;

namespace WorkoutPlanner.Controller;

[ApiController]
[Route("sets")]
public class SetController(ISetService setService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllSets()
    {
        return Ok(await setService.GetAllSets());
    }

    [HttpPost]
    public async Task<IActionResult> CreateSet([FromBody] SetRequest setRequest)
    {
        try
        {
            var setResponse = await setService.CreateSet(setRequest);
            return Ok(setResponse);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{setId}")]
    public async Task<IActionResult> RemoveSetById(int setId)
    {
        try
        {
            await setService.DeleteSetById(setId);
            return NoContent();
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPut("{setId}")]
    public async Task<IActionResult> UpdateSetById(int setId, [FromBody] SetRequest setRequest)
    {
        try
        {
            await setService.UpdateSetById(setId, setRequest);
            return NoContent();
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
}