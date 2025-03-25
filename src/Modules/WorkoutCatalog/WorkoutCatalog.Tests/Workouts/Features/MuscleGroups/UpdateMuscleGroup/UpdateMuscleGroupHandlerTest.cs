

using FluentValidation;
using FluentValidation.Results;
using WorkoutCatalog.Workouts.Features.MuscleGroups.UpdateMuscleGroup;

namespace WorkoutCatalog.Tests.Workouts.Features.MuscleGroups.UpdateMuscleGroup
{
    public class UpdateMuscleGroupHandlerTest : IClassFixture<MuscleGroupFixture>
    {

        private readonly Mock<WorkoutCatalogDbContext> _dbContextMock;
        private readonly UpdateMuscleGroupCommandHandler _handler;
        private readonly MuscleGroupFixture _fixture;
        private readonly Mock<IValidator<UpdateMuscleGroupCommand>> _validatorMock;


        public UpdateMuscleGroupHandlerTest(MuscleGroupFixture fixture)
        {
            _fixture = fixture;
            _dbContextMock = new Mock<WorkoutCatalogDbContext>(new DbContextOptions<WorkoutCatalogDbContext>());

            var entities = fixture.MuscleGroups.CreateDbSetMock<Models.MuscleGroup, Guid>().Object;

            _dbContextMock.SetupGet(x => x.MuscleGroups).Returns(entities);

            _validatorMock = new Mock<IValidator<UpdateMuscleGroupCommand>>();
            _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<ValidationContext<UpdateMuscleGroupCommand>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            _handler = new UpdateMuscleGroupCommandHandler(_dbContextMock.Object, _validatorMock.Object);

        }

        [Fact]
        public async Task Handle_ShouldUpdateMuscleGroup()
        {
            // Arrange
            var existingEntity = _fixture.MuscleGroups.First();

            // Act
            var dto = new UpdateMuscleGroupDto
            {
                Id = existingEntity.Id,
                Muscle = "test hhh",
                Description = "test muscles"
            };

            var command = new UpdateMuscleGroupCommand(dto.Id, dto);
            var result = await _handler.Handle(command, CancellationToken.None);


            // Assert
            Assert.NotNull(result);

            var updatedEntity = await _dbContextMock.Object.MuscleGroups.FindAsync(existingEntity.Id);
            updatedEntity.Muscle.Should().Be(dto.Muscle);
            updatedEntity.Description.Should().Be(dto.Description);

            _dbContextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));


        }
    }
}
