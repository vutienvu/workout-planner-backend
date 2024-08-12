namespace WorkoutPlanner.Response;

public class WorkoutDetailResponse : WorkoutResponse
{
    public List<ExerciseResponse> Exercises { get; set; }
}