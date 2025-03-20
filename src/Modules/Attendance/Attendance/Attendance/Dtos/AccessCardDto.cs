
using static Attendance.Models.AccessCard;

namespace Attendance.Attendance.Dtos
{
    public record AccessCardDto
    {
        public string CardNumber { get; init; }
        public Guid UserId { get; init; } 
        
    }

    public record ViewAccessCardDto : AccessCardDto
    {
        public Guid Id { get; init; }
        public AccessCardStatus Status { get; init; }
        public DateTime IssuedAt { get; init; }
    }
}
