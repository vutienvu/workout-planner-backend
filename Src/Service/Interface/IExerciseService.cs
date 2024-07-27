using WorkoutPlanner.Response;

namespace WorkoutPlanner.Service.Interface;

public interface IExerciseService
{
    public Task<List<ExerciseResponse>> GetAllExercises();
}