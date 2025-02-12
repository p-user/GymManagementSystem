

using AutoMapper;
using WorkoutCatalog.Workouts.Features.ExerciseCategory.GetExerciseCategoryById;

namespace WorkoutCatalog.Tests.Workouts.Features.ExerciseCategory.GetExerciseCategryById
{
    public class GetExerciseCategoryByIdHandlerTest : IClassFixture<ExerciseCategoryFixture>
    {

        private readonly Mock<WorkoutCatalogDbContext> _dbContextMock;
        private readonly GetExerciseCategoryByIdQueryHandler _handler;
        private readonly Mock<IMapper> _mapper;
        private readonly ExerciseCategoryFixture _fixture;
        public GetExerciseCategoryByIdHandlerTest(ExerciseCategoryFixture exerciseCategoryFixture)
        {
            _mapper = new Mock<IMapper>();
            _fixture = exerciseCategoryFixture;
            _dbContextMock = new Mock<WorkoutCatalogDbContext>(new DbContextOptions<WorkoutCatalogDbContext>());
            _dbContextMock.Setup(s=>s.ExerciseCategories).Returns(_fixture.ExerciseCategories.CreateDbSetMock<Models.ExerciseCategory, Guid>().Object);
            _handler = new GetExerciseCategoryByIdQueryHandler(_dbContextMock.Object, _mapper.Object);
        }


        [Fact]
        public async Task Get_Exercise_Category_By_Id_When_Exists()
        {
            _mapper.Setup(_mapper => _mapper.Map<ViewExerciseCategoryDto>(It.IsAny<Models.ExerciseCategory>()))
                .Returns((Models.ExerciseCategory exerciseCategory) => new ViewExerciseCategoryDto
                {
                    Id = exerciseCategory.Id,
                    Name = exerciseCategory.Name,
                    Description = exerciseCategory.Description
                    
                });

            //arrange
            var entity = _fixture.ExerciseCategories.First();
            var query = new GetExerciseCategoryByIdQuery(entity.Id);

            //act
            var result = await _handler.Handle(query, CancellationToken.None);
            Assert.NotNull(result);
            Assert.Equal(entity.Id, result.Id);
            Assert.Equal(entity.Name, result.Name);
            Assert.Equal(entity.Description, result.Description);

        }
    }
}
