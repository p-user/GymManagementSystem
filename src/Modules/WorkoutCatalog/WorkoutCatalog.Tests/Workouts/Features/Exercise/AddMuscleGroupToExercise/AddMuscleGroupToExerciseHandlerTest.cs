
using AutoMapper;
using System.Linq.Expressions;
using WorkoutCatalog.Models;
using WorkoutCatalog.Workouts.Features.Exercise.AddMuscleGroupToExercise;

namespace WorkoutCatalog.Tests.Workouts.Features.Exercise.AddMuscleGroupToExercise
{
    public class AddMuscleGroupToExerciseHandlerTest : IClassFixture<ExerciseFixture>
    {

        private readonly ExerciseFixture _fixture;
        private readonly Mock<WorkoutCatalogDbContext> _dbContextMock;
        private readonly AddMuscleGroupToExerciseCommandHandler _handler;
        private readonly Mock<IMapper> _mapper;

        public AddMuscleGroupToExerciseHandlerTest(ExerciseFixture fixture)
        {
            _fixture = fixture;
            _dbContextMock = new Mock<WorkoutCatalogDbContext>(new DbContextOptions<WorkoutCatalogDbContext>());


            var mockDbSet = _fixture.Exercises.CreateDbSetMock<Models.Exercise, Guid>();


            var mockDbMuscles = fixture.MuscleGroups.MuscleGroups.CreateDbSetMock<Models.MuscleGroup, Guid>().Object;
            _dbContextMock.Setup(x => x.MuscleGroups).Returns(mockDbMuscles);

            _dbContextMock.Setup(x => x.Exercises).Returns(mockDbSet.Object);
            _dbContextMock.Setup(x => x.MuscleGroups).Returns(mockDbMuscles);



            _mapper = new Mock<IMapper>();


            _handler = new AddMuscleGroupToExerciseCommandHandler(_dbContextMock.Object, _mapper.Object );


        }

        [Fact]

        public async Task Add_MuscleGroup_To_Exercise_When_exists()
        {
            //arrange
            var entity = _fixture.Exercises.First();
            var totalCheckNo = entity.MuscleGroups.Count;
            var muscleGroup = _fixture.MuscleGroups.MuscleGroups.Where(s=>s.Muscle.Equals("Forearms")).FirstOrDefault();


            var command = new AddMuscleGroupToExerciseCommand(entity.Id, muscleGroup.Id);
        
            _mapper.Setup(x => x.Map<ViewExerciseDto>(It.IsAny<Models.Exercise>())).Returns((Models.Exercise exercise)=>new ViewExerciseDto
              {
                  Id = exercise.Id,
                  Name = exercise.Name,
                  Description = exercise.Description,
                  MuscleGroups= exercise.MuscleGroups.Select(mg => new ViewMuscleGroupDto
                  {
                      Id = mg.Id,
                      Muscle = mg.Muscle
                  }).ToList()

            });
            //act
            var result = await _handler.Handle(command, CancellationToken.None);
            //assert
            Assert.NotNull(result);
            Assert.Equal(totalCheckNo + 1, result.MuscleGroups.Count);
        }
    }
}
