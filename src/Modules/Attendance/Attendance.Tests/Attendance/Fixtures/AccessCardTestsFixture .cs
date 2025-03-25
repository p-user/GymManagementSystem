
namespace Attendance.Tests.Attendance.Fixtures
{
    public class AccessCardTestsFixture
    {
        public Faker<AccessCardDto> AccessCardFaker { get; }
        public Mock<AttendanceDbContext> MockDbContext { get; }
        public Mock<DbSet<AccessCard>> MockDbSet { get; }

        public AccessCardTestsFixture()
        {
            MockDbContext = new Mock<AttendanceDbContext>(new DbContextOptions<AttendanceDbContext>());
            MockDbSet = new Mock<DbSet<AccessCard>>();

            // Setup DbSet in Mock DbContext
            MockDbContext.Setup(c => c.AccessCards).Returns(MockDbSet.Object);

            // Use Bogus to generate fake AccessCard data
            AccessCardFaker = new Faker<AccessCardDto>()
                .RuleFor(ac => ac.OwnerId, f => Guid.NewGuid())
                .RuleFor(ac => ac.OwnerType, f => f.PickRandom<Models.AccessCard.AccessCardOwnerType>());

        }
    }
}
