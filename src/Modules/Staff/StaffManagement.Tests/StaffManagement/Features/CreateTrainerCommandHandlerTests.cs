namespace StaffManagement.Tests.StaffManagement.Features
{
    public class CreateTrainerCommandHandlerTests : IClassFixture<TrainerFixture>
    {
        private readonly Mock<StaffDbContext> _mockDbContext;
        private readonly Mock<ISender> _mockSender;
        private readonly CreateTrainerCommandHandler _handler;
        private readonly TrainerFixture fixture;


        public CreateTrainerCommandHandlerTests(TrainerFixture trainerFixture)
        {
            _mockDbContext = new Mock<StaffDbContext>(new DbContextOptions<StaffDbContext>());
            _mockSender = new Mock<ISender>();
            fixture = trainerFixture;

            _handler = new CreateTrainerCommandHandler(_mockDbContext.Object, _mockSender.Object);
        }

        [Fact]
        public async Task Handle_Should_Create_Trainer_When_User_Is_Registered()
        {



        }
    }
}
