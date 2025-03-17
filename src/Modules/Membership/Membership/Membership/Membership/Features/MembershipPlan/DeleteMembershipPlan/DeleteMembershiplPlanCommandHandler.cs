
using Membership.Membership.ModuleErrors;
using Results = Shared.Results.Results;

namespace Membership.Membership.Features.MembershipPlan.DeleteMembershipPlan
{
    public record DeleteMembershipPlanCommand(Guid Id) : IRequest<Results>;
    public class DeleteMembershiplPlanCommandHandler(MembershipDbContext _context) : IRequestHandler<DeleteMembershipPlanCommand, Results>
    {
        public async Task<Results> Handle(DeleteMembershipPlanCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.MembershipPlans.FindAsync(request.Id);
            if (entity == null)
            {
                return Results.Failure(MembershipPlanErrors.NotFound(request.Id));
            }
            if(entity.Memberships.Any())
            {
                var members = entity.Memberships.Select(s => s.GymMemberId).ToList();
                return Results.Failure(MembershipPlanErrors.DeleteProblem(members));
            }

            _context.MembershipPlans.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Results.Success();
        }
    }
}
