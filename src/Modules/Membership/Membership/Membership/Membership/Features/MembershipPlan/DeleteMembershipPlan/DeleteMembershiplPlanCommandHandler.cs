
namespace Membership.Membership.Features.MembershipPlan.DeleteMembershipPlan
{
    public record DeleteMembershipPlanCommand(Guid Id) : IRequest<DeleteMembershipPlanResponse>;
    public record DeleteMembershipPlanResponse(bool isDeleted);
    public class DeleteMembershiplPlanCommandHandler(MembershipDbContext _context) : IRequestHandler<DeleteMembershipPlanCommand, DeleteMembershipPlanResponse>
    {
        public async Task<DeleteMembershipPlanResponse> Handle(DeleteMembershipPlanCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.MembershipPlans.FindAsync(request.Id);
            if (entity == null)
            {
                throw new Exception("Membership plan was not found!");
            }
            if(entity.Memberships.Any())
            {
                throw new Exception("Membership plan has memberships associated with it!");
            }

            _context.MembershipPlans.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return new DeleteMembershipPlanResponse(true);
        }
    }
}
