using System.ComponentModel.DataAnnotations;

namespace WorkoutPlanner.Entity;

public class Exercise
{
    [Key]
    public int ExerciseId { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public int PauseDuration { get; set; }
    
    public Workout Workout { get; set; }
    public int WorkoutId { get; set; }

    public List<ExerciseTerm> ExerciseTerms { get; set; } = new List<ExerciseTerm>();
}