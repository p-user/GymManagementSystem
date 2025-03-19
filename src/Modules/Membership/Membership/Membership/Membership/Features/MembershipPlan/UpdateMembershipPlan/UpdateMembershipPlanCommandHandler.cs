

namespace Membership.Membership.Features.MembershipPlan.UpdateMembershipPlan
{
    public record UpdateMembershipPlanCommand(CreateMembershipPlanDto dto , Guid Id) : IRequest<Shared.Results.Results>;
    public record UpdateMembershipPlanResponse(Guid Id);

    public class UpdateMembershipPlanCommandHandler(MembershipDbContext _context) : IRequestHandler<UpdateMembershipPlanCommand, Shared.Results.Results>
    {
        public async Task<Shared.Results.Results> Handle(UpdateMembershipPlanCommand request, CancellationToken cancellationToken)
        {
            var membershipPlan = await _context.MembershipPlans.FindAsync(request.Id);
            if (membershipPlan == null)
            {
                return Shared.Results.Results.Failure(MembershipModuleErrors.MembershipPlanErrors.NotFound(request.Id));
               
            }

            membershipPlan.Update(
                request.dto.Name, 
                (Models.MembershipPlan.MembershipDuration)request.dto.DurationInMonths,
                (Models.MembershipPlan.WeeklyAllowance)request.dto.MaxVisitsPerWeek, 
                request.dto.Description
                );

            await _context.SaveChangesAsync(cancellationToken);
            return Shared.Results.Results.Success();
        }
    }
}
