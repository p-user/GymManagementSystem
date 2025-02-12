
using AutoMapper;
using WorkoutCatalog.Workouts.Features.WorkoutCategory.GetWorkoutCategoryById;

namespace WorkoutCatalog.Tests.Workouts.Features.WorkoutCategory.GetWorkoutCategoryById
{
    public class GetWorkoutCategoryByIdHandlerTest : IClassFixture<WorkoutCategoryFixture>
    {

        private readonly Mock<WorkoutCatalogDbContext> _dbContextMock;
        private readonly GetWorkoutCategoryByIdQueryHandler _handler;
        private readonly WorkoutCategoryFixture _fixture;
        private readonly Mock<IMapper> _mapper;

        public GetWorkoutCategoryByIdHandlerTest(WorkoutCategoryFixture workoutCategoryFixture)
        {
            _dbContextMock = new Mock<WorkoutCatalogDbContext>(new DbContextOptions<WorkoutCatalogDbContext>());
            _fixture = workoutCategoryFixture;
            _mapper = new Mock<IMapper>();
            _dbContextMock.Setup(x => x.WorkoutCategories).Returns(_fixture.WorkoutCategories.CreateDbSetMock<Models.WorkoutCategory, Guid>().Object);
            _handler = new GetWorkoutCategoryByIdQueryHandler(_dbContextMock.Object, _mapper.Object);
        }


        [Fact]
        public async Task Handle_ShouldReturnWorkoutCategory()
        {
            // Arrange
            var workoutCategory = _fixture.WorkoutCategories.First();
            var query = new GetWorkoutCategoryByIdQuery(workoutCategory.Id);

            _mapper.Setup(x => x.Map<ViewWorkoutCategoryDto>(It.IsAny<Models.WorkoutCategory>()))
                .Returns((Models.WorkoutCategory workoutCategory) => new ViewWorkoutCategoryDto
                {
                    Id = workoutCategory.Id,
                    Name = workoutCategory.Name
                });
            // Act
            var result = await _handler.Handle(query, CancellationToken.None);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(workoutCategory.Id, result.Id);

        }
    }
}
