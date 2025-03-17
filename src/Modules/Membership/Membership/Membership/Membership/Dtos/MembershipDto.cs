
namespace Membership.Membership.Dtos
{
    public record MembershipDto
    {
        public Guid MembershipPlanId { get; init; }
        public Guid GymMemberId { get; init; }
        public DateTime MembershipStartDate { get; init; }
        public DateTime? MembershipEndDate { get; init; }
        public int? VisitsRemaining { get; init; }
        public decimal? TotalPricePaid { get; set; }
        public string Status { get; init; } = string.Empty;
        public string? DiscountCode { get; init; }
    }

    public record CreateMembershipDto : MembershipDto
    {
        public DateTime MembershipStartDate { get; init; } = DateTime.UtcNow;


    }

    public record ViewMembershipDto : MembershipDto
    {
        public Guid Id { get; init; }
        public string MembershipPlanName { get; init; } = string.Empty;
        public string GymMemberName { get; init; } = string.Empty;
        public string? DiscountName { get; init; }
    }
}
