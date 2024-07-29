using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WorkoutPlanner.Entity;

public class Set
{
    [Key]
    public int SetId { get; set; }
    
    [Required]
    public int Reps { get; set; }
    
    [Required]
    [Precision(6, 2)]
    public float Weight { get; set; }
    
    [Required]
    public string RepsType { get; set; }
    
    [Required]
    public string WeightType { get; set; }
    
    public ExerciseTerm ExerciseTerm { get; set; }
    public int ExerciseTermId { get; set; }
}