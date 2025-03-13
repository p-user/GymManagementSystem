

namespace Membership.Membership.Dtos
{
    public record MemberDto
    {
    }


    public record UpdateMemberDto
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string PhoneNumber { get; init; }
        public Gender Gender { get; init; }
    }
}
