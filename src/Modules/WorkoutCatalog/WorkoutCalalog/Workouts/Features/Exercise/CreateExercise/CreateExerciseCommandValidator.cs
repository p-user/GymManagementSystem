namespace WorkoutCatalog.Workouts.Features.Exercise.CreateExercise
{
    public class CreateExerciseCommandValidator : AbstractValidator<CreateExerciseCommand>
    {
        public CreateExerciseCommandValidator()
        {
            RuleFor(x => x.dto.Name)
          .NotEmpty().WithMessage("Exercise name is required.")
          .MaximumLength(100).WithMessage("Exercise name cannot exceed 100 characters.");

            RuleFor(x => x.dto.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.dto.DescriptionLink)
                .Must(link => string.IsNullOrEmpty(link) || Uri.IsWellFormedUriString(link, UriKind.Absolute))
                .WithMessage("Description link must be a valid URL.");

            RuleFor(x => x.dto.MuscleGroups)
                .NotEmpty().WithMessage("At least one muscle group is required.");

            RuleForEach(x => x.dto.MuscleGroups)
                .Must(mg => mg != null && mg.Id != Guid.Empty)
                .WithMessage("Each muscle group must have a valid ID.");

            RuleFor(x => x.dto.ExerciseCategory)
                .NotEmpty().WithMessage("Exercise category is required.");
        }
    }
}
