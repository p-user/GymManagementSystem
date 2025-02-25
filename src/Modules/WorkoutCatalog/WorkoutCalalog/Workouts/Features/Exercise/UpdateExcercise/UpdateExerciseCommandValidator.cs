
using FluentValidation;

namespace WorkoutCatalog.Workouts.Features.Exercise.UpdateExcercise
{
    public class UpdateExerciseCommandValidator : AbstractValidator<UpdateExerciseCommand>
    {
        public UpdateExerciseCommandValidator()
        {

            RuleFor(x => x.Dto.Name)
           .NotEmpty().WithMessage("Exercise name is required.")
           .MaximumLength(100).WithMessage("Exercise name cannot exceed 100 characters.");

            RuleFor(x => x.Dto.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.Dto.DescriptionLink)
                .Must(link => string.IsNullOrEmpty(link) || Uri.IsWellFormedUriString(link, UriKind.Absolute))
                .WithMessage("Description link must be a valid URL.");

            RuleFor(x => x.Dto.ExerciseCategory)
                .NotEmpty().WithMessage("Exercise category is required.");
        }
    }
}
