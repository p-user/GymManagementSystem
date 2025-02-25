
using FluentValidation;

namespace WorkoutCatalog.Workouts.Features.MuscleGroups.CreateMuscleGroup
{
    public class CreateMuscleGroupCommandValidator : AbstractValidator<CreateMuscleGroupCommand>
    {
        public CreateMuscleGroupCommandValidator()
        {
            RuleFor(x => x.dto.Muscle)
           .NotEmpty().WithMessage("Muscle name is required.")
           .MaximumLength(100).WithMessage("Muscle name cannot exceed 100 characters.");

            RuleFor(x => x.dto.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
        }
    }
}
