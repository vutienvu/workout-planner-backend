using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Helper;

namespace WorkoutPlanner.Request.Validator;

public class ExerciseTermValidator : AbstractValidator<ExerciseTermRequest>
{
    private readonly DatabaseContext _databaseContext;
    
    public ExerciseTermValidator(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;

        RuleFor(et => et.TotalSets).GreaterThan(0).WithMessage("Exercise term {PropertyName} should be greater than 0.");
        RuleFor(et => et.ExerciseId).MustAsync(ExerciseExists).WithMessage("Exercise with {PropertyName} {PropertyValue} doesn't exist.");
    }

    public async Task<bool> ExerciseExists(int exerciseId, CancellationToken cancellationToken)
    {
        return await _databaseContext.Exercises.AnyAsync(e => e.ExerciseId == exerciseId, cancellationToken);
    }
}