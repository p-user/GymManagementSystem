
namespace Membership.Membership.Dtos
{
    public record MembershipPlanDto
    {
       
        public string Name { get; init ; }
        public decimal Price { get; init; }
        public MembershipDuration DurationInMonths { get; init; }  
        public WeeklyAllowance MaxVisitsPerWeek { get; init; }  // Visits per week (3, 5, 6, or 7 days)
        public string? Description { get; init; }

        // Enum for MembershipDuration and WeeklyAllowance could be handled as integers
        public enum MembershipDuration
        {
            OneMonth = 1,
            ThreeMonths = 3,
            SixMonths = 6,
            OneYear = 12
        }

        public enum WeeklyAllowance
        {
            ThreeTimes = 3,
            FiveTimes = 5,
            SixTimes = 6,
            Everyday = 7
        }
    }

    public record CreateMembershipPlanDto : MembershipPlanDto
    {
    }

    public record ViewMembershipPlanDto : MembershipPlanDto
    {
        public Guid Id { get; init; }
    }
}
