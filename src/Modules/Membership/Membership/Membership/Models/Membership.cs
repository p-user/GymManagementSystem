

namespace Membership.Models
{
    public class Membership : Entity<Guid>
    {
        public MembershipPlan MembershipPlan { get; private set; }
        public Guid MembershipPlanId { get; private set; }
        public Member GymMember { get; private set; }
        public Guid GymMemberId { get; private set; }
        public DateTime MembershipStartDate { get; private set; }
        public DateTime? MembershipEndDate { get; private set; }
        public int VisitsRemaining { get; private set; }

        //keep track of promotion code or onsale
        public decimal TotalPricePayed { get; private set; }
        public MembershipStatus Status { get; private set; }
        public Guid? DiscountId { get; private set; } // Added discount id
        public Discount? Discount { get; private set; }
        private Membership() { }


        public enum MembershipStatus
        {
            Active,
            Expired,
            Canceled
        }
        public static Membership Create(Member member, MembershipPlan membershipPlan, DateTime startDate, decimal? totalPricePayed)
        {
            ArgumentNullException.ThrowIfNull(member, nameof(member));
            ArgumentNullException.ThrowIfNull(membershipPlan, nameof(membershipPlan));
            var membership = new Membership
            {
                Id = Guid.NewGuid(),
                GymMember = member,
                MembershipStartDate = startDate,
                MembershipEndDate = startDate.AddMonths((int)membershipPlan.DurationInMonths),
                VisitsRemaining = (int)membershipPlan.MaxVisitsPerWeek,
                TotalPricePayed = totalPricePayed is null or 0 ? membershipPlan.Price : (decimal)totalPricePayed,
                Status = MembershipStatus.Active
            };
            return membership;
        }


        public void Cancel()
        {
            Status = MembershipStatus.Canceled;
            MembershipEndDate = DateTime.UtcNow;  // Set to current time for cancellation
        }

        public void UpdateVisitsRemaining(int visits)
        {
            VisitsRemaining = visits;
        }

        public void Expire()
        {
            Status = MembershipStatus.Expired;
            MembershipEndDate = DateTime.UtcNow;
        }

        public void Activate()
        {
            Status = MembershipStatus.Active;
        }

        public static void UpdateVisitsRemaining(Membership membership)
        {
            membership.VisitsRemaining--;

        }

        //TODO: implement background job to reset visits remaining for the upcomming week
        public static void ResetVisitsRemaining(Membership membership, MembershipPlan membershipPlan) 
        {
            membership.VisitsRemaining = (int)membershipPlan.MaxVisitsPerWeek;

        }
    }
}
