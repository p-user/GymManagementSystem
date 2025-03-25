

using AutoMapper;
using WorkoutCatalog.Workouts.Features.Exercise.GetExcerciseById;

namespace WorkoutCatalog.Tests.Workouts.Features.Exercise.GetExerciseById
{
    public class GetExerciseByIdHandlerTest : IClassFixture<ExerciseFixture>, IClassFixture<AutoMapperFixture>
    {

        private readonly ExerciseFixture _fixture;
        private readonly IMapper _mapper;
        private readonly GetExerciseByIdQueryHandler _handler;
        private readonly Mock<WorkoutCatalogDbContext> _dbContextMock;
        public GetExerciseByIdHandlerTest(ExerciseFixture exerciseFixture, AutoMapperFixture autoMapperFixture)
        {
            _dbContextMock = new Mock<WorkoutCatalogDbContext>(new DbContextOptions<WorkoutCatalogDbContext>());
            _fixture = exerciseFixture;
            _dbContextMock.Setup(x => x.Exercises).ReturnsDbSet(_fixture.Exercises);
            _dbContextMock.Setup(x => x.ExerciseCategories).ReturnsDbSet(_fixture.Categories.ExerciseCategories);
            _dbContextMock.Setup(x => x.MuscleGroups).ReturnsDbSet(_fixture.MuscleGroups.MuscleGroups);
            _mapper = autoMapperFixture.Mapper;

            _handler = new GetExerciseByIdQueryHandler(_dbContextMock.Object, _mapper);
        }

        [Fact]
        public async Task Get_exercise_By_Id_When_Exists()
        {
            // Arrange
            var exercise = _fixture.Exercises.First();
            // Act
            var result = await _handler.Handle(new GetExerciseByIdQuery(exercise.Id), CancellationToken.None);
            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(exercise.Id);
            result.Name.Should().Be(exercise.Name);
            result.Description.Should().Be(exercise.Description);
            result.ExerciseCategory.Should().Be(exercise.ExerciseCategory);

            result.MuscleGroups.Should().BeEquivalentTo(
                    exercise.MuscleGroups.Select(x => new ViewMuscleGroupDto
                    {
                        Id = x.Id,
                        Muscle = x.Muscle,
                        Description = x.Description

                    }),
                    options => options.WithoutStrictOrdering()
                );


        }

    }
}
