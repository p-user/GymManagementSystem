
namespace Authentication.Tests.Fixtures
{
    public class RegisterUserCommandHandlerFixture
    {
        public Mock<UserManager<Models.User>> UserManagerMock { get; }
        public Mock<RoleManager<Models.Role>> RoleManagerMock { get; }
        public Mock<ISender> SenderMock { get; }

        public RegisterUserCommandHandlerFixture()
        {
            UserManagerMock = new Mock<UserManager<Models.User>>(
                Mock.Of<IUserStore<Models.User>>(), null, null, null, null, null, null, null, null);

            RoleManagerMock = new Mock<RoleManager<Models.Role>>(
                Mock.Of<IRoleStore<Models.Role>>(), null, null, null, null);

            SenderMock = new Mock<ISender>();

            // Setup default behavior for UserManager
            UserManagerMock.Setup(u => u.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((string email) => email == "existing@example.com" ? new Models.User { Email = email } : null);

            UserManagerMock.Setup(u => u.CreateAsync(It.IsAny<Models.User>()))
                .ReturnsAsync(IdentityResult.Success);

            UserManagerMock.Setup(u => u.AddToRoleAsync(It.IsAny<Models.User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            // Setup default behavior for RoleManager
            RoleManagerMock.Setup(r => r.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((string roleName) => roleName == "ValidRole" ? new Models.Role { Name = roleName } : null);
        }
    }
}