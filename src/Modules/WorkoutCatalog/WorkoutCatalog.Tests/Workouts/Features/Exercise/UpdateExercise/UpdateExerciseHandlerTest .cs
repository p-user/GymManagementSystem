

using AutoMapper;
using Moq;
using WorkoutCatalog.Workouts.Features.Exercise.UpdateExcercise;

namespace WorkoutCatalog.Tests.Workouts.Features.Exercise.UpdateExercise
{
    public class UpdateExerciseHandlerTest : IClassFixture<ExerciseFixture>
    {

        private readonly ExerciseFixture _fixture;
        private readonly UpdateExerciseCommandHandler _handler;
        private readonly Mock<WorkoutCatalogDbContext> _dbContextMock;
        public UpdateExerciseHandlerTest(ExerciseFixture exerciseFixture)
        {
            _fixture = exerciseFixture;
            _dbContextMock = new Mock<WorkoutCatalogDbContext>(new DbContextOptions<WorkoutCatalogDbContext>());
            var mockSet = _fixture.Exercises.CreateDbSetMock<Models.Exercise, Guid>();
            _dbContextMock.Setup(x => x.Exercises).ReturnsDbSet(mockSet.Object);
            _dbContextMock.Setup(x => x.ExerciseCategories).ReturnsDbSet(_fixture.Categories.ExerciseCategories);
            _dbContextMock.Setup(x => x.MuscleGroups).ReturnsDbSet(_fixture.MuscleGroups.MuscleGroups);

         

            _handler = new UpdateExerciseCommandHandler(_dbContextMock.Object);
        }
        [Fact]
        public async Task Update_Exercise_When_Exists()
        {
            // Arrange
            var exercise = _fixture.Exercises.First();

            var dto = new UpdateExerciseDto
            {
                Name = "Updated Exercise",
                Description = "Updated Description",
                DescriptionLink = "https://www.youtube.com/watch?v=gl17x1L_8Kc&list=RDgl17x1L_8Kc&start_radio=1",
                ExerciseCategory = exercise.ExerciseCategory,

            };
            var updatedExercise = new UpdateExerciseCommand(dto, exercise.Id);
            // Act
            var result = await _handler.Handle(updatedExercise, CancellationToken.None);
            // Assert
           

            _dbContextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));



            var updatedEntity = await _dbContextMock.Object.Exercises.FirstOrDefaultAsync(e => e.Id == exercise.Id);
            updatedEntity.Name.Should().Be("Updated Exercise");
            updatedEntity.Description.Should().Be("Updated Description");
            updatedEntity.ExerciseCategory.Should().Be(exercise.ExerciseCategory);
       
        }
    }
}
