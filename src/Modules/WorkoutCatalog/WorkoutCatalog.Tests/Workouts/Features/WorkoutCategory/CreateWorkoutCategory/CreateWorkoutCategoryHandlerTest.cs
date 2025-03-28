﻿
using FluentValidation;
using FluentValidation.Results;
using WorkoutCatalog.Workouts.Features.WorkoutCategory.CreateWorkoutCategory;

namespace WorkoutCatalog.Tests.Workouts.Features.WorkoutCategory.CreateWorkoutCategory
{
    public class CreateWorkoutCategoryHandlerTest
    {

        private readonly Mock<WorkoutCatalogDbContext> _dbContextMock;
        private readonly CreateWorkoutCategoryCommandHandler _handler;
        private readonly Mock<IValidator<CreateWorkoutCategoryCommand>> _validatorMock;
        public CreateWorkoutCategoryHandlerTest()
        {
            _dbContextMock = new Mock<WorkoutCatalogDbContext>(new DbContextOptions<WorkoutCatalogDbContext>());

            _dbContextMock.Setup(x => x.WorkoutCategories)
                .ReturnsDbSet(new List<Models.WorkoutCategory>());

            _validatorMock = new Mock<IValidator<CreateWorkoutCategoryCommand>>();
            _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<ValidationContext<CreateWorkoutCategoryCommand>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            _handler = new CreateWorkoutCategoryCommandHandler(_dbContextMock.Object, _validatorMock.Object);

        }

        [Fact]

        public async Task Handle_ShouldCreateWorkoutCategory()
        {
            // Arrange
            var command = new CreateWorkoutCategoryCommand(new CreateWorkoutCategoryDto
            {
                Name = "Strength",
                Description = "Strength training is a type of physical exercise specializing in the use of resistance to induce muscular contraction, which builds the strength, anaerobic endurance, and size of skeletal muscles."
            });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            // Assert
            result.Should().NotBe(Guid.Empty);
            _dbContextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }



    }
}
