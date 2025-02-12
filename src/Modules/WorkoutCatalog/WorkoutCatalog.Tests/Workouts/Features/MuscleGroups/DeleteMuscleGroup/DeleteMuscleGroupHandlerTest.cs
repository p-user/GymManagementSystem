

using WorkoutCatalog.Workouts.Features.MuscleGroups.DeleteMuscleGroup;

namespace WorkoutCatalog.Tests.Workouts.Features.MuscleGroups.DeleteMuscleGroup
{
    public class DeleteMuscleGroupHandlerTest : IClassFixture<MuscleGroupFixture>
    {
        private readonly Mock<WorkoutCatalogDbContext> _dbContextMock;
        private readonly DeleteMuscleGroupCommandHandler _handler;
        private readonly MuscleGroupFixture _fixture;
        public DeleteMuscleGroupHandlerTest(MuscleGroupFixture muscleGroupFixture)
        {
            _fixture = muscleGroupFixture;
            _dbContextMock = new Mock<WorkoutCatalogDbContext>(new DbContextOptions<WorkoutCatalogDbContext>());
            var mockDbSet = muscleGroupFixture.MuscleGroups.CreateDbSetMock<Models.MuscleGroup, Guid>();
            _dbContextMock.Setup(s => s.MuscleGroups).Returns(mockDbSet.Object);
            _handler = new DeleteMuscleGroupCommandHandler(_dbContextMock.Object);
        }
        [Fact]
        public async Task Delete_MuscleGroup_When_Exists()
        {
            //Arrange 
            var idToDelete = _fixture.MuscleGroups.First().Id;


            //Act
            var command = new DeleteMuscleGroupCommand(idToDelete);
            var result = await _handler.Handle(command, CancellationToken.None);


            //
            Assert.True(result);
            _dbContextMock.Verify(v => v.MuscleGroups, Times.AtMost(3));
            _dbContextMock.Verify(v => v.SaveChangesAsync(CancellationToken.None), Times.Once);
            _dbContextMock.Verify(v => v.MuscleGroups.Remove(It.IsAny<Models.MuscleGroup>()), Times.Once);
        }
    }
}
