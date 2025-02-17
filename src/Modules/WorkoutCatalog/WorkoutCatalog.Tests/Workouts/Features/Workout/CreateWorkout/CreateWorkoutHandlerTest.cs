
using WorkoutCatalog.Workouts.Features.Workout.CreateWorkout;

namespace WorkoutCatalog.Tests.Workouts.Features.Workout.CreateWorkout
{
    public class CreateWorkoutHandlerTest : IClassFixture<WorkoutFixture>
    {

        private readonly Mock<WorkoutCatalogDbContext> _context;
        private readonly WorkoutFixture _fixture;
        private readonly CreateWorkoutCommandHandler _handler;
        public CreateWorkoutHandlerTest(WorkoutFixture fixture)
        {
            _fixture = fixture;
            _context = new Mock<WorkoutCatalogDbContext>(new DbContextOptions<WorkoutCatalogDbContext>());
            var dbMockSet = _fixture.Workouts.CreateDbSetMock<Models.Workout, Guid>();
            _context.Setup(db => db.Workouts).Returns(dbMockSet.Object);

            var exerciseSet = _fixture.Exercises.CreateDbSetMock<Models.Exercise, Guid>();
            _context.Setup(db => db.Exercises).Returns(exerciseSet.Object);

            var categorySet = _fixture.WorkoutCategories.CreateDbSetMock<Models.WorkoutCategory, Guid>();
            _context.Setup(db => db.WorkoutCategories).Returns(categorySet.Object);
            _handler = new CreateWorkoutCommandHandler(_context.Object);
        }


        [Fact]
        public async Task Create_Workout_When_Dependencies_Exits()
        {
            // Arrange
            var workout = _fixture.Workouts.First();
            var dto = new CreateWorkoutDto
            {
                Name = "Updated Workout",
                Description = "Updated Description",
                Categories = workout.WorkoutCategories.Select(s => s.Id).ToList(),
                Exercises = workout.Exercises.Select(s => s.Id).ToList()
            };
            var updatedWorkout = new CreateWorkoutCommand(dto);
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
