

using AutoMapper;


namespace Membership.Membership.Features.MembershipPlan.GetAllMembershipPlans
{

    public record GetAllMembershipPlansQuery : IRequest<Results<List<ViewMembershipPlanDto>>>;
    public record GetAllMembershipPlansQueryResponse(List<ViewMembershipPlanDto> MembershipPlans);
    public class GetAllMembershipPlansQueryHandler(IMapper _mapper, MembershipDbContext _context) : IRequestHandler<GetAllMembershipPlansQuery, Results<List<ViewMembershipPlanDto>>>
    {
        public async Task<Results<List<ViewMembershipPlanDto>>> Handle(GetAllMembershipPlansQuery request, CancellationToken cancellationToken)
        {
            var plans = await _context.MembershipPlans.ToListAsync(cancellationToken);
            var response = _mapper.Map<List<ViewMembershipPlanDto>>(plans);
            return response;
        }
    }
}
