

using WorkoutCatalog.Workouts.Features.ExerciseCategory.CreateExerciseCategory;

namespace WorkoutCatalog.Tests.Workouts.Features.ExerciseCategory.CreateExerciseCategory
{
    public class CreateExerciseCategoryHandlerTest
    {

        private readonly Mock<WorkoutCatalogDbContext> _dbContextMock;
        private readonly CreateExerciseCategoryCommandHandler _handler;

        public CreateExerciseCategoryHandlerTest()
        {
            _dbContextMock = new Mock<WorkoutCatalogDbContext>(new DbContextOptions<WorkoutCatalogDbContext>());

            _dbContextMock.Setup(x => x.ExerciseCategories)
                .ReturnsDbSet(new List<Models.ExerciseCategory>());

            _handler = new CreateExerciseCategoryCommandHandler(_dbContextMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateExerciseCategory()
        {
            // Arrange
            var command = new CreateExerciseCategoryCommand(new CreateExerciseCategoryDto
            {
                Name = "Compound",
                Description = "Exercises that involve multiple muscle groups and joints. These movements typically mimic real-life /" +
                "activities and help build overall strength and coordination. Examples include squats, deadlifts, and bench presses."
            });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBe(Guid.Empty);
            _dbContextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }


    }
}
