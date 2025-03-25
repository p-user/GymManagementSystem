
using Bogus;
using Membership.Contracts.Membership.Dtos;
using Shared.Constants;

namespace Authentication.Tests.Authentication.Fixtures
{
    public class RegisterMemberDtoFixture
    {
        public CreateMemberDto FakeUser { get; }
        public RegisterMemberDtoFixture()
        {
            // Generate fake data using Bogus
            var faker = new Faker<CreateMemberDto>()
                .RuleFor(u => u.UserRole, f => DefaultRoles.MemberRole)
                .RuleFor(u => u.Name, f => f.Name.FirstName())
                .RuleFor(u => u.Surname, f => f.Name.LastName())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.Telephone, f => f.Phone.PhoneNumber())
                .RuleFor(u => u.Gender, f => Shared.Enums.Gender.Female);


            FakeUser = faker.Generate();


        }
    }
}
