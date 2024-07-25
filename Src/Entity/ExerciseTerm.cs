using System.ComponentModel.DataAnnotations;

namespace WorkoutPlanner.Entity;

public class ExerciseTerm
{
    [Key]
    public int ExerciseTermId { get; set; }
    
    public DateTime TermDate { get; set; } = DateTime.Now;

    [Required]
    public int TotalSets { get; set; }
    
    public Exercise Exercise { get; set; }
    public int ExerciseId { get; set; }

    public List<Set> Sets { get; set; } = new List<Set>();
}