

using Results = Shared.Results.Results;

namespace Membership.Membership.Features.Membership.UpdateVisitsRemaining
{

    public record UpdateVisitsRemainingCommand(Guid UserId) : IRequest<Results>;

    public class UpdateVisitsRemainingCommandHandler(MembershipDbContext _context) : IRequestHandler<UpdateVisitsRemainingCommand, Results>
    {
        public async Task<Results> Handle(UpdateVisitsRemainingCommand request, CancellationToken cancellationToken)
        {

            var membership = await _context.Memberships
                .Where(m => m.GymMemberId == request.UserId && m.Status == Models.Membership.MembershipStatus.Active)
                .SingleOrDefaultAsync(cancellationToken);

            if (membership is null)
            {
                return Results.Failure(MembershipErrors.NotActiveMembershipProblem(request.UserId.ToString()));
            }

            if(membership.VisitsRemaining == 0)
            {
                return Results.Failure(MembershipErrors.NoVisitsRemainingProblem(request.UserId.ToString()));
            }
            membership.UpdateVisitsRemaining(membership.VisitsRemaining);

            _context.Update(membership);
            await _context.SaveChangesAsync(cancellationToken);
            return Results.Success();
        }
    }
}
