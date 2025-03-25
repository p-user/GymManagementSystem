using WorkoutCatalog.Workouts.Features.Exercise.RemoceMuscleGroupFromExercise;

namespace WorkoutCatalog.Tests.Workouts.Features.Exercise.RemoveMuscleGroupFromExercise
{
    public class RemoveMuscleGroupFromExerciseHandlerTest : IClassFixture<ExerciseFixture>
    {
        private readonly ExerciseFixture _fixture;
        private readonly Mock<WorkoutCatalogDbContext> _dbContextMock;
        private readonly RemoveMuscleGroupFromExerciseCommandHandler _handler;

        public RemoveMuscleGroupFromExerciseHandlerTest(ExerciseFixture exerciseFixture)
        {
            _fixture = exerciseFixture;
            _dbContextMock = new Mock<WorkoutCatalogDbContext>(new DbContextOptions<WorkoutCatalogDbContext>());
            _dbContextMock.Setup(x => x.Exercises).ReturnsDbSet(_fixture.Exercises);
            _dbContextMock.Setup(x => x.MuscleGroups).ReturnsDbSet(_fixture.MuscleGroups.MuscleGroups);

            _handler = new RemoveMuscleGroupFromExerciseCommandHandler(_dbContextMock.Object);

        }


        [Fact]
        public async Task Remove_MuscleGroup_From_Exercise_When_Exists()
        {
            // Arrange
            var exercise = _fixture.Exercises.First();
            var muscleGroup = exercise.MuscleGroups.First();

            // Act
            var result = await _handler.Handle(new RemoveMuscleGroupFromExerciseCommand(exercise.Id, muscleGroup.Id), CancellationToken.None);
            // Assert
            result.Should().BeTrue();
            _dbContextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
            var updatedExercise = await _dbContextMock.Object.Exercises.Include(e => e.MuscleGroups).FirstOrDefaultAsync(e => e.Id == exercise.Id);
            updatedExercise.MuscleGroups.Should().NotContain(muscleGroup);
        }

    }
}
