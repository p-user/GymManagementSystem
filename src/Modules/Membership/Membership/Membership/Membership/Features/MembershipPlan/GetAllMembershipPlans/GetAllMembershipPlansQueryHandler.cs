

using AutoMapper;

namespace Membership.Membership.Features.MembershipPlan.GetAllMembershipPlans
{

    public record GetAllMembershipPlansQuery : IRequest<GetAllMembershipPlansQueryResponse>;
    public record GetAllMembershipPlansQueryResponse(List<ViewMembershipPlanDto> MembershipPlans);
    public class GetAllMembershipPlansQueryHandler(IMapper _mapper, MembershipDbContext _context) : IRequestHandler<GetAllMembershipPlansQuery, GetAllMembershipPlansQueryResponse>
    {
        public async Task<GetAllMembershipPlansQueryResponse> Handle(GetAllMembershipPlansQuery request, CancellationToken cancellationToken)
        {
            var plans = await _context.MembershipPlans.ToListAsync(cancellationToken);
            var response = new GetAllMembershipPlansQueryResponse(_mapper.Map<List<ViewMembershipPlanDto>>(plans));
            return response;
        }
    }
}
