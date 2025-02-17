
using WorkoutCatalog.Workouts.Features.Workout.RemoveCategoryFromWorkout;

namespace WorkoutCatalog.Tests.Workouts.Features.Workout.RemoveCategoryFromWorkout
{
    public class RemoveCategoryFromWorkoutHandlerTest : IClassFixture<WorkoutFixture>
    {
        private readonly WorkoutFixture _fixture;
        private readonly Mock<WorkoutCatalogDbContext> _contextMock;
        private readonly RemoveCategoryFromWorkoutCommandHandler _handler;
        public RemoveCategoryFromWorkoutHandlerTest(WorkoutFixture fixture)
        {
            _fixture = fixture;

            _contextMock = new Mock<WorkoutCatalogDbContext>(new DbContextOptions<WorkoutCatalogDbContext>());

            var mockWorkoutSet = _fixture.Workouts.CreateDbSetMock<Models.Workout, Guid>();
            _contextMock.Setup(s=>s.Workouts).Returns(mockWorkoutSet.Object);

            var categories = _fixture.WorkoutCategories.CreateDbSetMock<Models.WorkoutCategory, Guid>();    
            _contextMock.Setup(s=>s.WorkoutCategories).Returns(categories.Object);

            _handler = new RemoveCategoryFromWorkoutCommandHandler(_contextMock.Object);


        }

        [Fact]
        public async Task Remove_Category_From_Workout_When_Exists()
        {
            //arrange
            var workout = _fixture.Workouts.First();
            var category = workout.WorkoutCategories.First();

            var command = new RemoveWorkoutCategoryCommand(workout.Id,category.Id);   

            //act 
            var result = await _handler.Handle(command, CancellationToken.None);


            //assert
             var updatedEntity = _contextMock.Object.Workouts.Include(s=>s.WorkoutCategories).FirstOrDefault(s=>s.Id== workout.Id);

            Assert.False(updatedEntity.WorkoutCategories.Any(s => s.Id.Equals(updatedEntity.Id)));
        }
    }
}
