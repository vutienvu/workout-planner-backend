using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Entity;

namespace WorkoutPlanner.Helper;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<Workout> Workouts { get; set; } = null!;
    public DbSet<Exercise> Exercises { get; set; } = null!;
    public DbSet<ExerciseTerm> ExerciseTerms { get; set; } = null!;
    public DbSet<Set> Sets { get; set; } = null!;
}