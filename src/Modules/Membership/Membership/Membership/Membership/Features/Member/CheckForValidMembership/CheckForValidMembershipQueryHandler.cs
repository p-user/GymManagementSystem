

using static Membership.Models.Membership;

namespace Membership.Membership.Features.Member.CheckForValidMembership
{
    public class CheckForValidMembershipQueryHandler(MembershipDbContext _context) : IRequestHandler<CheckForValidMembershipQuery, Results<bool>>
    {
        public async Task<Results<bool>> Handle(CheckForValidMembershipQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Memberships
                .Where(m => m.GymMemberId == request.MemberId)
                .Where(m => m.MembershipStartDate <= DateTime.UtcNow)
                .Where(m => m.MembershipEndDate >= DateTime.UtcNow)
                .Where(m => m.Status == MembershipStatus.Active)
                .AnyAsync(cancellationToken);
            return result;
        }
    }
}
