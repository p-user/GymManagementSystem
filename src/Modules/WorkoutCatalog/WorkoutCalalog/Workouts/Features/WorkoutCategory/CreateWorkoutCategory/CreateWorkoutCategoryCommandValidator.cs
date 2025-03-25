namespace WorkoutCatalog.Workouts.Features.WorkoutCategory.CreateWorkoutCategory
{
    public class CreateWorkoutCategoryCommandValidator : AbstractValidator<CreateWorkoutCategoryCommand>
    {
        public CreateWorkoutCategoryCommandValidator()
        {
            RuleFor(x => x.dto.Name)
           .NotEmpty().WithMessage("Workout category name is required.")
           .MaximumLength(100).WithMessage("Workout category name cannot exceed 100 characters.");

            RuleFor(x => x.dto.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
        }
    }
}
