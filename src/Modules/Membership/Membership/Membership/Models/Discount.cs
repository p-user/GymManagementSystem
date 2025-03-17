

namespace Membership.Models
{
    public class Discount : Entity<Guid>
    {
        public string Code { get; private set; } // Unique discount code
        public string Description { get; private set; }
        public decimal? DiscountPercentage { get; private set; }
        public decimal? DiscountAmount { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public int? UsageLimit { get; private set; }
        public int UsageCount { get; private set; }
        public bool IsActive { get; private set; }
        public bool AppliesToAllPlans { get; private set; }
        private readonly List<MembershipPlan> _applicablePlans = new List<MembershipPlan>();
        public IReadOnlyList<MembershipPlan> ApplicablePlans => _applicablePlans.AsReadOnly();
        private Discount() { }

        public static Discount Create(string code, string description, decimal? discountPercentage, decimal? discountAmount, DateTime? startDate, DateTime? endDate, int? usageLimit, bool appliesToAllPlans)
        {
            return new Discount
            {
                Id = Guid.NewGuid(),
                Code = code,
                Description = description,
                DiscountPercentage = discountPercentage,
                DiscountAmount = discountAmount,
                StartDate = startDate,
                EndDate = endDate,
                UsageLimit = usageLimit,
                IsActive = true,
                AppliesToAllPlans = appliesToAllPlans
            };
        }


        public void Update(string code, string description, decimal? discountPercentage, decimal? discountAmount, DateTime? startDate, DateTime? endDate, int? usageLimit, int usageCount, bool appliesToAllPlans)
        {

            Code = code;
            Description = description;
            DiscountPercentage = discountPercentage;
            DiscountAmount = discountAmount;
            StartDate = startDate;
            EndDate = endDate;
            UsageLimit = usageLimit;
            AppliesToAllPlans = appliesToAllPlans;
           
        }


        public void Deactivate()
        {
            IsActive = false;
        }

        public void Activate()
        {
            IsActive = true;
        }

        public void IncrementUsageCount()
        {
            UsageCount++;
        }

        public bool IsApplicableToPlan(MembershipPlan plan)
        {
            return AppliesToAllPlans || ApplicablePlans.Contains(plan);
        }
        public void AddApplicablePlan(MembershipPlan plan)
        {
            if (plan == null)
            {
                throw new ArgumentNullException(nameof(plan), "Membership plan cannot be null.");
            }

            if (!_applicablePlans.Contains(plan))  
            {
                _applicablePlans.Add(plan);
            }
        }
        public void RemoveApplicablePlan(MembershipPlan plan)
        {
            _applicablePlans.Remove(plan);
        }

        public void ClearApplicablePlans()
        {
            _applicablePlans.Clear();
        }


        public decimal ApplyDiscount(decimal originalPrice)
        {
            
            if (DiscountAmount.HasValue)
            {
                return originalPrice * (1 - DiscountAmount.Value);
            }

            else 
            {
                return originalPrice - DiscountAmount.Value;
            }
  
        }
    }
}
