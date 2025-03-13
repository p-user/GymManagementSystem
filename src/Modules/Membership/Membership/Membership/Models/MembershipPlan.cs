

namespace Membership.Models
{
    public class MembershipPlan : Entity<Guid>
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public MembershipDuration DurationInMonths { get; private set; }  // Monthly, 3 months, 6 months, or 1 year
        public WeeklyAllowance MaxVisitsPerWeek { get; private set; }  // Visits per week (3, 5, 6, or 7 days)
        public string? Description { get; set; }

        private List<Membership> _memberships = new List<Membership>();

        public IReadOnlyList<Membership> Memberships  => _memberships.AsReadOnly();

        private readonly List<Discount> _discountsApplicable = new List<Discount>();
        public IReadOnlyList<Discount> DiscountsApplicable => _discountsApplicable.AsReadOnly();



        private MembershipPlan() { }


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

        public static MembershipPlan Create(string name, decimal price, MembershipDuration duration, WeeklyAllowance maxVisitsPerWeek, string description)
        {
            ArgumentNullException.ThrowIfNull(name, nameof(name));
            ArgumentNullException.ThrowIfNull(description, nameof(description));
            var membershipPlan = new MembershipPlan
            {
                Id = Guid.NewGuid(),
                Name = name,
                Price = price,
                DurationInMonths = duration,
                MaxVisitsPerWeek = maxVisitsPerWeek,
                Description = description
            };
            return membershipPlan;
        }


        public void Update(string name,  MembershipDuration duration, WeeklyAllowance maxVisitsPerWeek, string description)
        {
            ArgumentNullException.ThrowIfNull(name, nameof(name));
            ArgumentNullException.ThrowIfNull(description, nameof(description));

            Name = name;
            DurationInMonths = duration;
            MaxVisitsPerWeek = maxVisitsPerWeek;
            Description = description;
          
        }


        public void UpdatePrice(decimal newPrice)
        {
            Price = newPrice;
        }

        public void AddDiscount(Discount discount)
        {
            
            if (!_discountsApplicable.Contains(discount))
                _discountsApplicable.Add(discount);
        }

        public void RemoveDiscount(Discount discount)
        {
            _discountsApplicable.Remove(discount);
        }

        public bool HasDiscount(Discount discount)
        {
            return _discountsApplicable.Contains(discount);
        }



    }
}
