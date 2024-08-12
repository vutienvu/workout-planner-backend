namespace WorkoutPlanner.Response;

public class SetResponse
{
    public int SetId { get; set; }
    public int Reps { get; set; }
    public float Weight { get; set; }
    public string RepsType { get; set; }
    public string WeightType { get; set; }
    public int ExerciseTermId { get; set; }
}