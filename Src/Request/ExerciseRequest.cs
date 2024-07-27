namespace WorkoutPlanner.Request;

public class ExerciseRequest
{
    public string Name { get; set; }
    public int PauseDuration { get; set; }
    public int WorkoutId { get; set; }
}