using WorkoutPlanner.Request;
using WorkoutPlanner.Response;

namespace WorkoutPlanner.Service.Interface;

public interface IExerciseService
{
    public Task<List<ExerciseResponse>> GetAllExercises();
    public Task<ExerciseResponse> CreateExercise(ExerciseRequest exerciseRequest);
    public Task DeleteExerciseById(int exerciseId);
    public Task UpdateExerciseById(int exerciseId, ExerciseRequest exerciseRequest);
}