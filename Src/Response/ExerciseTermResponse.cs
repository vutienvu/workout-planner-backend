namespace WorkoutPlanner.Response;

public class ExerciseTermResponse
{
    public int ExerciseTermId { get; set; }
    public DateTime TermDate { get; set; }
    public int TotalSets { get; set; }
    public int ExerciseId { get; set; }
    public List<SetResponse> Sets { get; set; }
}