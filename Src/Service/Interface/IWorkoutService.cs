using WorkoutPlanner.Request;
using WorkoutPlanner.Response;

namespace WorkoutPlanner.Service.Interface;

public interface IWorkoutService
{
    public Task<List<WorkoutResponse>> GetAllWorkouts();
    public Task<WorkoutResponse> CreateWorkout(WorkoutRequest workoutRequest);
    public Task<WorkoutDetailResponse> GetWorkoutById(int workoutId);
    public Task DeleteWorkoutById(int workoutId);
    public Task UpdateWorkoutById(int workoutId, WorkoutRequest workoutRequest);
}