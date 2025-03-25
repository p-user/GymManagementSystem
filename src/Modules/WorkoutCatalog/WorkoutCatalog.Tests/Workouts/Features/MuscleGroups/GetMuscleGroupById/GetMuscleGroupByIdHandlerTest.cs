
using AutoMapper;
using WorkoutCatalog.Workouts.Features.MuscleGroups.GetMuscleGroupById;

namespace WorkoutCatalog.Tests.Workouts.Features.MuscleGroups.GetMuscleGroupById
{
    public class GetMuscleGroupByIdHandlerTest : IClassFixture<MuscleGroupFixture>
    {

        private readonly Mock<WorkoutCatalogDbContext> _dbContextMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly List<Models.MuscleGroup> muscleGroups;
        private readonly GetMuscleGroupByIdQueryHandler _handler;
        public GetMuscleGroupByIdHandlerTest(MuscleGroupFixture fixture)
        {
            _dbContextMock = new Mock<WorkoutCatalogDbContext>(new DbContextOptions<WorkoutCatalogDbContext>());
            _mapperMock = new Mock<IMapper>();
            muscleGroups = fixture.MuscleGroups;
            var arranges = fixture.MuscleGroups.CreateDbSetMock<Models.MuscleGroup, Guid>();
            _dbContextMock.Setup(x => x.MuscleGroups).Returns(arranges.Object);
            _handler = new GetMuscleGroupByIdQueryHandler(_dbContextMock.Object, _mapperMock.Object);

        }

        [Fact]
        public async Task Handle_ShouldReturnMuscleGroup()
        {
            // Arrange
            var muscleGroup = muscleGroups.First();
            var query = new GetMuscleGroupByIdQuery(muscleGroup.Id);

            _mapperMock.Setup(x => x.Map<ViewMuscleGroupDto>(It.IsAny<Models.MuscleGroup>())).
                Returns((Models.MuscleGroup muscleGroup) => new ViewMuscleGroupDto
                {
                    Id = muscleGroup.Id,
                    Muscle = muscleGroup.Muscle,
                    Description = muscleGroup.Description,
                });
            // Act
            var result = await _handler.Handle(query, CancellationToken.None);
            // Assert
            Assert.NotNull(result);

            Assert.Equal(muscleGroup.Id, result.Id);
            Assert.Equal(muscleGroup.Muscle, result.Muscle);
        }
    }
}
