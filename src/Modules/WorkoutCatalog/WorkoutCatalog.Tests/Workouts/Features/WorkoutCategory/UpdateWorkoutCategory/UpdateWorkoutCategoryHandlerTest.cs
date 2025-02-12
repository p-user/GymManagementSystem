
using WorkoutCatalog.Workouts.Features.WorkoutCategory.UpdateWorkoutCategory;


namespace WorkoutCatalog.Tests.Workouts.Features.WorkoutCategory.UpdateWorkoutCategory
{
    public class UpdateWorkoutCategory : IClassFixture<WorkoutCategoryFixture>
    {
        private readonly WorkoutCategoryFixture _fixture;
        private readonly Mock<WorkoutCatalogDbContext> _dbContextMock;
        private readonly UpdateWorkoutCategoryCommandHandler _handler;
        private readonly Mock<DbSet<Models.WorkoutCategory>> _mockSet;

        public UpdateWorkoutCategory(WorkoutCategoryFixture fixture)
        {
            _fixture = fixture;
            _mockSet = _fixture.WorkoutCategories.CreateDbSetMock<Models.WorkoutCategory, Guid>();

            _dbContextMock = new Mock<WorkoutCatalogDbContext>(new DbContextOptions<WorkoutCatalogDbContext>());
      

            _dbContextMock.Setup(x => x.WorkoutCategories).Returns(_mockSet.Object);
            _handler = new UpdateWorkoutCategoryCommandHandler(_dbContextMock.Object);
        }

        [Fact]

        public async Task Handle_ShouldUpdateWorkoutCategory()
        {

            var test = _dbContextMock.Object.WorkoutCategories.ToList();
            // Arrange

            var existingEntity = _fixture.WorkoutCategories.First();
        var id = existingEntity.Id;

            var sampleDto = new UpdateWorkoutCategoryDto
            {
                Id = id,
                Name = "StrenGth", //here name is changed to check if it is updated
                Description = "Strength training is a type of Physical exercise specializing in the use of resistance to induce muscular contraction, which builds the strength, anaerobic endurance, and size of skeletal muscles."
            };

            var command = new UpdateWorkoutCategoryCommand(sampleDto);
            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            // Assert
            result.Should().NotBe(Guid.Empty); //to verify the response from handler 

            //verify if the changes are reflected in db as well

            var updatedEntity = await _dbContextMock.Object.WorkoutCategories.FindAsync(existingEntity.Id);
            updatedEntity.Name.Should().Be(sampleDto.Name);
            updatedEntity.Description.Should().Be(sampleDto.Description);


            _dbContextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }


        [Fact]
        public async Task Handle_ShouldThrowExceptionWhenWorkoutCategoryNotFound()
        {
            // Arrange
            var sampleDto = new UpdateWorkoutCategoryDto
            {
                Id = Guid.NewGuid(),
                Name = "Strength",
                Description = "Strength training is a type of physical exercise specializing in the use of resistance to induce muscular contraction, which builds the strength, anaerobic endurance, and size of skeletal muscles."
            };
            var command = new UpdateWorkoutCategoryCommand(sampleDto);
            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);
            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Workout category not found!");
        }



    }
}
