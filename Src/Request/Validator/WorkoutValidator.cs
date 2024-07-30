using FluentValidation;

namespace WorkoutPlanner.Request.Validator;

public class WorkoutValidator : AbstractValidator<WorkoutRequest>
{
    public WorkoutValidator()
    {
        RuleFor(workout => workout.Name).NotEmpty().WithMessage("Workout {PropertyName} should not be empty.");
    }
}