
using AutoMapper;
using WorkoutCatalog.Workouts.Features.MuscleGroups.GetMuscleGroups;

namespace WorkoutCatalog.Tests.Workouts.Features.MuscleGroups.GetMuscleGroups
{
    public class GetMuscleGroupsHandlerTest : IClassFixture<MuscleGroupFixture>
    {

        private readonly Mock<WorkoutCatalogDbContext> _dbContextMock;
        private readonly GetMuscleGroupsQueryHandler _handler;
        private readonly Mock<IMapper> _mapperMock;
        public GetMuscleGroupsHandlerTest(MuscleGroupFixture fixture)
        {
            _dbContextMock = new Mock<WorkoutCatalogDbContext>(new DbContextOptions<WorkoutCatalogDbContext>());
            _mapperMock = new Mock<IMapper>();
            var entities = fixture.MuscleGroups.CreateDbSetMock<Models.MuscleGroup, Guid>().Object;
            _dbContextMock.Setup(s => s.MuscleGroups).Returns(entities);
            _handler = new GetMuscleGroupsQueryHandler(_dbContextMock.Object, _mapperMock.Object);

        }

        [Fact]
        public async Task Handle_Should_Return_MuscleGroups()
        {
            // Mock the AutoMapper Map method to return the expected DTOs
            _mapperMock.Setup(mapper => mapper.Map<List<ViewMuscleGroupDto>>(It.IsAny<List<Models.MuscleGroup>>()))
                       .Returns((List<Models.MuscleGroup> muscleGroups) => muscleGroups.Select(m => new ViewMuscleGroupDto
                       {
                           Id = m.Id,
                           Muscle = m.Muscle
                       }).ToList());


            //Arrange
            var query = new GetMuscleGroupsQuery();
            //Act
            var result = await _handler.Handle(query, CancellationToken.None);
            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(10);

            _dbContextMock.Verify(v => v.MuscleGroups, Times.Once); //ensures that db is accessed once
            _mapperMock.Verify(v => v.Map<List<ViewMuscleGroupDto>>(It.IsAny<List<Models.MuscleGroup>>()), Times.Once); //ensures that mapper is called once
            Assert.Contains(result, r => r.Muscle == "Chest");
        }
    }

}
