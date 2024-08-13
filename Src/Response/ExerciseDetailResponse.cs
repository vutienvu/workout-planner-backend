namespace WorkoutPlanner.Response;

public class ExerciseDetailResponse : ExerciseResponse
{
    public List<ExerciseTermResponse> ExerciseTerms { get; set; }
}