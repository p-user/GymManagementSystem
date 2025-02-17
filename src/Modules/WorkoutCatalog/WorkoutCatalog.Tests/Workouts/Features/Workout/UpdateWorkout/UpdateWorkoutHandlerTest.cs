
using WorkoutCatalog.Workouts.Features.Workout.UpdateWorkout;

namespace WorkoutCatalog.Tests.Workouts.Features.Workout.UpdateWorkout
{
    public class UpdateWorkoutHandlerTest : IClassFixture<WorkoutFixture>
    {

        private readonly Mock<WorkoutCatalogDbContext> _context;
        private readonly UpdateworkoutCommandHandler _handler;
        private readonly WorkoutFixture _fixture;
        public UpdateWorkoutHandlerTest(WorkoutFixture fixture)
        {
            _context = new Mock<WorkoutCatalogDbContext>(new DbContextOptions<WorkoutCatalogDbContext>());

            _fixture = fixture;
         
            var dbMockSet = _fixture.Workouts.CreateDbSetMock<Models.Workout, Guid>();

            _context.Setup(db => db.Workouts).Returns(dbMockSet.Object);

            var exerciseSet = _fixture.Exercises.CreateDbSetMock<Models.Exercise, Guid>();
            _context.Setup(db => db.Exercises).Returns(exerciseSet.Object);

            var categorySet = _fixture.WorkoutCategories.CreateDbSetMock<Models.WorkoutCategory, Guid>();
            _context.Setup(db => db.WorkoutCategories).Returns(categorySet.Object);

            _handler = new UpdateworkoutCommandHandler(_context.Object);
        }

        [Fact]
        public async Task Update_Workout_When_Exists()
        {
            // Arrange
            var workout = _fixture.Workouts.First();
            var dto = new UpdateWorkoutDto
            {
                Id= workout.Id,
                Name = "Updated Workout",
                Description = "Updated Description",
     
            };
            var updatedWorkout = new UpdateWorkoutCommand(dto);
            // Act
            var result = await _handler.Handle(updatedWorkout, CancellationToken.None);
            // Assert
            _context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
            var updatedEntity = await _context.Object.Workouts.FindAsync(result);
            Assert.NotNull(updatedEntity);
            Assert.Equal("Updated Workout", updatedEntity.Name);
        }
    }
}
