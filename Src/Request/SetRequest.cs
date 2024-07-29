namespace WorkoutPlanner.Request;

public class SetRequest
{
    public int Reps { get; set; }
    public double Weight { get; set; }
    public string RepsType { get; set; }
    public string WeightType { get; set; }
    public int ExerciseTermId { get; set; }
}