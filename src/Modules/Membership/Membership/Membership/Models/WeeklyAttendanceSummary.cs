
namespace Membership.Models
{
    public class WeeklyAttendanceSummary
    {

        public Guid MemberId { get; }
        public int VisitsThisWeek { get; }
        public int MaxAllowedVisits { get; }

        public WeeklyAttendanceSummary(Guid memberId, int visitsThisWeek, int maxAllowedVisits)
        {
            MemberId = memberId;
            VisitsThisWeek = visitsThisWeek;
            MaxAllowedVisits = maxAllowedVisits;
        }

        public bool CanEnter() => MaxAllowedVisits == 0 || VisitsThisWeek < MaxAllowedVisits;
    }
}
