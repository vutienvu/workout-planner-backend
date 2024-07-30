using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Helper;

namespace WorkoutPlanner.Request.Validator;

public class SetValidator : AbstractValidator<SetRequest>
{
    private readonly DatabaseContext _databaseContext;
    
    public SetValidator(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;

        RuleFor(s => s.Reps).GreaterThan(0).WithMessage("Set {PropertyName} should be greater than 0.");
        RuleFor(s => s.Weight).GreaterThan(0).WithMessage("Set {PropertyName} should be greater than 0.");
        RuleFor(s => s.RepsType).Must(rt => rt is "repetition" or "seconds").WithMessage("RepsType must be either 'repetition' or 'seconds'");
        RuleFor(s => s.WeightType).Must(wt => wt is "kg" or "lb").WithMessage("WeightType must be either 'kg' or 'lb'.");
        RuleFor(s => s.ExerciseTermId).MustAsync(ExerciseTermExists).WithMessage("Set with {PropertyName} {PropertyValue} doesn't exist.");
    }

    public async Task<bool> ExerciseTermExists(int exerciseTermId, CancellationToken cancellationToken)
    {
        return await _databaseContext.ExerciseTerms.AnyAsync(et => et.ExerciseTermId == exerciseTermId, cancellationToken);
    }
}