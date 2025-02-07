
using AutoMapper;
using WorkoutCatalog.Workouts.Features.WorkoutCategory.GetWorkoutCategories;

namespace WorkoutCatalog.Tests.Workouts.Features.WorkoutCategory.GetWorkoutCategories
{
    public class GetWorkoutCategoriesHandlerTest
    {

        private readonly Mock<WorkoutCatalogDbContext> _dbContextMock;
        private readonly GetWorkoutCategoriesQueryHandler _handler;
        private readonly Mock<IMapper> _mapperMock;
        public GetWorkoutCategoriesHandlerTest()
        {
            _dbContextMock = new Mock<WorkoutCatalogDbContext>(new DbContextOptions<WorkoutCatalogDbContext>());

            _dbContextMock.Setup(s=>s.WorkoutCategories)
                .ReturnsDbSet(new List<Models.WorkoutCategory>());

            _mapperMock = new Mock<IMapper>();


            _handler = new GetWorkoutCategoriesQueryHandler(_dbContextMock.Object, _mapperMock.Object);

        }

        [Fact]
        public async Task Handle_Should_Return_WorkoutCategory_When_Exists()
        {

        }
    }
}
