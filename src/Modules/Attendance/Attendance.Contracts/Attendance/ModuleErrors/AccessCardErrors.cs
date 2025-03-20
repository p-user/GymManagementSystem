using Shared.Exceptions;

namespace Attendance.Contracts.Attendance.ModuleErrors
{
    public static class AccessCardErrors
    {

        public static Error NotFound(Guid accessCardId) => Error.NotFound("AccessCard.NotFound", $"Access card with id {accessCardId} was not found");
    }
}
