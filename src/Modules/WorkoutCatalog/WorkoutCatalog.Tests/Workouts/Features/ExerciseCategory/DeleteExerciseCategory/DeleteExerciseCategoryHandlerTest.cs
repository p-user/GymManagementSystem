
using IntegrationTesting.Testing_Utilities;
using WorkoutCatalog.Workouts.Features.ExerciseCategory.DeleteExerciseCategory;

namespace WorkoutCatalog.Tests.Workouts.Features.ExerciseCategory.DeleteExerciseCategory
{
    public class DeleteExerciseCategoryHandlerTest : IClassFixture<ExerciseCategoryFixture>
    {

        private readonly Mock<WorkoutCatalogDbContext> _context;
        private readonly ExerciseCategoryFixture _fixture;
        private readonly DeleteExerciseCategoryCommandHandler _handler;

        public DeleteExerciseCategoryHandlerTest(ExerciseCategoryFixture fixture)
        {
            _fixture = fixture;
            _context = new Mock<WorkoutCatalogDbContext>(new DbContextOptions<WorkoutCatalogDbContext>());

            var dbMockSet = _fixture.ExerciseCategories.CreateDbSetMock<Models.ExerciseCategory, Guid>();


            _context.Setup(db => db.ExerciseCategories).Returns(dbMockSet.Object);
            _handler = new DeleteExerciseCategoryCommandHandler(_context.Object);
        }

        [Fact]
        public async Task Delete_Exercise_Category_When_Exists()
        {
            // Arrange
            var exerciseCategory = _fixture.ExerciseCategories.First();
            var command = new DeleteExcerciseCategoryCommand(exerciseCategory.Id);


            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.True(result);
            _context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            _context.Verify(x=>x.ExerciseCategories.Remove(It.IsAny<Models.ExerciseCategory>()), Times.Once);
        }
    }
}
