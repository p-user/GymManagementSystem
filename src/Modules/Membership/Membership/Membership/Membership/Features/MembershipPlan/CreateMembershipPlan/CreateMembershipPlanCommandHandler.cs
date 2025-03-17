

namespace Membership.Membership.Features.MembershipPlan.CreateMembershipPlan
{
    public record CreateMembershipPlanCommand(CreateMembershipPlanDto dto) : IRequest<Results<Guid>>;
    public record CreateMembershipPlanResponse(Guid Id);
    public class CreateMembershipPlanCommandHandler(MembershipDbContext membershipDbContext) : IRequestHandler<CreateMembershipPlanCommand, Results<Guid>>
    {
        public async  Task<Results<Guid>> Handle(CreateMembershipPlanCommand request, CancellationToken cancellationToken)
        {
            var membershipPlan = CreateMembershipPlan(request.dto);
            var entity =await membershipDbContext.MembershipPlans.AddAsync(membershipPlan);
            await membershipDbContext.SaveChangesAsync(cancellationToken);
            return membershipPlan.Id;
        }

        private Models.MembershipPlan CreateMembershipPlan(CreateMembershipPlanDto dto)
        {
            var membershipPlan = Models.MembershipPlan.Create(dto.Name, dto.Price, (Models.MembershipPlan.MembershipDuration)dto.DurationInMonths, (Models.MembershipPlan.WeeklyAllowance)dto.MaxVisitsPerWeek, dto.Description);
            return membershipPlan;
        }
    }
}
