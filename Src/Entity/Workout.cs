using System.ComponentModel.DataAnnotations;

namespace WorkoutPlanner.Entity;

public class Workout
{
    [Key]
    public int WorkoutId { get; set; }
    
    [Required]
    public string Name { get; set; }

    public List<Exercise> Exercises { get; set; } = new List<Exercise>();
}