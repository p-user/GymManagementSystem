
using Authentication.Contracts.Authentication.Dtos;
using Authentication.Contracts.Authentication.Features;
using Authentication.Tests.Authentication.Fixtures;
using FluentAssertions;
using Membership.Contracts.Membership.Dtos;
using Shared.Constants;

namespace Authentication.Tests.Authentication.Features.RegisterUser
{
    public class RegisterUserCommandHandlerTest : IClassFixture<RegisterUserCommandHandlerFixture>,IClassFixture<RegisterMemberDtoFixture>
    {
        private readonly RegisterUserCommandHandlerFixture _fixture;
        private readonly RegisterMemberDtoFixture _registerMemberDtoFixture;
        private readonly Mock<UserManager<Models.User>> _mockUserManager;
        private readonly Mock<RoleManager<Models.Role>> _mockRoleManager;
        private readonly Mock<ISender> _mockSender;


        public RegisterUserCommandHandlerTest(RegisterUserCommandHandlerFixture fixture, RegisterMemberDtoFixture fakeMemberFixture )
        {
            _registerMemberDtoFixture = fakeMemberFixture;
            _fixture = fixture;
            _mockUserManager = fixture.UserManagerMock;
            _mockRoleManager = fixture.RoleManagerMock;
           _mockSender = new Mock<ISender>();
        }

        [Fact]
        public async Task Handle_ShouldRegisterUser_WhenRoleIsValidAndUserDoesNotExist()
        {
            // Arrange
            var validRole = DefaultRoles.MemberRole;
            var request = new RegisterUserCommand<CreateMemberDto>(_registerMemberDtoFixture.FakeUser);

            var mockRole = new Models.Role { Name = validRole };
            _mockRoleManager.Setup(x => x.FindByNameAsync(validRole))
                .ReturnsAsync(mockRole); 

            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<Models.User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success); 

            var handler = new RegisterUserCommandHandler<CreateMemberDto>(_mockUserManager.Object, _mockRoleManager.Object, _mockSender.Object);

            // Act
            var response = await handler.Handle(request, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();

            _fixture.UserManagerMock.Verify(u => u.CreateAsync(It.IsAny<Models.User>()), Times.Once);
            _fixture.UserManagerMock.Verify(u => u.AddToRoleAsync(It.IsAny<Models.User>(), DefaultRoles.MemberRole), Times.Once);
        }


       

        [Fact]
        public async Task Handle_ShouldThrowException_WhenRoleIsInvalid()
        {
            // Arrange
            var invalidRole = "test";
            var request = new RegisterUserCommand<CreateMemberDto>(new CreateMemberDto { UserRole = invalidRole }); //create member or create trainer

            _mockRoleManager.Setup(x => x.FindByNameAsync(invalidRole))

                .ReturnsAsync((Models.Role)null); 

            var handler = new RegisterUserCommandHandler<CreateMemberDto>(_mockUserManager.Object, _mockRoleManager.Object, _mockSender.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => handler.Handle(request, CancellationToken.None));
   
        }

    }
}
