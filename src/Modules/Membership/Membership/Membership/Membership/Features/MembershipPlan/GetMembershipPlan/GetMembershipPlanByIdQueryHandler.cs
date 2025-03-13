

using AutoMapper;

namespace Membership.Membership.Features.MembershipPlan.GetMembershipPlan
{
    public record GetMembershipPlanByIdQuery(Guid Id) : IRequest<GetMembershipPlanByIdResponse>;
    public record GetMembershipPlanByIdResponse(ViewMembershipPlanDto MembershipPlan);
    public class GetMembershipPlanByIdQueryHandler(MembershipDbContext _membershipDbContext, IMapper _mapper) : IRequestHandler<GetMembershipPlanByIdQuery, GetMembershipPlanByIdResponse>
    {
        
       
        public async Task<GetMembershipPlanByIdResponse> Handle(GetMembershipPlanByIdQuery request, CancellationToken cancellationToken)
        {
            var membershipPlan = await _membershipDbContext.MembershipPlans.FindAsync(request.Id, cancellationToken);
            if (membershipPlan is null)
            {
                throw new Exception("Membership was not found!");
            }
            var response = _mapper.Map<ViewMembershipPlanDto>(membershipPlan);
            return new GetMembershipPlanByIdResponse(response);
        }
    }
    
}
