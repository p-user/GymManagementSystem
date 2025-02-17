
using WorkoutCatalog.Workouts.Features.Workout.UpdateWorkoutCategories;

namespace WorkoutCatalog.Tests.Workouts.Features.Workout.UpdateWorkoutCategories
{
    public class AddCategoriesToWorkoutHandlerTest : IClassFixture<WorkoutFixture>
    {

        private readonly Mock<WorkoutCatalogDbContext> _context;
        private readonly AddCategoriesToWorkoutCommandHandler _handler;
        private readonly WorkoutFixture _fixture;
        public AddCategoriesToWorkoutHandlerTest(WorkoutFixture fixture)
        {
            _fixture = fixture;
            _context = new Mock<WorkoutCatalogDbContext>(new DbContextOptions<WorkoutCatalogDbContext>());

            var dbMockSet = _fixture.Workouts.CreateDbSetMock<Models.Workout, Guid>();
            _context.Setup(db => db.Workouts).Returns(dbMockSet.Object);

            var categorySet = _fixture.WorkoutCategories.CreateDbSetMock<Models.WorkoutCategory, Guid>();
            _context.Setup(db => db.WorkoutCategories).Returns(categorySet.Object);

            _handler = new AddCategoriesToWorkoutCommandHandler(_context.Object);
        }

        [Fact]
        public async Task Add_Categories_To_Workout_when_Exits()
        {
            // Arrange
            var workout = _fixture.Workouts.First();
            var categories = _fixture.WorkoutCategories.Where(s=>s.Name.Equals("Strength Training")).ToList();
            var categoryIds = categories.Select(x => x.Id).ToList();
            var command = new AddCategoriesToWorkoutCommand(workout.Id, categoryIds);
            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            // Assert
            _context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
            var updatedEntity = await _context.Object.Workouts.FindAsync(result);
            Assert.NotNull(updatedEntity);
            Assert.Equal(workout.Id, updatedEntity.Id);
        }

    }
}
