﻿using FluentValidation;

namespace WorkoutCatalog.Workouts.Features.ExerciseCategory.CreateExerciseCategory
{
    public record CreateExerciseCategoryCommand(CreateExerciseCategoryDto dto) : IRequest<Guid>;
    public class CreateExerciseCategoryCommandHandler(WorkoutCatalogDbContext context, IValidator<CreateExerciseCategoryCommand> _validator) : IRequestHandler<CreateExerciseCategoryCommand, Guid>
    {
        public async Task<Guid> Handle(CreateExerciseCategoryCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var entity = CreateExerciseCategory(request.dto);
            await context.ExerciseCategories.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }

        private Models.ExerciseCategory CreateExerciseCategory(CreateExerciseCategoryDto dto)
        {
            return Models.ExerciseCategory.Create(dto.Name, dto.Description);
        }
    }
}
