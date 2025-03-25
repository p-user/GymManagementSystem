namespace WorkoutCatalog.Workouts.Features.Workout.CreateWorkout
{
    public class CreateWorkoutCommandValidator : AbstractValidator<CreateWorkoutCommand>
    {
        public CreateWorkoutCommandValidator()
        {
            RuleFor(x => x.dto.Name)
            .NotEmpty().WithMessage("Workout name is required.")
            .MaximumLength(100).WithMessage("Workout name cannot exceed 100 characters.");


            RuleFor(x => x.dto.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.dto.Exercises)
                .NotEmpty().WithMessage("At least one exercise must be included.");

            RuleForEach(x => x.dto.Exercises)
                .Must(id => id != Guid.Empty).WithMessage("Each exercise must have a valid ID.");


            RuleForEach(x => x.dto.Categories)
                .Must(id => id != Guid.Empty).WithMessage("Each category must have a valid ID.");
        }
    }
}
