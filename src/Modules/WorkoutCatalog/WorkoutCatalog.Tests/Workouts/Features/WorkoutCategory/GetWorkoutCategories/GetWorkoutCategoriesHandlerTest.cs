
using AutoMapper;
using WorkoutCatalog.Workouts.Features.WorkoutCategory.GetWorkoutCategories;

namespace WorkoutCatalog.Tests.Workouts.Features.WorkoutCategory.GetWorkoutCategories
{
    public class GetWorkoutCategoriesHandlerTest : IClassFixture<WorkoutCategoryFixture>
    {

        private readonly Mock<WorkoutCatalogDbContext> _dbContextMock;
        private readonly GetWorkoutCategoriesQueryHandler _handler;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<DbSet<Models.WorkoutCategory>> _mockSet;
        public GetWorkoutCategoriesHandlerTest(WorkoutCategoryFixture fixture)
        {
            _dbContextMock = new Mock<WorkoutCatalogDbContext>(new DbContextOptions<WorkoutCatalogDbContext>());

            var mockDbSet = fixture.WorkoutCategories.CreateDbSetMock<Models.WorkoutCategory, Guid>();

            _dbContextMock.Setup(s => s.WorkoutCategories).Returns(mockDbSet.Object);
            _mapperMock = new Mock<IMapper>();


            _handler = new GetWorkoutCategoriesQueryHandler(_dbContextMock.Object, _mapperMock.Object);

        }

        [Fact]
        public async Task Handle_Should_Return_WorkoutCategories()
        {
            // Mock the AutoMapper Map method to return the expected DTOs
            _mapperMock.Setup(mapper => mapper.Map<List<ViewWorkoutCategoryDto>>(It.IsAny<List<Models.WorkoutCategory>>()))
                       .Returns((List<Models.WorkoutCategory> categories) => categories.Select(c => new ViewWorkoutCategoryDto
                       {
                           Id = c.Id,
                           Name = c.Name
                       }).ToList());
            //Arrange
            var query = new GetWorkoutCategoriesQuery();

            //Act
            var result = await _handler.Handle(query, CancellationToken.None);

            //Assert

            result.Should().NotBeNull();
            result.Should().HaveCount(2);

            _dbContextMock.Verify(v => v.WorkoutCategories, Times.Once); //ensures that db is accessed once
            _mapperMock.Verify(v => v.Map<List<ViewWorkoutCategoryDto>>(It.IsAny<List<Models.WorkoutCategory>>()), Times.Once); //ensures that mapper is called once

            Assert.Contains(result, r => r.Name == "Strength Training");



        }
    }
}
