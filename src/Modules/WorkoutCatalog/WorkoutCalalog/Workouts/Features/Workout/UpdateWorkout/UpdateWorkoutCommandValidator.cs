
using FluentValidation;

namespace WorkoutCatalog.Workouts.Features.Workout.UpdateWorkout
{
    public class UpdateWorkoutCommandValidator : AbstractValidator<UpdateWorkoutCommand>
    {
        public UpdateWorkoutCommandValidator()
        {
            RuleFor(x => x.dto.Name)
           .NotEmpty().WithMessage("Workout name is required.")
           .MaximumLength(100).WithMessage("Workout name cannot exceed 100 characters.");


            RuleFor(x => x.dto.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

           
        }
    
    }
}
