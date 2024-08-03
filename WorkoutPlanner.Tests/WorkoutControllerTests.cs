using Microsoft.AspNetCore.Mvc;
using Moq;
using WorkoutPlanner.Controller;
using WorkoutPlanner.Response;
using WorkoutPlanner.Service.Interface;

namespace WorkoutPlanner.Tests;

public class WorkoutControllerTests
{
    private readonly Mock<IWorkoutService> _mockWorkoutService;
    private readonly WorkoutController _workoutController;

    public WorkoutControllerTests()
    {
        _mockWorkoutService = new Mock<IWorkoutService>();
        _workoutController = new WorkoutController(_mockWorkoutService.Object);
    }
    
    
    [Fact]
    public async void GetAllWorkouts_ValidFlow_ReturnsOkWithListOfWorkoutResponses()
    {
        // Arrange
        var expectedWorkoutResponses = new List<WorkoutResponse>() { new WorkoutResponse() { WorkoutId = 1, Name = "Running" }, new WorkoutResponse() { WorkoutId = 2, Name = "Climbing" } };

        _mockWorkoutService.Setup(ws => ws.GetAllWorkouts()).ReturnsAsync(expectedWorkoutResponses);
        
        // Act
        IActionResult actualResponse = await _workoutController.GetAllWorkouts();
        
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actualResponse);
        var returnedWorkouts = Assert.IsType<List<WorkoutResponse>>(okResult.Value);
        
        Assert.Equal(expectedWorkoutResponses.Count, returnedWorkouts.Count);

        for (int i = 0; i < expectedWorkoutResponses.Count; i++)
        {
            Assert.Equal(expectedWorkoutResponses[i].WorkoutId, returnedWorkouts[i].WorkoutId);
            Assert.Equal(expectedWorkoutResponses[i].Name, returnedWorkouts[i].Name);
        }
    }

}