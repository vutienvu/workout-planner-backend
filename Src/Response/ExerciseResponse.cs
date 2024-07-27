namespace WorkoutPlanner.Response;

public class ExerciseResponse
{
    public int ExerciseId { get; set; }
    public string Name { get; set; }
    public int PauseDuration { get; set; }
    public int WorkoutId { get; set; }
}