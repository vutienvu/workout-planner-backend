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
}