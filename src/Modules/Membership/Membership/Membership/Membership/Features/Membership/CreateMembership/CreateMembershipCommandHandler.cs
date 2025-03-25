
using ModuleErrors = Membership.Contracts.Membership.ModuleErrors;
using Results = Shared.Results.Results;

namespace Membership.Membership.Features.Membership.CreateMembership
{
    public record CreateMembershipCommand(MembershipDto dto, string authenticationId) : IRequest<Results>;

    public class CreateMembershipCommandHandler(MembershipDbContext membershipDbContext) : IRequestHandler<CreateMembershipCommand, Results>
    {
        public async Task<Results> Handle(CreateMembershipCommand request, CancellationToken cancellationToken)
        {
            var membershipPlan = await membershipDbContext.MembershipPlans.FindAsync(request.dto.MembershipPlanId);
            if (membershipPlan is null)
            {
                return Results.Failure(ModuleErrors.MembershipPlanErrors.NotFound(request.dto.MembershipPlanId));
            }

            var member = await membershipDbContext.Members.FindAsync(request.dto.GymMemberId);
            if (member is null || member.AuthenticationId.ToString() != request.authenticationId)
            {
                return Results.Failure(ModuleErrors.MemberErrors.NotFound(request.dto.GymMemberId));
            }


            var entity = CreateMembership(request.dto, membershipPlan, member);

            await membershipDbContext.Memberships.AddAsync(entity);
            await membershipDbContext.SaveChangesAsync(cancellationToken);

            return Results.Success();
        }

        private Models.Membership CreateMembership(MembershipDto dto, Models.MembershipPlan membershipPlan, Models.Member? member)
        {
            return Models.Membership.Create(member, membershipPlan, dto.MembershipStartDate, dto.TotalPricePaid);
        }
    }

}
