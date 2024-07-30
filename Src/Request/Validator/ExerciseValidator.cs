using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Helper;

namespace WorkoutPlanner.Request.Validator;

public class ExerciseValidator : AbstractValidator<ExerciseRequest>
{
    private readonly DatabaseContext _databaseContext;
    
    public ExerciseValidator(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        
        RuleFor(e => e.Name).NotEmpty().WithMessage("Exercise {PropertyName} should not be empty");
        RuleFor(e => e.PauseDuration).GreaterThan(0).WithMessage("Exercise {PropertyName} should be greater than 0.");
        RuleFor(e => e.WorkoutId).MustAsync(WorkoutExists).WithMessage("Workout with {PropertyName} {PropertyValue} doesn't exist.");
    }

    private async Task<bool> WorkoutExists(int workoutId, CancellationToken cancellationToken = default)
    {
        return await _databaseContext.Workouts.AnyAsync(w => w.WorkoutId == workoutId, cancellationToken);
    }
}