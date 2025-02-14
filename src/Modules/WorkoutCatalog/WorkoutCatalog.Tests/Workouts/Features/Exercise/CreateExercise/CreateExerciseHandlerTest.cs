using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WorkoutCatalog.Tests.Workouts.Fixtures;
using WorkoutCatalog.Workouts.Features.Exercise.CreateExcercise;

namespace WorkoutCatalog.Tests.Workouts.Features.Exercise.CreateExercise
{
    public class CreateExerciseHandlerTest : IClassFixture<ExerciseFixture>, IClassFixture<AutoMapperFixture>
    {
        private readonly ExerciseFixture _fixture;
        private readonly Mock<WorkoutCatalogDbContext> _dbContextMock;
        private readonly CreateExcerciseCommandHandler _handler;
        private readonly IMapper _mapper;

        public CreateExerciseHandlerTest(ExerciseFixture fixture, AutoMapperFixture autoMapperFixture)
        {
            //ToDo : fix this
            _dbContextMock = new Mock<WorkoutCatalogDbContext>(new DbContextOptions<WorkoutCatalogDbContext>());
            _fixture = fixture;

            _mapper = autoMapperFixture.Mapper;

            var mockDb = _fixture.Exercises.CreateDbSetMock<Models.Exercise, Guid>();
          
           
            _dbContextMock.Setup(x => x.Exercises).Returns(mockDb.Object);
          

            var mockDbCategories = fixture.Categories.ExerciseCategories.CreateDbSetMock<Models.ExerciseCategory, Guid>().Object;
            _dbContextMock.Setup(x => x.ExerciseCategories).Returns(mockDbCategories);
         

            var mockDbMuscles = fixture.MuscleGroups.MuscleGroups.CreateDbSetMock<Models.MuscleGroup, Guid>().Object;
            _dbContextMock.Setup(x => x.MuscleGroups).Returns(mockDbMuscles);


            _handler = new CreateExcerciseCommandHandler(_dbContextMock.Object);
        }

        [Fact]
        public async Task Create_Exercise_Returns_True()
        {
            //arrange
            var entity = _fixture.Exercises.First();

            var dto = _mapper.Map<CreateExerciseDto>(entity);   

          

            var command = new CreateExcerciseCommand(dto);


            //act
            var result = await _handler.Handle(command, CancellationToken.None);

            
            _dbContextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

           var dbEntity = await  _dbContextMock.Object.Exercises.FindAsync(entity.Id);


            //assert
            Assert.NotNull(dbEntity);
           dbEntity.Name.Should().Be(entity.Name);
           dbEntity.Description.Should().Be(entity.Description);
        }
    }
}
