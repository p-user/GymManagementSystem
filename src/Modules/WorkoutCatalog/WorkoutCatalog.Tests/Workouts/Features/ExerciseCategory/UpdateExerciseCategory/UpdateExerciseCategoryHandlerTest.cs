using FluentValidation;
using FluentValidation.Results;
using WorkoutCatalog.Workouts.Features.Exercise.CreateExercise;
using WorkoutCatalog.Workouts.Features.ExerciseCategory.UpdateExerciseCategory;

namespace WorkoutCatalog.Tests.Workouts.Features.ExerciseCategory.UpdateExerciseCategory
{
    public class UpdateExerciseCategoryHandlerTest : IClassFixture<ExerciseCategoryFixture>
    {

        private readonly Mock<WorkoutCatalogDbContext> _context;
        private readonly ExerciseCategoryFixture _fixture;
        private readonly UpdateExerciseCategoryCommandHandler _handler;
        private readonly Mock<IValidator<UpdateExerciseCategoryCommand>> _validatorMock;


        public UpdateExerciseCategoryHandlerTest(ExerciseCategoryFixture fixture)
        {
            _fixture = fixture;
            _context = new Mock<WorkoutCatalogDbContext>(new DbContextOptions<WorkoutCatalogDbContext>());
            var mockObj = _fixture.ExerciseCategories.CreateDbSetMock<Models.ExerciseCategory, Guid>();
            _context.Setup(db => db.ExerciseCategories).Returns(mockObj.Object);

            _validatorMock = new Mock<IValidator<UpdateExerciseCategoryCommand>>();
            _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<ValidationContext<UpdateExerciseCategoryCommand>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());
            _handler = new UpdateExerciseCategoryCommandHandler(_context.Object, _validatorMock.Object);
        }
        [Fact]
        public async Task Update_Exercise_Category_When_Exists()
        {
          
            // Arrange
            var exerciseCategory = _fixture.ExerciseCategories.First();

            var dto = new CreateExerciseCategoryDto
            {
                Name = "Compound Exercises Update test",
                Description = "Engage multiple muscle groups"
            };
            var command = new UpdateExerciseCategoryCommand(dto, exerciseCategory.Id);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.True(result);
            _context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

            var updatedEntity = await _context.Object.ExerciseCategories.FindAsync(exerciseCategory.Id);
            updatedEntity.Name.Should().Be(dto.Name);
            updatedEntity.Description.Should().Be(dto.Description);
        }
    }
}
