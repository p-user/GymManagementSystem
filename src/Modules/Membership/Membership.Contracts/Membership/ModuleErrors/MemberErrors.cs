using Shared.Exceptions;

namespace Membership.Contracts.Membership.ModuleErrors
{
   public static class MemberErrors
    {
        public static Error NotFound(Guid memberId) =>
       Error.NotFound("Members.NotFound", $"The member with the identifier {memberId} was not found");
    }
}
