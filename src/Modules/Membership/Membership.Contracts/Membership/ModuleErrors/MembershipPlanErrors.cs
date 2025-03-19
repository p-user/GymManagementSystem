
using Shared.Exceptions;

namespace Membership.Contracts.Membership.ModuleErrors
{
    public static class MembershipPlanErrors
    {
        public static Error NotFound(Guid membershipPlanId) =>
       Error.NotFound("MembershipPlan.NotFound", $"The membershipPlan with the identifier {membershipPlanId} was not found");

        public static Error DeleteProblem(List<Guid> memberIds) =>
      Error.Problem("MembershipPlan.AssociatedWithMembers", $"The membershipPlan has been associated with active members listed : {memberIds} ");
    }
}
