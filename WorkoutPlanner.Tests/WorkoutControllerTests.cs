using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WorkoutPlanner.Controller;
using WorkoutPlanner.Response;
using WorkoutPlanner.Service.Exception;
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
        var actualResponse = await _workoutController.GetAllWorkouts();
        
        // Assert
        var okResult = Assert.IsType<Ok<List<WorkoutResponse>>>(actualResponse);
        var returnedWorkouts = Assert.IsType<List<WorkoutResponse>>(okResult.Value);
        
        Assert.Equal(expectedWorkoutResponses.Count, returnedWorkouts.Count);

        for (int i = 0; i < expectedWorkoutResponses.Count; i++)
        {
            Assert.Equal(returnedWorkouts[i].WorkoutId, expectedWorkoutResponses[i].WorkoutId);
            Assert.Equal(returnedWorkouts[i].Name, expectedWorkoutResponses[i].Name);
        }
    }

    [Fact]
    public async void GetWorkoutById_ValidWorkoutId_ReturnsOkWithWorkoutDetailResponse()
    {
        // Arrange
        int validWorkoutId = 1;
        var expectedWorkoutDetailResponse = new WorkoutDetailResponse() { WorkoutId = validWorkoutId, Name = "Running", Exercises = [] };

        _mockWorkoutService.Setup(ws => ws.GetWorkoutById(validWorkoutId)).ReturnsAsync(expectedWorkoutDetailResponse);
        
        // Act
        var actualResponse = await _workoutController.GetWorkoutById(validWorkoutId);
        
        // Assert
        var okResult = Assert.IsType<Ok<WorkoutDetailResponse>>(actualResponse.Result);
        var returnedWorkout = Assert.IsType<WorkoutDetailResponse>(okResult.Value);

        Assert.Equal(returnedWorkout.WorkoutId, expectedWorkoutDetailResponse.WorkoutId);
        Assert.Equal(returnedWorkout.Name, expectedWorkoutDetailResponse.Name);
        Assert.Equal(returnedWorkout.Exercises.Count, expectedWorkoutDetailResponse.Exercises.Count);
    }

    [Fact]
    public async void GetWorkoutById_InvalidWorkoutId_ThrowsWorkoutNotFoundException()
    {
        // Arrange
        int invalidWorkoutId = 1;
        var expectedException = new WorkoutNotFoundException();
    
        _mockWorkoutService.Setup(ws => ws.GetWorkoutById(invalidWorkoutId)).ThrowsAsync(expectedException);
        
        // Act & Assert
        await Assert.ThrowsAsync<WorkoutNotFoundException>(() => _workoutController.GetWorkoutById(invalidWorkoutId));
    }
}