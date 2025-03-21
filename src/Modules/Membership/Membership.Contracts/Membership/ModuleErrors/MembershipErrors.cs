
using Shared.Exceptions;

namespace Membership.Contracts.Membership.ModuleErrors
{
    public static class MembershipErrors
    {

        public static Error NotActiveMembershipProblem(string Id) =>
   Error.Problem("Membership.NotActive", $"The membership  identified for the user  {Id} is not active or the user does not exists!");



        public static Error NoVisitsRemainingProblem(string Id) =>
            Error.Problem("Membership.VisitsExceeded", $"The membership  {Id} has no more remaining visitis for this week!");
    }
}