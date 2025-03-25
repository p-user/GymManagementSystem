namespace WorkoutCatalog.Workouts.Features.ExerciseCategory.UpdateExerciseCategory
{
    public class UpdateExerciseCategoryCommandValidator : AbstractValidator<UpdateExerciseCategoryCommand>
    {
        public UpdateExerciseCategoryCommandValidator()
        {
            RuleFor(x => x.dto.Name)
           .NotEmpty().WithMessage("Exercise category name is required.")
           .MaximumLength(100).WithMessage("Exercise category name cannot exceed 100 characters.");

            RuleFor(x => x.dto.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
        }
    }
}
