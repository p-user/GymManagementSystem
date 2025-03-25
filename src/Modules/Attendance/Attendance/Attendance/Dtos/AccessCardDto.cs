
using static Attendance.Models.AccessCard;

namespace Attendance.Attendance.Dtos
{
    public record AccessCardDto
    {
        public Guid OwnerId { get; init; }
        public AccessCardOwnerType OwnerType { get; init; }

    }

    public record ViewAccessCardDto : AccessCardDto
    {
        public string CardNumber { get; init; }
        public Guid Id { get; init; }
        public AccessCardStatus Status { get; init; }
        public DateTime IssuedAt { get; init; }
    }
}
