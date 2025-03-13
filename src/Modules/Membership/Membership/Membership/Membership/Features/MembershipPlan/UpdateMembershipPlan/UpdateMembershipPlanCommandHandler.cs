
namespace Membership.Membership.Features.MembershipPlan.UpdateMembershipPlan
{
    public record UpdateMembershipPlanCommand(CreateMembershipPlanDto dto , Guid Id) : IRequest<UpdateMembershipPlanResponse>;
    public record UpdateMembershipPlanResponse(Guid Id);

    public class UpdateMembershipPlanCommandHandler(MembershipDbContext _context) : IRequestHandler<UpdateMembershipPlanCommand, UpdateMembershipPlanResponse>
    {
        public async Task<UpdateMembershipPlanResponse> Handle(UpdateMembershipPlanCommand request, CancellationToken cancellationToken)
        {
            var membershipPlan = await _context.MembershipPlans.FindAsync(request.Id);
            if (membershipPlan == null)
            {
                throw new Exception("Membership plan was not found!");
            }

            membershipPlan.Update(
                request.dto.Name, 
                (Models.MembershipPlan.MembershipDuration)request.dto.DurationInMonths,
                (Models.MembershipPlan.WeeklyAllowance)request.dto.MaxVisitsPerWeek, 
                request.dto.Description
                );

            await _context.SaveChangesAsync(cancellationToken);
            return new UpdateMembershipPlanResponse(membershipPlan.Id);
        }
    }
}
