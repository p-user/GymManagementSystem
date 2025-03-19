
using Results = Shared.Results.Results;
using ModuleErrors= Membership.Contracts.Membership.ModuleErrors;

namespace Membership.Membership.Features.Membership.CreateMembership
{
    public record  CreateMembershipCommand(MembershipDto dto, string authenticationId) : IRequest<Results>;

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

            //apply discount if it exists
            //if (request.dto.DiscountCode is not null)
            //{
            //    var discount = await membershipDbContext.Discounts.Where(s=>s.Code ==request.dto.DiscountCode ).FirstOrDefaultAsync(cancellationToken);
            //    if (discount is null)
            //    {
            //        return Results.Failure(ModuleErrors.DicountErrors.NotFound(request.dto.DiscountCode));
            //    }
            //    if (!discount.IsApplicableToPlan(membershipPlan))
            //    {
            //        return Results.Failure(ModuleErrors.DicountErrors.MembershipPlanProblem(request.dto.DiscountCode));
            //    }
            //    if (!discount.IsActive)
            //    {
            //        return Results.Failure(ModuleErrors.DicountErrors.NotActiveProblem(request.dto.DiscountCode));
            //    }

            //    request.dto.TotalPricePaid = discount.ApplyDiscount(membershipPlan.Price);

            //}

            var entity = CreateMembership(request.dto, membershipPlan, member);

            await membershipDbContext.Memberships.AddAsync(entity);
            await membershipDbContext.SaveChangesAsync(cancellationToken);

            return Results.Success();
        }

        private  Models.Membership CreateMembership(MembershipDto dto, Models.MembershipPlan membershipPlan, Models.Member? member)
        {
            return  Models.Membership.Create(member, membershipPlan, dto.MembershipStartDate, dto.TotalPricePaid);
        }
    }
    
}
