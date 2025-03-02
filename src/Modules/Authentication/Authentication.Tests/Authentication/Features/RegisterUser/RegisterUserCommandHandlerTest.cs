
namespace Authentication.Tests.Authentication.Features.RegisterUser
{
    public class RegisterUserCommandHandlerTest : IClassFixture<RegisterUserCommandHandlerFixture>
    {
        private readonly RegisterUserCommandHandlerFixture _fixture;
        private readonly RegisterUserCommandHandler _handler;

        public RegisterUserCommandHandlerTest(RegisterUserCommandHandlerFixture fixture)
        {
            _fixture = fixture;
            _handler = new RegisterUserCommandHandler(
                _fixture.UserManagerMock.Object,
                _fixture.RoleManagerMock.Object,
                _fixture.SenderMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldRegisterUser_WhenRoleIsValidAndUserDoesNotExist()
        {
            // Arrange
            var userDto = new RegisterUserDto { Email = "newuser@example.com", Name = "Test", Surname = "User", Telephone = "1234567890" };
            var command = new RegisterUserCommand(userDto, "ValidRole");

            _fixture.RoleManagerMock.Setup(r => r.FindByNameAsync("ValidRole")).ReturnsAsync(new Models.Role { Name = "ValidRole" });
            _fixture.UserManagerMock.Setup(u => u.FindByEmailAsync(userDto.Email)).ReturnsAsync((Models.User)null);
            _fixture.UserManagerMock.Setup(u => u.CreateAsync(It.IsAny<Models.User>())).ReturnsAsync(IdentityResult.Success);
            _fixture.UserManagerMock.Setup(u => u.AddToRoleAsync(It.IsAny<Models.User>(), "ValidRole")).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal("true", result.test);
            _fixture.UserManagerMock.Verify(u => u.CreateAsync(It.IsAny<Models.User>()), Times.Once);
            _fixture.UserManagerMock.Verify(u => u.AddToRoleAsync(It.IsAny<Models.User>(), "ValidRole"), Times.Once);
        }


        [Fact]
        public async Task Handle_ShouldThrowException_WhenUserAlreadyExists()
        {
            // Arrange
            var userDto = new RegisterUserDto { Email = "existing@example.com", Name = "Test", Surname = "User", Telephone = "1234567890" };
            var command = new RegisterUserCommand(userDto, "ValidRole");

            _fixture.RoleManagerMock.Setup(r => r.FindByNameAsync("ValidRole")).ReturnsAsync(new Models.Role { Name = "ValidRole" });
            _fixture.UserManagerMock.Setup(u => u.FindByEmailAsync(userDto.Email)).ReturnsAsync(new Models.User { Email = userDto.Email });

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        }


        [Fact]
        public async Task Handle_ShouldThrowException_WhenRoleIsInvalid()
        {
            // Arrange
            var userDto = new RegisterUserDto { Email = "newuser@example.com", Name = "Test", Surname = "User", Telephone = "1234567890" };
            var command = new RegisterUserCommand(userDto, "InvalidRole");

            _fixture.RoleManagerMock.Setup(r => r.FindByNameAsync("InvalidRole")).ReturnsAsync((Models.Role)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        }

    }
}
