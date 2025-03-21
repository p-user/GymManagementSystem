
using Attendance.Attendance.Features.AccessCards.CreateAccessCard;
using Attendance.Tests.Attendance.Fixtures;
using FluentValidation;

namespace Attendance.Tests.Attendance.Features.AccessCards.CreateAccessCard
{
    public class CreateAccessCardTests : IClassFixture<AccessCardTestsFixture>
    {
        private readonly AccessCardTestsFixture _fixture;
        private readonly CreateAccessCardCommandHandler _handler;
        private readonly Mock<IValidator<CreateAccessCardCommand>> _validator;

        public CreateAccessCardTests(AccessCardTestsFixture fixture)
        {
            _fixture = fixture;
            _validator = new Mock<IValidator<CreateAccessCardCommand>>();

            _validator.Setup(v => v.ValidateAsync(It.IsAny<CreateAccessCardCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult());


            _handler = new CreateAccessCardCommandHandler(_fixture.MockDbContext.Object, _validator.Object);
        }


        [Fact]
        public async Task Handle_ValidRequest_CreatesAccessCard()
        {
            // Arrange
            var mockDbContext = _fixture.MockDbContext;
            var mockDbSet = _fixture.MockDbSet;

            var validAccessCard = _fixture.AccessCardFaker.Generate();
            var command = new CreateAccessCardCommand(validAccessCard);


            AccessCard? capturedAccessCard = null;
            mockDbSet.Setup(s => s.AddAsync(It.IsAny<AccessCard>(), It.IsAny<CancellationToken>()))
                .Callback<AccessCard, CancellationToken>((ac, _) => capturedAccessCard = ac)
                 .ReturnsAsync((AccessCard ac, CancellationToken _) =>
                 {
                     var entry = new Mock<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AccessCard>>(null); 
                     entry.Setup(e => e.Entity).Returns(ac); 
                     return entry.Object;
                 });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(capturedAccessCard);
            Assert.Equal(validAccessCard.OwnerId, capturedAccessCard.OwnerId);
            Assert.Equal(validAccessCard.OwnerType, capturedAccessCard.OwnerType);
          
            Assert.NotNull(capturedAccessCard.CardNumber);

            mockDbSet.Verify(s => s.AddAsync(It.IsAny<AccessCard>(), It.IsAny<CancellationToken>()), Times.Once);
            mockDbContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
