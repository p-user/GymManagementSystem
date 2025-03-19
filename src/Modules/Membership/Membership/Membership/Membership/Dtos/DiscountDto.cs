
namespace Membership.Membership.Dtos
{
    public record DiscountDto
    {
        public string Code { get; init; }
        public string Description { get; init; }
        public decimal? DiscountPercentage { get; init; }
        public decimal? DiscountAmount { get; init; }
        public DateTime? StartDate { get; init; }
        public DateTime? EndDate { get; init; }
        public int? UsageLimit { get; init; }
        public int UsageCount { get; init; }
        public bool AppliesToAllPlans { get; init; }
    }

    public record CreateDiscountDto : DiscountDto
    {
    }

    public record UpdateDiscountDto : DiscountDto
    {
    }

    public record ApplyDiscountDto
    {
        public Guid DiscountId { get; set; }
        public decimal Price { get; set; }
    }



}
