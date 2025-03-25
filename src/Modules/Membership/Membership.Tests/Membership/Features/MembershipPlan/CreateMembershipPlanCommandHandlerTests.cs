

using Bogus;
using FluentAssertions;
using Membership.Data;
using Membership.Membership.Dtos;
using Membership.Membership.Features.MembershipPlan.CreateMembershipPlan;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using Moq.EntityFrameworkCore;
using Shared.Results;

namespace Membership.Tests.Membership.Features.MembershipPlan
{
    public class CreateMembershipPlanCommandHandlerTests
    {
        private readonly Mock<MembershipDbContext> _mockDbContext;
        private readonly CreateMembershipPlanCommandHandler _handler;
        private readonly Faker<CreateMembershipPlanDto> _faker;

        public CreateMembershipPlanCommandHandlerTests()
        {
            _faker = new Faker<CreateMembershipPlanDto>()
          .RuleFor(d => d.Name, f => f.Commerce.ProductName())
          .RuleFor(d => d.Price, f => f.Finance.Amount(10, 200))
          .RuleFor(d => d.DurationInMonths, f => f.PickRandom<CreateMembershipPlanDto.MembershipDuration>())
          .RuleFor(d => d.MaxVisitsPerWeek, f => f.PickRandom<CreateMembershipPlanDto.WeeklyAllowance>())
          .RuleFor(d => d.Description, f => f.Lorem.Sentence());



            _mockDbContext = new Mock<MembershipDbContext>(new DbContextOptions<MembershipDbContext>());


            _mockDbContext.Setup(db => db.MembershipPlans).ReturnsDbSet(new List<Models.MembershipPlan>());
            _handler = new CreateMembershipPlanCommandHandler(_mockDbContext.Object);
        }

        [Fact]
        public async Task Handle_Should_Create_MembershipPlan_And_Return_Id()
        {
            // Arrange
            Results<CreateMembershipPlanDto> dto = _faker.Generate();
            var command = new CreateMembershipPlanCommand(dto.Value);
            _mockDbContext.Setup(db => db.AddAsync(It.IsAny<Models.MembershipPlan>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync((Models.MembershipPlan entity, CancellationToken _) =>
                 {
                     var entityEntryMock = new Mock<EntityEntry<Models.MembershipPlan>>();
                     entityEntryMock.Setup(e => e.Entity).Returns(entity);
                     return entityEntryMock.Object;
                 });

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeTrue();
            response.Value.Should().NotBeEmpty();

            //response.;
        }
    }
}
