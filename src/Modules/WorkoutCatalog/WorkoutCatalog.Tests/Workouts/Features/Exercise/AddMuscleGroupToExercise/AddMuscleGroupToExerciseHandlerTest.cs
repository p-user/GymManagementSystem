
using AutoMapper;
using WorkoutCatalog.Workouts.Features.Exercise.AddMuscleGroupToExercise;

namespace WorkoutCatalog.Tests.Workouts.Features.Exercise.AddMuscleGroupToExercise
{
    public class AddMuscleGroupToExerciseHandlerTest : IClassFixture<ExerciseFixture>, IClassFixture<AutoMapperFixture>
    {

        private readonly ExerciseFixture _fixture;
        private readonly Mock<WorkoutCatalogDbContext> _dbContextMock;
        private readonly AddMuscleGroupToExerciseCommandHandler _handler;
        private readonly IMapper _mapper;

        public AddMuscleGroupToExerciseHandlerTest(ExerciseFixture fixture, AutoMapperFixture autoMapperFixture)
        {
            _fixture = fixture;
            _dbContextMock = new Mock<WorkoutCatalogDbContext>(new DbContextOptions<WorkoutCatalogDbContext>());


            var mockDbSet = _fixture.Exercises.CreateDbSetMock<Models.Exercise, Guid>();


            var mockDbMuscles = fixture.MuscleGroups.MuscleGroups.CreateDbSetMock<Models.MuscleGroup, Guid>().Object;
            _dbContextMock.Setup(x => x.MuscleGroups).Returns(mockDbMuscles);

            _dbContextMock.Setup(x => x.Exercises).Returns(mockDbSet.Object);
            _dbContextMock.Setup(x => x.MuscleGroups).Returns(mockDbMuscles);



            _mapper = autoMapperFixture.Mapper;


            _handler = new AddMuscleGroupToExerciseCommandHandler(_dbContextMock.Object, _mapper);


        }

        [Fact]

        public async Task Add_MuscleGroup_To_Exercise_When_exists()
        {
            //arrange
            var entity = _fixture.Exercises.First();

            var muscleGroup = _fixture.MuscleGroups.MuscleGroups.Where(s => s.Muscle.Equals("Forearms")).FirstOrDefault();


            var command = new AddMuscleGroupToExerciseCommand(entity.Id, muscleGroup.Id);

            //act
            var result = await _handler.Handle(command, CancellationToken.None);

            var updatedEntity = await _dbContextMock.Object.Exercises
                            .Include(e => e.MuscleGroups)
                            .FirstOrDefaultAsync(e => e.Id == entity.Id);


            //assert
            Assert.NotNull(result);
            Assert.Equal(updatedEntity.MuscleGroups.Count, result.MuscleGroups.Count);
        }
    }
}
