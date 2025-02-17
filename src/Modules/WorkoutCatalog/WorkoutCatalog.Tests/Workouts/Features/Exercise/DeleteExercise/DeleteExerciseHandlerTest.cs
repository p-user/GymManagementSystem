
using AutoMapper;
using Moq.EntityFrameworkCore;
using WorkoutCatalog.Workouts.Features.Exercise.DeleteExcercise;

namespace WorkoutCatalog.Tests.Workouts.Features.Exercise.DeleteExercise
{
    public class DeleteExerciseHandlerTest : IClassFixture<ExerciseFixture>, IClassFixture<AutoMapperFixture>
    {

        private readonly ExerciseFixture _fixture;
        private readonly IMapper _mapper;
        private readonly DeleteExerciseCommandHandler _handler;
        private readonly Mock<WorkoutCatalogDbContext> _dbContextMock;

        public DeleteExerciseHandlerTest(ExerciseFixture exerciseFixture, AutoMapperFixture autoMapperFixture)
        {
            _dbContextMock= new Mock<WorkoutCatalogDbContext>(new DbContextOptions<WorkoutCatalogDbContext>());
            _fixture = exerciseFixture;

            var mockSet = _fixture.Exercises.CreateDbSetMock<Models.Exercise, Guid>();
            _dbContextMock.Setup(x => x.Exercises).ReturnsDbSet(mockSet.Object);
            _mapper = autoMapperFixture.Mapper;
            _handler = new DeleteExerciseCommandHandler(_dbContextMock.Object);
        }

        [Fact]
        public async Task Delete_Exercise_When_Exists()
        {
            // Arrange
            var exercise = _fixture.Exercises.First();

            // Act
            var result = await _handler.Handle(new DeleteExerciseCommand(exercise.Id), CancellationToken.None);
            // Assert
            result.Should().BeTrue();
            _dbContextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
            _dbContextMock.Verify(x => x.Exercises.Remove(It.IsAny<Models.Exercise>()));
        }
    }
}
