namespace WorkoutPlanner.Response;

public class WorkoutResponse
{
    public int WorkoutId { get; set; }
    public string Name { get; set; }
    public List<ExerciseResponse> Exercises { get; set; }
}