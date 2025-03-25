
using AutoMapper;
using WorkoutCatalog.Workouts.Features.ExerciseCategory.GetExerciseCategories;

namespace WorkoutCatalog.Tests.Workouts.Features.ExerciseCategory.GetExerciseCategories
{
    public class GetExerciseCategoriesHandlerTest : IClassFixture<ExerciseCategoryFixture>
    {

        private readonly ExerciseCategoryFixture _fixture;
        private readonly Mock<WorkoutCatalogDbContext> _context;
        private readonly GetExerciseCategoriesQueryHandler _handler;
        private readonly Mock<IMapper> _mapper;

        public GetExerciseCategoriesHandlerTest(ExerciseCategoryFixture fixture)
        {
            _fixture = fixture;
            _context = new Mock<WorkoutCatalogDbContext>(new DbContextOptions<WorkoutCatalogDbContext>());
            _mapper = new Mock<IMapper>();
            var dbMockSet = _fixture.ExerciseCategories.CreateDbSetMock<Models.ExerciseCategory, Guid>();
            _context.Setup(db => db.ExerciseCategories).Returns(dbMockSet.Object);
            _handler = new GetExerciseCategoriesQueryHandler(_context.Object, _mapper.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnExerciseCategories()
        {
            //configure mapper 
            _mapper.Setup(_mapper => _mapper.Map<List<ViewExerciseCategoryDto>>(It.IsAny<List<Models.ExerciseCategory>>()))
                .Returns((List<Models.ExerciseCategory> exerciseCategories) => exerciseCategories.Select(x => new ViewExerciseCategoryDto
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList());


            // Arrange
            var query = new GetExerciseCategoriesQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(_fixture.ExerciseCategories.Count, result.Count);
        }
    }
}
