

using AutoMapper;
using WorkoutCatalog.Workouts.Features.Exercise.GetExercises;

namespace WorkoutCatalog.Tests.Workouts.Features.Exercise.GetExercises
{
    public class GetExercisesHandlerTest : IClassFixture<ExerciseFixture>, IClassFixture<AutoMapperFixture>
    {

        private readonly ExerciseFixture _fixture;
        private readonly IMapper _mapper;
        private readonly GetExercisesQueryHandler _handler;
        private readonly Mock<WorkoutCatalogDbContext> _dbContextMock;

        public GetExercisesHandlerTest(ExerciseFixture exerciseFixture, AutoMapperFixture autoMapperFixture)
        {
            _fixture = exerciseFixture;

            _dbContextMock = new Mock<WorkoutCatalogDbContext>(new DbContextOptions<WorkoutCatalogDbContext>());

            var mockSet = _fixture.Exercises.CreateDbSetMock<Models.Exercise, Guid>();
            _dbContextMock.Setup(x => x.Exercises).ReturnsDbSet(mockSet.Object);
            _dbContextMock.Setup(x => x.ExerciseCategories).ReturnsDbSet(_fixture.Categories.ExerciseCategories);
            _dbContextMock.Setup(x => x.MuscleGroups).ReturnsDbSet(_fixture.MuscleGroups.MuscleGroups);

            _mapper = autoMapperFixture.Mapper;
            _handler = new GetExercisesQueryHandler(_dbContextMock.Object, _mapper);
        }

        [Fact]
        public async Task Get_Exercises()
        {
            // Arrange
            var query = new GetExercisesQuery();
            // Act
            var result = await _handler.Handle(query, CancellationToken.None);
            // Assert
            result.Should().NotBeNull();
            result.Count().Should().Be(_fixture.Exercises.Count);
        }
    }
}
