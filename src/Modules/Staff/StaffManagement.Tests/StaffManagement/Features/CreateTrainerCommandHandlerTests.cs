
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
            // Arrange

            //var registerResponse = new RegisterUserCommandResponse(Guid.NewGuid(), "User registered");
            //_mockSender
            //    .Setup(s => s.Send(It.IsAny<RegisterUserCommand<CreateTra>>(), It.IsAny<CancellationToken>()))
            //    .ReturnsAsync(registerResponse);

            //var registerUserDto = ""; //fixture.CreateValidRegisterUserDto(); //fix

            //var trainerDbSetMock = new Mock<DbSet<Trainer>>();
            //_mockDbContext.Setup(db => db.Trainers).Returns(trainerDbSetMock.Object);

            //// Act: 
            //var command = new CreateTrainerCommand(registerUserDto);
            //var result = await _handler.Handle(command, CancellationToken.None);

            //// Assert
            //_mockSender.Verify(s => s.Send(It.IsAny<RegisterUserCommand<CreateTrainerdt>>(), It.IsAny<CancellationToken>()), Times.Once);
            //_mockDbContext.Verify(db => db.Trainers.AddAsync(It.IsAny<Trainer>(), It.IsAny<CancellationToken>()), Times.Once);
            //_mockDbContext.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);


            //result.Should().NotBeNull();
            //result.message.Should().Be("User registered");
        }
    }
}
