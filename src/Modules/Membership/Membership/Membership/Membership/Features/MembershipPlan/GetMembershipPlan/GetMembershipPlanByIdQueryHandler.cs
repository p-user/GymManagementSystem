namespace Membership.Membership.Features.MembershipPlan.GetMembershipPlan
{
    public record GetMembershipPlanByIdQuery(Guid Id) : IRequest<Results<ViewMembershipPlanDto>>;
    public class GetMembershipPlanByIdQueryHandler(MembershipDbContext _membershipDbContext, IMapper _mapper) : IRequestHandler<GetMembershipPlanByIdQuery, Results<ViewMembershipPlanDto>>
    {
        public async Task<Results<ViewMembershipPlanDto>> Handle(GetMembershipPlanByIdQuery request, CancellationToken cancellationToken)
        {
            var membershipPlan = await _membershipDbContext.MembershipPlans.FindAsync(request.Id, cancellationToken);
            if (membershipPlan is null)
            {
                throw new Exception("Membership was not found!");
            }
            var response = _mapper.Map<ViewMembershipPlanDto>(membershipPlan);
            return response;
        }
    }

}
