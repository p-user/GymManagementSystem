
using WorkoutCatalog.Workouts.Features.MuscleGroups.CreateMuscleGroup;

namespace WorkoutCatalog.Tests.Workouts.Features.MuscleGroups.CreateMuscleGroup
{
    public class CreateMuscleGroupHandlerTest
    {
        private readonly Mock<WorkoutCatalogDbContext> _dbContextMock;
        private readonly CreateMuscleGroupCommandHandler _handler;

        public CreateMuscleGroupHandlerTest()
        {
            _dbContextMock = new Mock<WorkoutCatalogDbContext>(new DbContextOptions<WorkoutCatalogDbContext>());

            _dbContextMock.Setup(x => x.MuscleGroups)
                .ReturnsDbSet(new List<Models.MuscleGroup>());

            _handler = new CreateMuscleGroupCommandHandler(_dbContextMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateMuscleGroup()
        {
            // Arrange
            var command = new CreateMuscleGroupCommand(new CreateMuscleGroupDto
            {
                Muscle = "Biceps",
                Description = "The biceps brachii is a two-headed muscle located on the front of the arm."
            });


            // Act
            var result = await _handler.Handle(command, CancellationToken.None);


            // Assert
            result.Should().NotBe(Guid.Empty);
            _dbContextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }


    }
}
