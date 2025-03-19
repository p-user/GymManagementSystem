

using Shared.Exceptions;

namespace StaffManagement.Contracts.StaffManagement.ModuleErrors
{
    public static class SessionErrors
    {
        public static Error NotFound(Guid Id) =>
     Error.NotFound("Session.NotFound", $"The Session with the identifier {Id} was not found");
    }
}
