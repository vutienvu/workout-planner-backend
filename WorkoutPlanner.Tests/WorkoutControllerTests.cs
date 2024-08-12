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

    [Fact]
    public async void GetWorkoutById_ValidWorkoutId_ReturnsOkWithWorkoutResponse()
    {
        // Arrange
        int validWorkoutId = 1;
        var expectedWorkoutDetailResponse = new WorkoutDetailResponse() { WorkoutId = validWorkoutId, Name = "Running", Exercises = [] };

        _mockWorkoutService.Setup(ws => ws.GetWorkoutById(validWorkoutId)).ReturnsAsync(expectedWorkoutDetailResponse);
        
        // Act
        var actualResponse = await _workoutController.GetWorkoutById(validWorkoutId);
        
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actualResponse);
        var returnedWorkout = Assert.IsType<WorkoutDetailResponse>(okResult.Value);
        
        Assert.Equal(expectedWorkoutDetailResponse.WorkoutId, returnedWorkout.WorkoutId);
        Assert.Equal(expectedWorkoutDetailResponse.Name, returnedWorkout.Name);
        Assert.Equal(expectedWorkoutDetailResponse.Exercises.Count, returnedWorkout.Exercises.Count);
    }

    [Fact]
    public async void GetWorkoutById_InvalidWorkoutId_ReturnsNotFoundWithExceptionMessage()
    {
        // Arrange
        int invalidWorkoutId = 1;
        var expectedException = new Exception("No workout with such id.");

        _mockWorkoutService.Setup(ws => ws.GetWorkoutById(invalidWorkoutId)).ThrowsAsync(expectedException);
        
        // Act
        var actualResponse = await _workoutController.GetWorkoutById(invalidWorkoutId);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(actualResponse);
        var actualExceptionMessage = Assert.IsType<string>(notFoundResult.Value);
        
        Assert.Equal(expectedException.Message, actualExceptionMessage);
    }
}