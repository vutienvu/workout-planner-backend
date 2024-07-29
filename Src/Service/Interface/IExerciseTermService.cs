using WorkoutPlanner.Request;
using WorkoutPlanner.Response;

namespace WorkoutPlanner.Service.Interface;

public interface IExerciseTermService
{
    public Task<List<ExerciseTermResponse>> GetAllExerciseTerms();
    public Task<ExerciseTermResponse> CreateExerciseTerm(ExerciseTermRequest exerciseTermRequest);
    public Task DeleteExerciseTermById(int exerciseTermId);
    public Task UpdateExerciseTermById(int exerciseTermId, ExerciseTermRequest exerciseTermRequest);
}