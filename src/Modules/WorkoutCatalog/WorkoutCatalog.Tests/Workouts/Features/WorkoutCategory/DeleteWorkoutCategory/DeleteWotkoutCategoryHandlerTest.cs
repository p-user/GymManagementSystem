

using WorkoutCatalog.Workouts.Features.WorkoutCategory.DeleteWorkoutCategory;

namespace WorkoutCatalog.Tests.Workouts.Features.WorkoutCategory.DeleteWorkoutCategory
{
    public class DeleteWotkoutCategoryHandlerTest : IClassFixture<WorkoutCategoryFixture>
    {
        private readonly Mock<WorkoutCatalogDbContext> _dbContextMock;
        private readonly DeleteWorkoutCategoryCommandHandler _handler;
        private readonly WorkoutCategoryFixture _fixture;
        public DeleteWotkoutCategoryHandlerTest(WorkoutCategoryFixture workoutCategoryFixture)
        {
            _fixture = workoutCategoryFixture;
            _dbContextMock = new Mock<WorkoutCatalogDbContext>(new DbContextOptions<WorkoutCatalogDbContext>());
            var mockDbSet = workoutCategoryFixture.WorkoutCategories.CreateDbSetMock<Models.WorkoutCategory, Guid>();
            _dbContextMock.Setup(s => s.WorkoutCategories).Returns(mockDbSet.Object);
            _handler = new DeleteWorkoutCategoryCommandHandler(_dbContextMock.Object);
        }

        [Fact]
        public async Task Delete_WorkoutCategory_When_Exists()
        {
            //Arrange 
            var idToDelete = _fixture.WorkoutCategories.First().Id;
            var command= new DeleteWorkoutCategoryCommand(idToDelete);

            //Act
            var result= await _handler.Handle(command, CancellationToken.None);
            Assert.NotNull(result);
            Assert.True(result);


            _dbContextMock.Verify(v => v.WorkoutCategories, Times.AtMost(3));
            _dbContextMock.Verify(v => v.SaveChangesAsync(CancellationToken.None), Times.Once);
            _dbContextMock.Verify(v => v.WorkoutCategories.Remove(It.IsAny<Models.WorkoutCategory>()), Times.Once);
        }
    }
}
