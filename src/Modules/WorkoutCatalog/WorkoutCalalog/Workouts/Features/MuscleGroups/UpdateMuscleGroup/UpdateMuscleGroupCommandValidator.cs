namespace WorkoutCatalog.Workouts.Features.MuscleGroups.UpdateMuscleGroup
{
    public class UpdateMuscleGroupCommandValidator : AbstractValidator<UpdateMuscleGroupCommand>
    {
        public UpdateMuscleGroupCommandValidator()
        {
            RuleFor(x => x.CreateMuscleGroupDto.Muscle)
          .NotEmpty().WithMessage("Muscle name is required.")
          .MaximumLength(100).WithMessage("Muscle name cannot exceed 100 characters.");

            RuleFor(x => x.CreateMuscleGroupDto.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
        }
    }
}
