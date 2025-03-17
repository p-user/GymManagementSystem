
using Shared.Exceptions;

namespace Membership.Membership.ModuleErrors
{
    public static class DicountErrors
    {
        public static Error NotFound(string Id) =>
      Error.NotFound("Discount.NotFound", $"The discount with the identifier {Id} was not found");

        public static Error NotActiveProblem(string Id) =>
    Error.Problem("Discount.NotActive", $"The discount with the identifier {Id} is not active anymore!");

        public static Error MembershipPlanProblem(string Id) =>
     Error.Problem("Discount.NotApplicableToThisMembershipPlan", $"The discount with the identifier {Id} is not applicable to the membershipplan you provided");
    }
}

